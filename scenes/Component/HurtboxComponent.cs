using Godot;
using System;

/* 受伤控制组件 */
public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent healthComponent;//导入血量控制组件

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEnterd;//连接信号
	}


    // Called every frame. 'delta' is the elapsed time since the previous frame.


    public override void _Process(double delta)
	{
	}
	

	/* 连接信号，当进入区域后触发 */
    private void OnAreaEnterd(Area2D area)
    {
		if (area is not HitboxComponent){//确定检测到的区域为击中组件
			return;
		}
		if (healthComponent == null){//检测受伤组件不为空
			return;
		}
        HitboxComponent hitboxComponent = area as HitboxComponent;
        healthComponent.Damage(hitboxComponent.damage);
    }

	

}
