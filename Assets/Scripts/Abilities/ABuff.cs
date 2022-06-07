using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ABuff", menuName = "MDigi/Abilities/Buff", order = 0)]
public class ABuff : Ability {
	public float PauseTime = .2f;
	public int HealthInc = 0;
	public int DamageInc = 0;

	public override IEnumerator Perform(List<Unit> units) {
		// Gotta have some effects or something here dude
		foreach (Unit unit in units) {
			unit.Health += HealthInc;
			unit.Damage += DamageInc;

			if (HealthInc > 0) unit.HealAnim();
		}
		yield return new WaitForSeconds(PauseTime);
	}
}