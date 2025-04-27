using Godot;
using System;
using System.ComponentModel;

public partial class WizardEnemy : CharacterBody2D
{
	[Export]

	public Node2D visuals;//效果层（将精灵和其它与精灵绑定的效果放在此层下，操作此层便可以对所有精灵的所有效果生效）

	private VelocityComponent velocityComponent;//移动组件

	private AnimationPlayer animationPlayer;

	private bool isMoving = false;//控制是否移动
	public override void _Ready()
	{

		visuals = GetNode<Node2D>("Visuals");
		velocityComponent = GetNode<VelocityComponent>("VelocityComponent");
		if (velocityComponent is null)
		{
			throw new Exception("基本敌人(BasicEnemy)没有绑定移动组件(VelocityComponent)!!!");
		}

		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		if (animationPlayer is null)
		{
			throw new Exception("基本敌人(BasicEnemy)没有绑定移动组件(AnimationPlayer)!!!");
		}
	}

	public override void _Process(double delta)
	{
		if (isMoving)
		{
			//移动敌人
			velocityComponent.AccelerateInPlayer();
		}
		else
		{
			velocityComponent.Decelerate();
		}
		velocityComponent.Move(this);

		//根据移动反向调整敌人朝向
		float move_silde = MathF.Sign(velocityComponent.velocity.X);
		if (move_silde != 0)
		{
			GD.Print(move_silde);
			visuals.Scale = new Vector2(move_silde, 1);
		}

	}
	/// <summary>
	/// 控制当前角色是否移动
	/// </summary>
	/// <param name="moving">是/否</param>
	public void SetIsMoving(bool moving)
	{
		isMoving = moving;
	}
}
