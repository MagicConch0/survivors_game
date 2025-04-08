using Godot;
using System;
/* 设置结算界面 */
public partial class EndScreen : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Paused = true;//暂停游戏
		GetNode<Button>("%RestartButton").Pressed += OnPressedRestartButtion;
		GetNode<Button>("%QuitButton").Pressed += OnPressedQuitButton;
	}

	//设置失败时显示的文本
	public void SetDefeat(){
		GetNode<Label>("%TitleLabel").Text = "死亡";
		GetNode<Label>("%DescriptionLabel").Text = "死亡";
	}


	/* 当按下重新开始按钮 */
	private void OnPressedRestartButtion()
	{
		GetTree().Paused = false;//继续游戏
		
		// GetTree().CallDeferred("change_scene_to_file","res://scenes/main/main.tscn");
		GetTree().ChangeSceneToFile("res://scenes/main/main.tscn");//重新加载主场景
	}
	/* 当按下退出按钮 */
	private void OnPressedQuitButton()
	{
		GetTree().Quit();//退出 
	}

}
