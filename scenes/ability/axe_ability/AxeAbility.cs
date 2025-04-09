using Godot;
using System;

public partial class AxeAbility : Node2D
{

	 public HitboxComponent hitboxComponent;

	private float MAXRADIUS = 100;//最大半径
	// Called when the node enters the scene tree for the first time.

	private Vector2 baseRotation = Vector2.Right;//设置一个基础随机旋转
	public override void _Ready()
	{
		baseRotation = baseRotation.Rotated((float)GD.RandRange(0.0,Math.Tau));//设置一个随机角度，让斧头在随机角度生成
		hitboxComponent = GetNode<HitboxComponent>("HitboxComponent");

		//制作动画
		Tween tween = CreateTween();//用代码控制对象动画的工具
		//3秒内不断调用TweenMethod(float rotations)方法，
		//rotations在3秒内逐渐由0.0增长至2.0
		//注意参数必须是浮点数，如果参数是0-2 ，那么增长时是以整数为周期0 1 2，不够平滑
		tween.TweenMethod(Callable.From<float>(TweenMethod),0.0,2.0,3);//设置斧头旋转，从0-2旋转2圈，用时3秒
		tween.Finished += QueueFree;//动画完成后销毁
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	/// <summary>
	/// Tween动画，0.0-2.0作为旋转圈数传入方法，在3秒内完成斧头围绕玩家旋转3圈的动画效果
	/// </summary>
	/// <param name="rotations">旋转的圈数</param>
	private void TweenMethod(float rotations){
        float percent = rotations / 2;
		float current_radius = percent * MAXRADIUS;//获得旋转半径
		
        Vector2 current_direciton = baseRotation.Rotated(rotations * float.Tau);//获得旋转角度

		Player player = GetTree().GetFirstNodeInGroup("player") as Player;
		if(player == null){
			 return;
		}
		//current_direciton是一个单位为1的向量
		//current_direciton * current_radius将单位向量拉长到半径的长度
		//player.GlobalPosition + (current_direciton * current_radius) 让计算出的长度加上圆心的位置
		GlobalPosition = player.GlobalPosition + (current_direciton * current_radius);//起始位置+半径*角度

	}
}
