using Godot;
using System;

public partial class GameCamera : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//使用当前相机
		MakeCurrent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var player_nodes =  GetTree().GetNodesInGroup("player");
		if (player_nodes.Count > 0)
		{
			var player = player_nodes[0] as Player;
			GlobalPosition  =  player.GlobalPosition;
		}
	}
}
