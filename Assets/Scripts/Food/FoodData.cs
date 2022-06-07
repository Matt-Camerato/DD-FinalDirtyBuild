using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct FoodData {
	public int HealthBuff;
	public int DamageBuff;
	public string Name;
	public string Description;
	public List<Ability> Abilities;
	public Sprite Sprite;

	public FoodData(int healthBuff, int damageBuff, string name, string description, List<Ability> abilities, Sprite sprite) {
		HealthBuff = healthBuff;
		DamageBuff = damageBuff;
		Name = name;
		Description = description;
		Abilities = abilities;
		Sprite = sprite;
	}

	public UnitData Apply(UnitData unit) {
		unit.Health += HealthBuff;
		unit.Damage += DamageBuff;
		unit.Abilities.AddRange(Abilities);
		return unit;
	}
}