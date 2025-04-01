using Godot;
using System;
using System.Diagnostics.Tracing;
using System.Runtime.Serialization;

public partial class ExperienceManager : Node
{

	public float currentExperience = 0f;//当前经验

	public int currentLevel = 1;//当前等级
	public float targetExperience = 10;//升级所需经验

	public int TARGET_EXPERIENCE_GROWTH = 5;//每升一级最大经验增加5

	
	[Signal]
	public delegate void ExperienceUpdateEventHandler(float currentLevel,float targetExperience);//声明经验更新信号


	public override void _Ready()
	{
		GameEvents gameEvents = GetNode<GameEvents>("/root/GameEvents"); // 自定义信号类，此类已在项目中设置为全局自动加载
		gameEvents.ExperienceVialCollected += OnExperienceVialCollected;
	}

	/* 
		增加经验
	 */
	public void IncrementExperience(float experience)
	{
		//计算升级后溢出的经验，加入到下一级的经验量中
		float _experience = (currentExperience + experience) - targetExperience;
		//返回增加后的经验值，Min方法保证返回的值不会超过当前等级的最大经验值
		currentExperience = Math.Min(currentExperience + experience, targetExperience);

		EmitSignal("ExperienceUpdate",currentExperience,targetExperience);//发射经验更新信号
		if(currentExperience == targetExperience){//如果当前经验等于最大经验，则等级提升
			currentLevel +=1;//等级提升
			targetExperience += TARGET_EXPERIENCE_GROWTH;//提升经验上限
			currentExperience =_experience;//重置当前经验，如果升级后经验有剩余则并入下一级
			EmitSignal("ExperienceUpdate",currentExperience,targetExperience);//发射经验更新信号
		}
	}

	/* 当收集到经验后执行 */
	private void OnExperienceVialCollected(float experience)
	{

		IncrementExperience(experience);
		GD.Print("当前经验：" + currentExperience);

	}
}
