using Godot;
using System;

public partial class AxeAbilityController : Node
{

	[Export]
	PackedScene axeBility;//导入斧头

	public float damage = 10; //伤害

	Player player;

	Timer timer;
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimeOut;

		player = GetTree().GetFirstNodeInGroup("player") as Player;
		if (axeBility == null)
		{
			throw new Exception("斧头技能控制器里缺少斧头技能的引用！！！！");
		}

	}

	/// <summary>
	/// 在玩家的位置生成斧头
	/// </summary>
	private void OnTimeOut()
	{


		AxeAbility axeInstant = axeBility.Instantiate() as AxeAbility;
		if (player == null)
		{
			return;
		}
		Vector2 playerGlobalPositon = player.GlobalPosition;//获取玩家位置
		Node2D foregroundLayer = GetTree().GetFirstNodeInGroup("Foreground_layer") as Node2D;//获取前景层

		foregroundLayer.AddChild(axeInstant);

		axeInstant.GlobalPosition = playerGlobalPositon;
		axeInstant.hitboxComponent.damage = damage;

	}
}
