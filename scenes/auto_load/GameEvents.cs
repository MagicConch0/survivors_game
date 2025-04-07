using Godot;
using Godot.Collections;
/* 此类为全局加载类，需项目设置中设置全局加载 */
public partial class GameEvents : Node
{
	/* 设置实例化方法，用于C#脚本在其它地方调用 */
	public static GameEvents Instance { get; private set; }

	/* 
		自定义收集经验信号
	 */
	[Signal]
	public delegate void ExperienceVialCollectedEventHandler(float experience);

	/* 
	声明能力升级信号 */
	[Signal]
	public delegate void AbilityUpgradeAddedEventHandler(Ability_upgrade upgrade, Dictionary<string, Dictionary> currentAbility);

	/*  初始化静态引用 */
	public override void _Ready()
	{
		Instance = this;//初始化静态引用
	}



	/* 发射收集经验信号 */
	public void EmitExperienceVialCollected(float experience)
	{
		//发射信号
		EmitSignal("ExperienceVialCollected", experience);
	}

	/* 发射能力升级信号 */
	public void EmitAbilityUpgradeAdded(Ability_upgrade upgrade, Dictionary<string, Dictionary> currentAbility)
	{
		EmitSignal("AbilityUpgradeAdded", upgrade, currentAbility);
	}
}
