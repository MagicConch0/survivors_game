using Godot;
using System;
/* 游戏时间 */
public partial class ArenaTimeManager : Node
{

	[Export]
	public Timer timer;

	[Export]
	public PackedScene EndScreenScene;

	[Signal]
	public delegate void ArenaDifficultyIncreasedEventHandler(int arenaDifficulty);//定义难度升级信号

	private float DIFFICULTY_INTERVAL = 5;//规定难度升级时间
	private int arenaDifficulty = 0;//当前游戏难度，每过5秒（DIFFICULTY_INTERVAL）增加一次难度

	private double previousTime = 0;//上一帧时间

	private double nextTimeTarget = 0;//下一次难度升级时间

	public override void _Ready()
	{
		previousTime = timer.WaitTime;
		timer.Timeout += OnTimeOut;
		if (EndScreenScene == null)
		{
			return;
		}

	}

	public override void _Process(double delta)
	{
		nextTimeTarget = timer.WaitTime - ((arenaDifficulty + 1) * DIFFICULTY_INTERVAL);
		if (timer.TimeLeft <= nextTimeTarget)
		{
			arenaDifficulty ++;
			EmitSignal("ArenaDifficultyIncreased",arenaDifficulty);//发射难度升级信号
		}
	}




	/*
		获取计时器剩余时间	
	 */
	public double GetTimer()
	{
		return timer.WaitTime - timer.TimeLeft;
	}

	/* 当游戏时间结束后显示胜利界面 */
	private void OnTimeOut()
	{
		Node victoryScreen = EndScreenScene.Instantiate();
		AddChild(victoryScreen);
	}
}
