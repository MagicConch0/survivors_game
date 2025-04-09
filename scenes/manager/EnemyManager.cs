using Godot;
using System;
using System.Threading;
/*
	 * 敌人管理器（生成器）

*/
public partial class EnemyManager : Node
{
	// Called when the node enters the scene tree for the first time.

	[Export]
	PackedScene enemyScene;//敌人资源

	[Export]
	public float SPAWN_RADIUS = 320;//生成半径

	[Export]
	public ArenaTimeManager arenaTimeManager;//导出游戏时间管理器

	Player player;//获取玩家

	Godot.Timer timer;//生成敌人计时器

	private double baseSpawnTime = 0f;//基础敌人生成时间
	public override void _Ready()
	{

		timer = GetNode<Godot.Timer>("Timer");
		baseSpawnTime = timer.WaitTime;
		//连接信号：将timer的Timeout信号连接到当前类的OnTimerTimeout方法
		timer.Timeout += OnTimerTimeout;
		if (arenaTimeManager != null)
		{
			arenaTimeManager.ArenaDifficultyIncreased += OnArenaDifficultyIncreased;
		}
		player = GetTree().GetFirstNodeInGroup("player") as Player;//获取玩家节点
		if (player == null)
		{
			throw new Exception("敌人管理器(EnemyManager)中获取玩家节点失败");
		}
	}




	/// <summary>
	/// 获取生成位置，判断位置是否在地图边缘内，如果不在则将位置旋转90度后再次检测，
	/// 最多4次旋转即可获取正确生成位置
	/// </summary>
	/// <returns>地图范围内随机位置</returns>
	public Vector2 GetSpawnPosition()
	{
		if (player == null)
		{
			return Vector2.Zero;
		}
		//设置一个生成范围，因为游戏窗口高度为640，宽为360，所以如果想要让敌人生成在玩笑视野外，则需要范围半径在320或者以上
		Vector2 range_derction = Vector2.Right.Rotated((float)GD.RandRange(0.0f, (float)Math.Tau)) * SPAWN_RADIUS;
		Vector2 spawn_position = Vector2.Zero;//定义变量，存放生成敌人的目标位置
											  //判断目标位置是否生成在地图外，如果生成地图外则让生成位置旋转90度，最多旋转4此后必定会生成在地图内
		for (int i = 1; i <= 4; i++)
		{
			spawn_position = player.GlobalPosition + range_derction;//设置目标位置

			//判定目标位置是否可用，是否在地图边缘内
			//设置入参：参数1：玩家位置，参数2：敌人生成位置，参数3：与世界地形的碰撞层（瓦片地图设置在了0层，值为1）
			var qurey_paramaters = PhysicsRayQueryParameters2D.Create(player.GlobalPosition, spawn_position, 1);
			//从起始点向目标点发射射线，检测路径上有没有与指定碰撞层发生碰撞
			Godot.Collections.Dictionary dictionary = GetTree().Root.World2D.DirectSpaceState.IntersectRay(qurey_paramaters);


			if (dictionary.Count == 0)
			{//如果没有发生碰撞，则敌人可以在该位置生成
				break;
			}
			else
			{//如果发生了碰撞，则说明敌人生成的位置在地图边界之外，需要重新生成位置
				range_derction = range_derction.Rotated(Mathf.DegToRad(90));//将角度旋转90度
			}
		}
		return spawn_position;

	}


	/// <summary>
	/// 定时器结束后实例化敌人
	/// </summary>
	private void OnTimerTimeout()
	{
		timer.Start();//用start()方法可以使在代码中修改计时器时间生效
					  // Node2D player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null)
		{
			return;
		}
		//实例化敌人
		Node2D enemy = enemyScene.Instantiate() as Node2D;

		//获取实体层，将敌人添加到实体层中，用y轴判断位置，y轴大的覆盖y轴小的
		Node2D entitys_layer = GetTree().GetFirstNodeInGroup("enities_layer") as Node2D;
		entitys_layer.AddChild(enemy);//添加到实体层节点
		enemy.GlobalPosition = GetSpawnPosition();//设置敌人位置
	}

	/// <summary>
	/// 当游戏难度升级时触发,随着时间增长组件加快敌人生成速度，限制在最块0.3秒一波
	/// </summary>
	/// <param name="arenaDifficulty">当前游戏难度</param>
	private void OnArenaDifficultyIncreased(int arenaDifficulty)
	{
		double time_off = (.1 / 12) * arenaDifficulty;
		timer.WaitTime = Math.Max(baseSpawnTime - time_off, .3);

	}
}
