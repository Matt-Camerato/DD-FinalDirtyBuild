using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct UnitData {
	int health;
	public int Health {get => health; set => health = Mathf.Max(0, value); }
	int damage;
	public int Damage {get => damage; set => damage = Mathf.Max(0, value); }
	public string Name;
	public string Description;
	public Sprite sprite;
	public List<Ability> Abilities;

	public UnitData(int health, int damage, string name, string description, Sprite sprite, List<Ability> abilities) {
		this.health = health;
		this.damage = damage;
		Name = name;
		Description = description;
		this.sprite = sprite;
		Abilities = abilities;
	}
}