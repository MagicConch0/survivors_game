using Godot;
using System;
/* 击中控制组件 */
public partial class HitboxComponent : Area2D
{


	public float damage;//伤害量
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
