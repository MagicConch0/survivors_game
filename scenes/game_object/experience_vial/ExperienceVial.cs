using Godot;
using System;
using System.IO;

/// <summary>
/// 经验瓶
/// </summary>
public partial class ExperienceVial : Node2D
{



	//获取碰撞，因为经验瓶飞向玩家的动画，可能使经验瓶远离玩家再靠近，导致先离开碰撞范围在进入，会多次触发进入碰撞范围方法内的代码，
	// 因此需要在玩家第一次进入经验瓶碰撞范围时，关闭碰撞范围的检测
	private CollisionShape2D collisionShape2D;//碰撞

	public override void _Ready()
	{


		Area2D area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += OnAreaEntered;
		collisionShape2D = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");

	}


	/* 
		当进入区域
		 */
	private void OnAreaEntered(Area2D area)
	{
		collisionShape2D.SetDeferred("disabled", true);//禁用碰撞属性

		Tween tween = CreateTween();
		tween.SetParallel();//让以下多个动画同时运行
		tween.TweenMethod(Callable.From((float percent) =>
		{
			TweenCollect(percent, GlobalPosition);
		}), 0.0f, 1.0f, 0.5f)
		.SetEase(Tween.EaseType.In)
		.SetTrans(Tween.TransitionType.Back);//调整飞向玩家的动画

		tween.TweenProperty(this,"scale",Vector2.Zero,.05).SetDelay(.45);//调整缩放

		tween.Chain();//在上面动画执行结束后再执行下面的动画

		tween.TweenCallback(Callable.From(() => collect()));//拾取动画完成后，发射增加经验新信号

	}

	/// <summary>
	/// 动画回调函数，用于当玩家拾起经验瓶，经验瓶会飞向玩家
	/// </summary>
	/// <param name="percent">百分比</param>
	/// <param name="startPosition">起始位置</param>
	private void TweenCollect(float percent, Vector2 startPosition)
	{
		Player player = GetTree().GetFirstNodeInGroup("player") as Player;
		if (player is null)
		{
			return;
		}
		GlobalPosition = startPosition.Lerp(player.GlobalPosition, percent);
		Vector2 directionFromPlayer = (player.GlobalPosition - startPosition).Normalized();
		float target_direction = directionFromPlayer.Angle() + Mathf.DegToRad(90);
		this.Rotation = Mathf.LerpAngle(Rotation, target_direction, (float)(1 - Mathf.Exp(-2 * GetProcessDeltaTime())));

	}
	/// <summary>
	/// 当动画飞向玩家动画播放完后，发射经验收集信号，并且销毁经验瓶自身
	/// </summary>
	private void collect()
	{
		GameEvents.Instance.EmitExperienceVialCollected(1f);
		QueueFree();
	}

}
