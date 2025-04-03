using Godot;
using System;
/* UI界面：生成升级卡片，用于描述升级信息（技能名字，作用等） */
public partial class AbilityUpgradeCard : PanelContainer
{
	[Signal]
	public delegate void SelectedEventHandler();//声明左键单击选中信号


	public override void _Ready()
	{
		GuiInput += OnGuiInput;//连接鼠标点击信号

	}


	/* 当鼠标点击升级选项时触发 */
	private void OnGuiInput(InputEvent @event)
	{
		if(@event.IsActionPressed("鼠标左键")){//如果点击鼠标左键
			EmitSignal("Selected");//发射左键选中信号
		}
	}


	/* 设置升级卡片上显示的信息 */
	public void SetAbilityUpgrade(Ability_upgrade ability_Upgrade)
	{
		GetNode<Label>("%NameLabel").Text = ability_Upgrade.name;
		GetNode<Label>("%DescriptionLabel").Text = ability_Upgrade.description;
	}
}
