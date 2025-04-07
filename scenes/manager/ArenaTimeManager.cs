using Godot;
using System;
/* 游戏时间 */
public partial class ArenaTimeManager : Node
{

	[Export]
	public Timer timer;

	[Export]
	public PackedScene VictoryScreenScene;

    public override void _Ready()
    {

		timer.Timeout += OnTimeOut;
		if(VictoryScreenScene == null ){
			return;
		}
        
    }


    /*
		获取计时器剩余时间	
	 */
    public double GetTimer(){
	   return timer.WaitTime - timer.TimeLeft;
   }

   /* 当游戏时间结束后显示胜利界面 */
    private void OnTimeOut()
    {
        Node victoryScreen = VictoryScreenScene.Instantiate();
		AddChild(victoryScreen);
    }
}
