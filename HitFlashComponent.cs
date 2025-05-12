using Godot;
using System;
/// <summary>
/// 受伤闪烁组件
/// </summary>
public partial class HitFlashComponent : Node
{

	[Export]
	public HealthComponent healthComponent ;

	[Export]
	public Sprite2D sprite2D;
	[Export]
	ShaderMaterial hitFlashMaterial ;//闪烁材质

	Tween hitFlashTween;
	public override void _Ready()
	{
		if(healthComponent is null ){
			throw new Exception("受伤闪烁组件(HitFlashComponent)缺少健康组件(HealthComponent)引用");
		}

		if(sprite2D is null ){
			throw new Exception("受伤闪烁组件(HitFlashComponent)缺少精灵(sprite2D)引用");
		}
		healthComponent.HelathChanged += OnHelathChanged;
		sprite2D.Material = hitFlashMaterial;

	}


	/// <summary>
	/// 被击中时，闪烁白色
	/// </summary>
    private void OnHelathChanged()
    {
		//检查当前是否正在播放动画，如果正在播放，则删除
		if(hitFlashTween is not null && hitFlashTween.IsValid()){
			hitFlashTween.Kill();
		}
		(sprite2D.Material as ShaderMaterial).SetShaderParameter("lerp_percent", 1.0);//设置自定义着色器参数为1
		hitFlashTween =CreateTween();
		hitFlashTween.TweenProperty(sprite2D.Material,"shader_parameter/lerp_percent",0.0,.3)
		.SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Cubic);
		
    }

}
