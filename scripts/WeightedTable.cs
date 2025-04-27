using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
/// <summary>
/// 权重表，决定资源出现的几率
/// </summary>
public partial class WeightedTable
{
	// private Dictionary[] items = [];
	private Array<Dictionary> items = [];

    public WeightedTable(Array<Dictionary> items)
    {
        this.items = items;
    }


    private int weightSun = 0;//权重总和
	
    public WeightedTable()
    {
    }



    public int WeightSun { get => weightSun; set => weightSun = value; }
    public Array<Dictionary> Items { get => items; set => items = value; }






    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="item">添加项</param>
    /// <param name="weight">权重</param>
    public void AddItem(Variant item, int weight)
	{
		Dictionary dictionary = new Dictionary(){
			{"item",item},
			{"weight",weight}
		};
		items.Add(dictionary);
        weightSun += weight;//权重累积
        GD.Print(items);
	}

	/// <summary>
	/// 随机选择列表中的一项,返回指定类型
	/// </summary>
	/// <returns>随机结果，可能为null</returns>
	public T PickItem<[MustBeVariant] T>()
	{
		int chosenWeight = GD.RandRange(1, weightSun);
		int iterationSum = 0;//迭代权重
		if(items.Count == 0 || items is null){
			return default;

		}
		foreach (Dictionary dic in items)
		{
			iterationSum += (int)dic["weight"];
			if (chosenWeight <= iterationSum)
			{
				// 尝试将 Variant 转换为目标类型 T
				try
				{
					return (T)dic["item"].As<T>();
				}
				catch (InvalidCastException)
				{
					GD.PushError($"无法将 {dic["weight"].VariantType} 转换为 {typeof(T)}");
					return default;
				}
			}
		}
		return default;
	}
	/// <summary>
	/// 随机选择列表中的一项,返回var类型
	/// </summary>
	/// <returns>随机结果，可能为null</returns>
	public Variant PickItem()
	{
		int chosenWeight = GD.RandRange(1, weightSun);
		int iterationSum = 0;//迭代权重
		if(items.Count == 0 || items is null){
			return default;

		}
		foreach (Dictionary dic in items)
		{
			iterationSum += (int)dic["weight"];
			if (chosenWeight <= iterationSum)
			{
				return dic["item"];
			}
		}
		return default;
	}

	
}
