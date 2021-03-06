using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ADamage", menuName = "MDigi/Abilities/Damage", order = 0)]
public class ADamage : Ability {
	public float PauseTime = .2f;
	public int Damage = 0;

	public override IEnumerator Perform(List<Unit> units, int index) {
		// Gotta have some effects or something here dude
		string names = "";
		foreach (Unit unit in units)
		{
			unit.Health -= Damage;
			if (Damage > 0) unit.DamageAnim();
			name = unit.name+" ";
		}
		Debug.Log(name+"has been damage for "+ Damage);
		yield return new WaitForSeconds(PauseTime);
	}
}