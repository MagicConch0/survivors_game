using Godot;
using System;

public partial class ArenaTimeManager : Node
{

	[Export]
	public Timer timer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		

	}
	/*
		获取计时器剩余时间
	 */
   public double GetTimer(){
	   return timer.WaitTime - timer.TimeLeft;
   }
}
