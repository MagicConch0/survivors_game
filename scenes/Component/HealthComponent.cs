using Godot;
using System;

public partial class HealthComponent : Node
{

	[Export]
	public float maxHealth = 100; //最大血量

	[Signal]
	public delegate void diedEventHandler();//定义死亡信号

	[Signal]
	public delegate void HelathChangedEventHandler();//声明生命值改变信号

	public float currentHealth;//当前血量

	public override void _Ready()
	{
		currentHealth = maxHealth;
	}



	/* 定义收到伤害后要执行的动作 */
	public void Damage(float damage)
	{
		currentHealth = Math.Max(currentHealth - damage, 0);//受到伤害后，当前生命值减去伤害，且限制最低生命值为0
		EmitSignal("HelathChanged");//发射生命值改变信号
		if (currentHealth == 0)
		{//如果生命值等于0，发出死亡信号，随即消除自身的父节点
			EmitSignal("died");
			Owner.QueueFree();//Owner表示当前节点的"拥有者"节点。它建立了节点之间的父子关系之外的另一种重要关联。
		}
	}

}
