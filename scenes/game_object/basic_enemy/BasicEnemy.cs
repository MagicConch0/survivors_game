using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D
{
	[Export]
	public float speed1 = 100;//移送速度

	HealthComponent healthComponent;//生命组件，负责控制角色的生命值与死亡动作

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area2D area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += OnAreaEntered;
		healthComponent = GetNode<HealthComponent>("HealthComponent");
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var player_position = Get_directory_to_player();
		Velocity = (player_position - GlobalPosition).Normalized() * speed1;
		MoveAndSlide();
	}

	/* 
		 获取玩家位置
	 */
	private Vector2 Get_directory_to_player()
	{
		var player_nodes = GetTree().GetNodesInGroup("player");
		Vector2 player_position = new Vector2();
		if (player_nodes.Count > 0)
		{
			var player = player_nodes[0] as Player;
			player_position = player.GlobalPosition;
		}
		return player_position;
	}


	private void OnAreaEntered(Area2D area)
	{
		if(healthComponent == null){
			return ;
		}
		healthComponent.Damage(100);
		
	}

}





