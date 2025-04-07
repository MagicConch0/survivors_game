using Godot;
using System;
using System.Runtime.Serialization;
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
	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("Timer");
		//连接信号：将timer的Timeout信号连接到当前类的OnTimerTimeout方法
		timer.Timeout += OnTimerTimeout;
	}


	/* 
		定时器结束后实例化敌人
	 */
	 private void OnTimerTimeout()
    {
		Node2D player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if(player == null){
			return;
		}
		//设置一个生成范围，因为游戏窗口高度为640，宽为360，所以如果想要让敌人生成在玩笑视野外，则需要范围半径在320或者以上
		Vector2 range_derction = Vector2.Right.Rotated((float)GD.RandRange(0.0f ,(float)Math.Tau)) * SPAWN_RADIUS;
		Vector2 spawn_position = player.GlobalPosition + range_derction;
		//实例化敌人
        Node2D enemy =  enemyScene.Instantiate() as Node2D;
		enemy.GlobalPosition = spawn_position;//设置敌人位置

		//获取实体层，将敌人添加到实体层中，用y轴判断位置，y轴大的覆盖y轴小的
		Node2D entitys_layer = GetTree().GetFirstNodeInGroup("enities_layer") as Node2D;
		entitys_layer.AddChild(enemy);//添加到实体层节点
    }
}
