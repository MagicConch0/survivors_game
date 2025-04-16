using Godot;
using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

public partial class BasicEnemy : CharacterBody2D
{
	[Export]
	public float speed = 50;//移动速度

	public Node2D visuals;

    public override void _Ready()
    {

		visuals = GetNode<Node2D>("Visuals");
    }

	public override void _Process(double delta)
	{
		var player_position = Get_directory_to_player();
		Velocity = (player_position - GlobalPosition).Normalized() * speed;
		MoveAndSlide();
		//根据移动反向调整敌人朝向
		float move_silde = MathF.Sign(Velocity.X);
		if (move_silde != 0)
		{
			GD.Print(move_silde);
			visuals.Scale =new Vector2(-move_silde,1);
		}
	}

	/// <summary>
	/// 获取玩家位置
	/// </summary>
	/// <returns>记录玩家位置的Vector2向量</returns>
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





