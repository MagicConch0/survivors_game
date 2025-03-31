using Godot;
using System;
using System.Runtime.Serialization;

public partial class ArenaTimeUi : CanvasLayer
{
	[Export]
	public ArenaTimeManager node;

	[Export]
	public Label label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(node == null){
			return;

		}

		if(label == null){
			return;
		}


		var timer = node.GetTimer();
		label.Text =ToMmSs(timer);
	}
	public  string ToMmSs( double totalSeconds)
    {
        int total = (int)Math.Floor(totalSeconds);
        return $"{total / 60:D2}:{total % 60:D2}";
    }
}
