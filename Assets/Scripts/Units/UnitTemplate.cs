using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UnitTemplate", menuName = "MDigi/UnitTemplate", order = 0)]
public class UnitTemplate : ScriptableObject {
	public int Health;
	public int Damage;
	public string Name;
	public string Description;
	public Sprite Sprite;
	public List<Ability> Abilities = new List<Ability>();

	public GameObject Generate(GameObject UnitPrefab) {
		GameObject obj = GameObject.Instantiate(UnitPrefab);
		UnitData data = GenerateData();
		Unit unit = obj.GetComponent<Unit>();
		unit.SetData(data);
		return obj;
	}

	public UnitData GenerateData()
		=> new UnitData(Health, Damage, Name, Description, Sprite, Abilities);
}