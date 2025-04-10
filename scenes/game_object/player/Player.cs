using System;
using System.ComponentModel.Design;
using Godot;
using Godot.Collections;


public partial class Player : CharacterBody2D
{

	[Export]
	public float SPEED = 125;//移动速度
	[Export]
	public float ACCELERATION_SMOOTHING = 25;//加速度平滑度

	[Export]
	public Node abilitys;//引用能力组件

	[Export]
	public AnimationPlayer animationPlayer;//动画组件
	[Export]
	public Node2D visuals;//视觉组件，玩家精灵节点的父级，用于控制玩家左右翻转
	public HealthComponent healthComponent;//获取健康组件

	private Timer damageIntervalTimer;//用于控制可受到伤害的间隔

	private ProgressBar hp;//获取血条
	private int number_colliding_bodies = 0;//与玩家碰撞的敌人数量
	public override void _Ready()
	{
		healthComponent = GetNode<HealthComponent>("HealthComponent");//初始化健康组件
		damageIntervalTimer = GetNode<Timer>("DamageIntervalTimer");//初始化伤害间隔计时器
		(GetNode("CollisionArea2D") as Area2D).BodyEntered += OnBodyEntered;//监听是否有敌人进入玩家碰撞范围
		(GetNode("CollisionArea2D") as Area2D).BodyExited += OnBOdyExited;//监听是否有敌人进入玩家碰撞范围
		if (damageIntervalTimer != null)
		{
			damageIntervalTimer.Timeout += OnDamageIntervalTimerTimerOut;//当伤害间隔超时时，检测是否要收到伤害
		}
		hp = GetNode<ProgressBar>("HP");//初始化血条
		hp.MaxValue = healthComponent.maxHealth;//初始化最大血量
		hp.Value = healthComponent.currentHealth;//初始化当前血量
		healthComponent.HelathChanged += OnHelathChange;//连接血量改变事件
		GameEvents.Instance.AbilityUpgradeAdded += OnAbilityUpgradeAdded;
		if (animationPlayer == null)
		{
			throw new Exception("玩家(Player)缺少一个动画组件(AnimationPlayer)的引用");
		}
		if (visuals == null)
		{
			throw new Exception("玩家(Player)缺少一个视觉组件(Visuals)的引用");
		}

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.


	public override void _Process(double delta)
	{
		Vector2 target_speed = GetMoverInput() * SPEED;
		//使任务角色的速度逐渐接近目标速度，以此来实现平滑加速
		Velocity = Velocity.Lerp(target_speed, (float)(1.0 - Math.Exp(-delta * ACCELERATION_SMOOTHING)));
		MoveAndSlide();

	}

	/* 
		获取玩家移动输入
		返回一个二维向量，x表示水平方向的输入，y表示垂直方向的输入
		 */
	private Godot.Vector2 GetMoverInput()
	{
		var x = Input.GetActionStrength("右") - Input.GetActionStrength("左");
		var y = Input.GetActionStrength("下") - Input.GetActionStrength("上");
		if (x != 0 || y != 0)
		{
			if (x != 0)
			{
				visuals.Scale = new Vector2(Math.Sign(x), 1);//控制左右翻转，sing（x），如果x大于0会返回1，小于0会返回-1
			}
			animationPlayer.Play("walk");
		}
		else
		{
			animationPlayer.Play("RESET");
		}
		return new Godot.Vector2(x, y).Normalized();//向量归一化
	}


	/* 检测是否收到伤害	 */
	private void CheckDealDamage()
	{
		if (number_colliding_bodies == 0 || !damageIntervalTimer.IsStopped())//如果没有与敌人碰撞，或者在伤害间隔内，则不受到伤害
		{
			return;
		}
		healthComponent.Damage(1f);//受到伤害
		damageIntervalTimer.Start();//开启受伤间隔计时器，0.5秒内不能重复受伤
	}



	/* 用于检测敌人是否进入玩家的碰撞范围,进入时与玩家碰撞的数量加1  */
	private void OnBodyEntered(Node2D body)
	{
		number_colliding_bodies += 1;
		CheckDealDamage();//检测是否收到伤害
	}

	/* 用于检测敌人是离开玩家的碰撞范围，离开时与玩家碰撞的数量减1  */
	private void OnBOdyExited(Node2D body)
	{
		number_colliding_bodies -= 1;
	}



	/* 当伤害间隔计时器超时后执行 */
	private void OnDamageIntervalTimerTimerOut()
	{
		CheckDealDamage();//检测是否收到伤害
	}

	/* 当血量变化后，同步更改血条 */
	private void OnHelathChange()
	{
		hp.Value = healthComponent.currentHealth;
	}



	/// <summary>
	/// 升级时选择能力后触发
	/// </summary>
	/// <param name="upgrade">当前选择的升级</param>
	/// <param name="currentAbility">已经拥有的升级</param>
	/// <exception cref="NotImplementedException"></exception>
	private void OnAbilityUpgradeAdded(Ability_upgrade upgrade, Dictionary<string, Dictionary> currentAbility)
	{
		//判断技能类型是不是具体技能（Ability），而不是技能的升级(Ability_upgrade)，
		//Ability了类继承自Ability_upgrade，是Ability_upgrade的之类，
		//如果upgrade可以转换成Ability类型，则说明这是个具体的技能，而不是升级某个技能的属性
		if (upgrade is not Ability)
		{
			return;
		}
		Ability upgrade_ability = upgrade as Ability;
		abilitys.AddChild(upgrade_ability.AbilityControllerScene.Instantiate());//将能力实例化且添加到场景


	}



}
