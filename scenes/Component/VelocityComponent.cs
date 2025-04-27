using Godot;
using System;
using System.Net.NetworkInformation;
/// <summary>
/// 控制移动组件，使当前角色朝向玩家移动，期间平滑加速，逐渐达到当前角色的最大移动速度
/// </summary>
public partial class VelocityComponent : Node
{

	[Export]
	public float maxSpeed = 40;//最大速度

	[Export]
	public float acceleration = 5;//加速度

	public Vector2 velocity = Vector2.Zero;


	/// <summary>
	/// 向玩家的方向移动，期间会平滑加速到最大移动速度
	/// </summary>
	public void AccelerateInPlayer()
	{
		Node2D ownerNode2D = Owner as Node2D;
		if (ownerNode2D is null)
		{
			return;
		}
		Player player = GetTree().GetFirstNodeInGroup("player") as Player;
		if (player is null)
		{
			return;
		}
		Vector2 direction = (player.GlobalPosition - ownerNode2D.GlobalPosition).Normalized();//获取指向玩家方向的单位向量
		AccelerateIndirection(direction);

	}
	/// <summary>
	/// 停止移动，移动速度逐渐减速到0
	/// </summary>
	public  void Decelerate(){
		AccelerateIndirection(Vector2.Zero);
	}



	/// <summary>
	/// 平滑加速，逐渐达到最大移动速度
	/// </summary>
	/// <param name="direction">移动方向单位向量</param>
	public void AccelerateIndirection(Vector2 direction)
	{
		Vector2 desiredVelocity = direction * maxSpeed;//期望速度
		velocity = velocity.Lerp(desiredVelocity, 1 - Mathf.Exp(-acceleration * (float)(GetProcessDeltaTime())));

	}

	/// <summary>
	/// 移动
	/// </summary>
	/// <param name="characterBody2D">移动的节点</param>
	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = velocity;
		characterBody2D.MoveAndSlide();
		velocity = characterBody2D.Velocity;
	}
}
