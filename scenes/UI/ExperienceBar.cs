using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class ExperienceBar : CanvasLayer
{

	[Export]
	public ExperienceManager experienceManager;//导出经验管理器
	ProgressBar progressBar;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (experienceManager == null)
		{
			return;
		}
		experienceManager.ExperienceUpdate += OnExperienceUpdate;
		progressBar = GetNode<ProgressBar>("MarginContainer/ProgressBar");
		if(progressBar == null ){
			return;
		}
		progressBar.MinValue = 0;//初始化经验条的值
	}

	/* 当经验变化时，控制经验条同步变化 */
	private void OnExperienceUpdate(float currentLevel, float targetExperience)
	{
		progressBar.MaxValue = targetExperience;
		progressBar.Value = currentLevel;
	}
}
