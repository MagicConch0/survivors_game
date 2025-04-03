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
	/* 当升级时触发 */
	private void OnLevelUp(int newLevel)
	{
		//升级时从能力池中随机选择一个，upgrade_pool.PickRandom()作用是从数组中随机选择一项返回，可能返回空值
		Ability_upgrade chosen_upgrade = upgrade_pool.PickRandom();
		if (chosen_upgrade == null)
		{
			return;
		}
		UpgradeScreen upgradeScreen_instance = upgradeScreen.Instantiate()  as UpgradeScreen;//实例化升级ui界面
		AddChild(upgradeScreen_instance);//添加到场景中
		upgradeScreen_instance.SetAbilityUpgrades([chosen_upgrade]);//添入可升级的技能，用于制作技能卡牌
		upgradeScreen_instance.SelelctAbility += OnSelectAbility;//连接选择技能卡片信号

		
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
	}

}
