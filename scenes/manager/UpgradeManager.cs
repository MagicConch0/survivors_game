using Godot;
using Godot.Collections;
using System;
/* 升级管理器 */
public partial class UpgradeManager : Node
{

	[Export]
	public Array<Ability_upgrade> upgrade_pool = [];//能力升级池

	[Export]
	public ExperienceManager experienceManager;//导出经验管理器

	[Export]
	private PackedScene upgradeScreen;//导出升级时显示的ui场景


	private Dictionary<string, Dictionary> current_upgrade = new();//定义字典：当前升级


	public override void _Ready()
	{
		if (experienceManager == null)
		{
			return;
		}
		experienceManager.LevelUp += OnLevelUp;

	}
	/// <summary>
	/// 根据界面中要显示的卡牌数量，制造选项
	/// </summary>
	/// <returns></returns>
	private Array<Ability_upgrade> PickUpgrades()
	{
		Array<Ability_upgrade> chosenUpgrades = [];
		Array<Ability_upgrade> filteredUpgrades = upgrade_pool.Duplicate();//复制当前升级池
		for (int i = 0; i < 2; i++)//界面显示俩张选择卡片，如果选择三张则循环3次
		{
			//升级时从能力池中随机选择一个，upgrade_pool.PickRandom()作用是从数组中随机选择一项返回，可能返回空值
			Ability_upgrade chosen_upgrade = filteredUpgrades.PickRandom();
			chosenUpgrades.Add(chosen_upgrade);
			filteredUpgrades.Remove(chosen_upgrade);
		}
		return chosenUpgrades;

	}


	/* 选择要升级的技能卡片后执行 */
	private void OnSelectAbility(Ability_upgrade ability_Upgrade)
	{
		ApplyUpgrade(ability_Upgrade);//应用升级项

	}

	/* 应用升级项 */
	private void ApplyUpgrade(Ability_upgrade ability_Upgrade)
	{

		var has_upgrade = current_upgrade.ContainsKey(ability_Upgrade.id);//查看是否已经升级过
		if (!has_upgrade)
		{//如果没有升级过，这添加该升级项
			current_upgrade[ability_Upgrade.id] = new Dictionary(){
				{"resource",ability_Upgrade},//技能
				{"quantity",1}//升级计数
			};
		}
		else
		{//如果升级过
			Dictionary upgradeData = current_upgrade[ability_Upgrade.id];
			upgradeData["quantity"] = (int)upgradeData["quantity"] + 1;//升级计数加1
		}
		GameEvents.Instance.EmitAbilityUpgradeAdded(ability_Upgrade, current_upgrade);//发射升级技能信号
	}


	/// <summary>
	/// 当升级时触发
	/// </summary>
	/// <param name="newLevel">当前等级</param>
	private void OnLevelUp(int newLevel)
	{
		
		UpgradeScreen upgradeScreen_instance = upgradeScreen.Instantiate() as UpgradeScreen;//实例化升级ui界面
		AddChild(upgradeScreen_instance);//添加到场景中
        Array<Ability_upgrade> chosenUpgrades = PickUpgrades();
		upgradeScreen_instance.SetAbilityUpgrades(chosenUpgrades);//添入可升级的技能，用于制作技能卡牌
		upgradeScreen_instance.SelelctAbility += OnSelectAbility;//连接选择技能卡片信号


	}

}
