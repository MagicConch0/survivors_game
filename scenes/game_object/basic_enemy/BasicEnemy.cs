using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D
{
	[Export]
	public float speed1 = 50;//移动速度

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




}





