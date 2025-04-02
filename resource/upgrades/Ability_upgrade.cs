using Godot;
using System;
/* 资源类，注意是继承Resource而不是Node，否则无法正确引用 */
public partial class Ability_upgrade : Resource
{
    [Export]
	public string id;//唯一标识
	[Export]
	public string name;//名字
	[Export(PropertyHint.MultilineText)]
	public string description;//描述

}
