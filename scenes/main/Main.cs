using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene endScreen;//导出结束界面
	public override void _Ready()
	{
		// playerHealthComponent = ;//获取玩家健康组件
		GetNode<Player>("%Player").healthComponent.died += OnDied;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	/* 玩家死亡时执行，显示死亡界面 */
	private void OnDied()
	{
		EndScreen endScreenInstancd = endScreen.Instantiate<EndScreen>();//实例化结束场景
		AddChild(endScreenInstancd);//添加到节点中
		endScreenInstancd.SetDefeat();//设置为失败场景
	}

}
