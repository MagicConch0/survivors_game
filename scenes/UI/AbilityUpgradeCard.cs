using Godot;
using System;
/* UI界面：生成升级卡片，用于描述升级信息（技能名字，作用等） */
public partial class AbilityUpgradeCard : PanelContainer
{
/* 设置升级卡片上显示的信息 */
  public void SetAbilityUpgrade(Ability_upgrade ability_Upgrade){
	GetNode<Label>("%NameLabel").Text = ability_Upgrade.name;
	GetNode<Label>("%DescriptionLabel").Text = ability_Upgrade.description;
  }
}
