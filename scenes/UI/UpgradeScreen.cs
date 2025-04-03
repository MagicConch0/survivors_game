using Godot;
using System;
/* 用于生成升级时选择技能的UI界面 */
public partial class UpgradeScreen : CanvasLayer
{
	[Export]
	public PackedScene upgrade_card_scene;//导出升级卡片场景

	[Signal]
	public delegate void SelelctAbilityEventHandler(Ability_upgrade ability_Upgrade);
	private HBoxContainer CardContainer; //获取升级卡片容器
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CardContainer = GetNode<HBoxContainer>("%CardContainer");
		GetTree().Paused = true;//升级时暂停游戏


	}

	/* 用于生成升级卡牌选项，传入几个技能，生成对应的技能卡牌 */
	public void SetAbilityUpgrades(Ability_upgrade[] ability_Upgrades)
	{
		foreach (Ability_upgrade ability_Upgrade in ability_Upgrades)
		{
			AbilityUpgradeCard upgradeCard_instantiate = upgrade_card_scene.Instantiate() as AbilityUpgradeCard;//示例化卡牌
			CardContainer.AddChild(upgradeCard_instantiate);//添加到ui中
			upgradeCard_instantiate.SetAbilityUpgrade(ability_Upgrade);//给卡牌添加信息
			upgradeCard_instantiate.Selected += () => OnSelected(ability_Upgrade);//连接选中信号,selected信号本身没有提任何参数，如果我们想要带参则需要用参数绑定方式
		}
	}

	/* 当此技能卡牌被选中时触发,发射选中信号，
	信号嵌套信号是为了解耦，技能卡片（AbilityUpgradeCard）发射selected信号是为了告诉升级场景（UpgradeScreen）这个卡片被选中了，
	升级场景（UpgradeScreen）发送onselected信号是为了告诉升级管理器（UpgradeManager）选中了那个卡片，
	以此方式实现不同功能只负责自己的工作 */
	private void OnSelected(Ability_upgrade ability_Upgrade)
	{
		EmitSignal("SelelctAbility",ability_Upgrade);
		GetTree().Paused = false;//选择要升级的技能卡片后，继续游戏
		QueueFree();//随后释放升级场景
	}

}
