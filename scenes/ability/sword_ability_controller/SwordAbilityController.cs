using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SwordAbilityController : Node
{


	[Export]
	public PackedScene sword_ability;//剑技能资源

	[Export]
	public float max_range = 150;//最大触发范围
								 // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("Timer");
		//连接信号：将timer的Timeout信号连接到当前类的OnTimerTimeout方法
		timer.Timeout += OnTimerTimeout;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnTimerTimeout()
	{
		//获取玩家节点
		Node2D player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null)
		{
			return;
		}
		//获取玩家位置
		Vector2 player_position = player.GlobalPosition;

		Array<Node> enemies = GetTree().GetNodesInGroup("enemy");
		if (enemies.Count == 0)
		{
			return;
		}
		//遍历敌人\,如果敌人与玩家的距离小于最大触发范围，则添加到数组中
		Array<Node2D> enemies2D = new Array<Node2D>();
		foreach (Node2D enemy in enemies)
		{
			if (enemy.GlobalPosition.DistanceSquaredTo(player_position) < Math.Pow(max_range, 2))
			{
				enemies2D.Add(enemy);
			}
		}
		if (enemies2D.Count == 0)
		{
			return;
		}
		//对敌人进行排序，按照敌人与玩家的距离进行排序
		List<Node2D> node2Ds = enemies2D.ToList();
		node2Ds.Sort((a, b) =>
		{
			return a.GlobalPosition.DistanceSquaredTo(player_position).CompareTo(b.GlobalPosition.DistanceSquaredTo(player_position));
		});
		enemies2D = new Array<Node2D>(node2Ds);


		//获取玩家的父节点（主节点）
		Node main = player.GetParent();
		//实例化剑技能
		var sword_instance = sword_ability.Instantiate() as Node2D;
		//设置剑技能的位置
		sword_instance.GlobalPosition = enemies2D[0].GlobalPosition;
		sword_instance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0.0,Math.Tau)) * 5f;//设置一个随机半径是10的圆形随机范围
		//设置剑技能的旋转角度
		Vector2 vector2 = enemies2D[0].GlobalPosition - sword_instance.GlobalPosition;
		sword_instance.GlobalRotation = vector2.Angle();
		//将剑技能添加到主节点
		main.AddChild(sword_instance);
	}

}
