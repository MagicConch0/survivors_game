using Godot;
using System;

public partial class VictoryScreen : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Paused = true;//暂停游戏
		GetNode<Button>("%RestartButton").Pressed += OnPressedRestartButtion;
		GetNode<Button>("%QuitButton").Pressed += OnPressedQuitButton;
	}

	/* 当按下重新开始按钮 */
	private void OnPressedRestartButtion()
	{
		GetTree().Paused = false;//继续游戏
		GetTree().ChangeSceneToFile("res://scenes/main/main.tscn");//重新加载主场景
	}
	/* 当按下退出按钮 */
	private void OnPressedQuitButton()
	{
		GetTree().Quit();//退出 
	}

}
