using Godot;
using System;
using System.ComponentModel;

public partial class SwordAbility : Node2D
{

	private HitboxComponent _hitboxComponent;//声明一个私有字段用于存储节点引用
	public HitboxComponent HitboxComponent => _hitboxComponent ??= GetNode<HitboxComponent>("HitboxComponent");//获取伤害组件
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
