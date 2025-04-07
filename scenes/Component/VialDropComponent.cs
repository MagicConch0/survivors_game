using Godot;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
/* 经验瓶生成组件 */
public partial class VialDropComponent : Node
{

	[Export]
	public PackedScene experienceVial;//暴露经验瓶场景
	[Export]
	public HealthComponent healthComponent;//暴露生命组件
	[Export(PropertyHint.Range, "0,1,0.01,hint_string(概率:0=不可能,1=必然)")]
	public float drop_percent = .5f;//设置掉率概率，默认50%
	public override void _Ready()
	{
		if (healthComponent == null)
		{
			throw new Exception("healthComponent组件为空,可能未正确绑定");
		}
		healthComponent.died += Ondied;//绑定死亡信号
	}



	/* 死亡时调用，执行生成经验瓶 */
	private void Ondied()
	{
		if (experienceVial == null)
			return;

		if(GD.Randf()>drop_percent){//控制生成概率
			return;
		}
		Node2D vial_instance = experienceVial.Instantiate() as Node2D;
		vial_instance.GlobalPosition = (Owner as Node2D).GlobalPosition;


		//获取实体层，将敌人添加到实体层中，用y轴判断位置，y轴大的覆盖y轴小的
		Node2D entitys_layer = GetTree().GetFirstNodeInGroup("enities_layer") as Node2D;
		entitys_layer.CallDeferred("add_child", vial_instance);//CallDeferred:延迟调用，保证在Owner节点销毁后再生成
	}

}
