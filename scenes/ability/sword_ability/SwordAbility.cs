using Godot;
using System;
using System.ComponentModel;

public partial class SwordAbility : Node2D
{

	private HitboxComponent _hitboxComponent;//声明一个私有字段用于存储节点引用
	public HitboxComponent HitboxComponent => _hitboxComponent ??= GetNode<HitboxComponent>("HitboxComponent");//获取伤害组件
	

	
}
