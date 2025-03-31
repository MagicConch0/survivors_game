using Godot;
using System;

public partial class GameEvents : Node
{


	/* 
		自定义收集经验信号
	 */
	[Signal]
	public delegate void ExperienceVialCollectedEventHandler(float experience);

	/* 发射信号 */
	public void EmitExperienceVialCollected(float experience)
	{
		//发射信号
		EmitSignal("ExperienceVialCollected", experience);
	}
}
