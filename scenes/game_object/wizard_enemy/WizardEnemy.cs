using Godot;
using System;

public partial class WizardEnemy : CharacterBody2D
{
	[Export]

	public Node2D visuals;//效果层（将精灵和其它与精灵绑定的效果放在此层下，操作此层便可以对所有精灵的所有效果生效）

	private VelocityComponent velocityComponent;//移动组件

	public override void _Ready()
	{

		visuals = GetNode<Node2D>("Visuals");
		velocityComponent = GetNode<VelocityComponent>("VelocityComponent");
		if (velocityComponent is null)
		{
			throw new Exception("基本敌人(BasicEnemy)没有绑定移动组件(VelocityComponent)!!!");
		} 
	}

	public override void _Process(double delta)
	{
		//移动敌人
		velocityComponent.AccelerateInPlayer();
		velocityComponent.Move(this);
		//根据移动反向调整敌人朝向
		float move_silde = MathF.Sign(velocityComponent.velocity.X);
		if (move_silde != 0)
		{
			GD.Print(move_silde);
			visuals.Scale = new Vector2(move_silde, 1);
		}
	}
}
