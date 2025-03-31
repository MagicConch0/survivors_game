using Godot;
using System;
using System.Diagnostics.Tracing;
using System.Runtime.Serialization;

public partial class ExperienceManager : Node
{

	[Export]
	public float currentExperience = 0f;//当前经验

	public override void _Ready()
	{
		GameEvents gameEvents = GetNode<GameEvents>("/root/GameEvents"); // 自定义信号类，此类已在项目中设置为全局自动加载
		gameEvents.ExperienceVialCollected += OnExperienceVialCollected1;
	}

	/* 
		增加经验
	 */
	public float IncrementExperience(float experience)
	{
		return currentExperience += experience;
	}

	/* 当收集到经验后执行 */
	private void OnExperienceVialCollected1(float experience)
	{
		IncrementExperience(experience);
		GD.Print("当前经验：" + currentExperience);
	}
}
