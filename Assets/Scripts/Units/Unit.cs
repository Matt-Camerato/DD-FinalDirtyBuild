using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	int _health = 0;
	int _damage = 0;
	[HideInInspector]
	public string Name;
	public string Description;
	public int Health {get => _health; set => _health = Mathf.Max(value, 0); }
	public int Damage {get => _damage; set => _damage = Mathf.Max(value, 0); }
	public TMP_Text Health_Tracker;
	public TMP_Text Attack_Tracker;
	public TMP_Text Name_Tracker;
	public SpriteRenderer Sprite;
	public List<Ability> Abilities = new List<Ability>();

	void LateUpdate() {
		Health_Tracker.text = Health.ToString();
		Attack_Tracker.text = Damage.ToString();
		Name_Tracker.text = Name;
	}

	public void SetData(UnitData data) {
		Health = data.Health;
		Damage = data.Damage;
		Sprite.sprite = data.sprite;
		Abilities = data.Abilities;
		Name = data.Name;
		Description = data.Description;
	}

	public void Discard(Unit unit)
	{

	}

	public void DamageAnim() => GetComponent<Animator>().Play("TakeDamage");

	public void HealAnim() => GetComponent<Animator>().Play("GainHealth");
}