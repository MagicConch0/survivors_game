using Godot;
using System;

/* 
	经验瓶
 */
public partial class ExperienceVial : Node2D
{


	private GameEvents gameEvents;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area2D area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += OnAreaEntered;
		gameEvents = GetNode<GameEvents>("/root/GameEvents");//获取全局自动加载对象
	}


	/* 
		当进入区域
		 */
	private void OnAreaEntered(Area2D area)
	{
		if (gameEvents == null)
		{
			return;
		}
		gameEvents.EmitExperienceVialCollected(1f);//发射信号
		QueueFree();
	}


}
