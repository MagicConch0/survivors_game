using Godot;
using System;
/* 用于生成升级时选择技能的UI界面 */
public partial class UpgradeScreen : CanvasLayer
{
	[Export]
	public PackedScene upgrade_card_scene;//导出升级卡片场景
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
		}
	}
}
