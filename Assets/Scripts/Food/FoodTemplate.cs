using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FoodTemplate", menuName = "MDigi/Food", order = 0)]
public class FoodTemplate : ScriptableObject {
	public int HealthBuff;
	public int DamageBuff;
	public string Name;
	public string Description;
	public List<Ability> Abilities;
	public Sprite Sprite;

	public FoodData Generate()
		=> new FoodData(HealthBuff, DamageBuff, Name, Description, Abilities, Sprite);
}