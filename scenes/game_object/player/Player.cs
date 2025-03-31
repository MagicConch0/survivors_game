using System;
using Godot;


public partial class Player : CharacterBody2D
{

	[Export]
	public float SPEED = 125;//移动速度
	[Export]
	public float  ACCELERATION_SMOOTHING = 25;//加速度平滑度
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 target_speed = GetMoverInput() * SPEED;
		//使任务角色的速度逐渐接近目标速度，以此来实现平滑加速
		Velocity = Velocity.Lerp(target_speed,(float)(1.0 - Math.Exp(-delta * ACCELERATION_SMOOTHING)));
		MoveAndSlide();
	}

	/* 
		获取玩家移动输入
		返回一个二维向量，x表示水平方向的输入，y表示垂直方向的输入
		 */
	private Godot.Vector2 GetMoverInput(){
		var x = Input.GetActionStrength("右") - Input.GetActionStrength("左");
		var y = Input.GetActionStrength("下") - Input.GetActionStrength("上");
		return new Godot.Vector2(x,y).Normalized();//向量归一化
	}
}
 