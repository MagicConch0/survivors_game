using Godot;
using System;

/// <summary>
/// 受伤控制组件
/// </summary>
public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent healthComponent;//导入血量控制组件

	public PackedScene floatingTextScene = ResourceLoader.Load<PackedScene>("res://scenes/UI/floating_text.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEnterd;//连接信号
	}


	/* 连接信号，当进入区域后触发 */
	private void OnAreaEnterd(Area2D area)
	{
		if (area is not HitboxComponent)
		{//确定检测到的区域为击中组件
			return;
		}
		if (healthComponent == null)
		{//检测受伤组件不为空
			return;
		}
		HitboxComponent hitboxComponent = area as HitboxComponent;
		healthComponent.Damage(hitboxComponent.damage);
		FloatingText floatingText = floatingTextScene.Instantiate<FloatingText>();
		GetTree().GetFirstNodeInGroup("Foreground_layer").AddChild(floatingText);

		floatingText.GlobalPosition = area.GlobalPosition + (Vector2.Up * 16);
		floatingText.start(hitboxComponent.damage.ToString());
	}



}
