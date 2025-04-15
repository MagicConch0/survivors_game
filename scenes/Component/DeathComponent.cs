using Godot;
using System;

public partial class DeathComponent : Node2D
{

	[Export]
	public HealthComponent healthComponent;//健康组件

	[Export]
	public Sprite2D sprite2D;//播放死亡动画时的纹理

	public override void _Ready()
	{
		if(healthComponent == null || sprite2D == null){
			throw new Exception("死亡组件(DeathComponent)缺少对健康组件(HealthComponent)或者精灵(Sprite2D)的引用！！！！");
		}
		healthComponent.died += OnDied;
		GetNode<GpuParticles2D>("GPUParticles2D").Texture = sprite2D.Texture;
	}
	/// <summary>
	/// 用于绑定此组件的节点死亡时播放死亡动画
	/// </summary>
	private void OnDied()
	{
		Node enities_layer = GetTree().GetFirstNodeInGroup("enities_layer");//获取实体层
		this.Reparent(enities_layer);//将当前节点的父节点切换为enities_layer
		GetNode<AnimationPlayer>("AnimationPlayer").Play("default");
	}
}
