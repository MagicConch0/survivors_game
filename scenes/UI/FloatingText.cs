using Godot;
using System;
using System.IO;

public partial class FloatingText : Node2D
{
	Label label ;
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
	}

	public void start(string text)
	{
		label.Text = text;
		Tween tween = CreateTween();
		tween.TweenProperty(this, "global_position", GlobalPosition + (Vector2.Up * 16), .3)
		.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic);
		tween.Parallel().TweenProperty(this,"scale",Vector2.One * 2,.15)
		.SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic);
		tween.TweenInterval(.15);//延迟0.15秒后执行
		tween.TweenProperty(this,"scale",Vector2.One,.15)
		.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Cubic);



		tween.Chain().TweenProperty(this,"global_position",GlobalPosition +  (Vector2.Up * 48),.4)
		.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Cubic);
		tween.Parallel().TweenProperty(this,"scale",Vector2.Zero,.4)
		.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Cubic);
		


		tween.Finished += QueueFree;
		
	}
}
