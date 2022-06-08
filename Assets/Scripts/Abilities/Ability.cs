using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Thuleanx.Utils;

[System.Serializable]
public enum AbillityTriggerEvents {
	Spawn,
	OnAttack,
	OnDamaged,
	TurnStart,
	TurnEnd,
	Death
}

[System.Serializable]
public enum AbilityTargets {
	Self,
	Behind,
	Ahead,
	FirstAlly,
	LastAlly,
	EnemyFront,
	EnemyBack,
	Allies,
	Enemies,
	Adjacent,
	RandomEnemy,
	RandomAlly,
	All
}

public abstract class Ability : ScriptableObject {
	public AbillityTriggerEvents TriggerEvent;
	public AbilityTargets Targets;

	public abstract IEnumerator Perform(List<Unit> units, int selfIndex);
	public static List<Unit> FindAllTargets(AbilityTargets targetIdentifier, UnitList allies, UnitList enemies, int selfIndex) {
		List<Unit> units = new List<Unit>();

		if (targetIdentifier == AbilityTargets.Self) units.Add(allies.Units[selfIndex]);

		if (targetIdentifier == AbilityTargets.Behind && selfIndex + 1 < allies.Units.Count ) units.Add(allies.Units[selfIndex + 1]);
		if (targetIdentifier == AbilityTargets.Ahead && selfIndex - 1 >= 0) units.Add(allies.Units[selfIndex - 1]);

		if (targetIdentifier == AbilityTargets.FirstAlly && !allies.Empty()) units.Add(allies.Units[0]);
		if (targetIdentifier == AbilityTargets.LastAlly && !allies.Empty()) units.Add(allies.Units[allies.Count - 1]);

		if (targetIdentifier == AbilityTargets.EnemyFront && !enemies.Empty()) units.Add(enemies.Units[0]);
		if (targetIdentifier == AbilityTargets.EnemyBack && !enemies.Empty()) units.Add(enemies.Units[enemies.Count - 1]);

		if (targetIdentifier == AbilityTargets.Allies) units.AddRange(allies.Units);
		if (targetIdentifier == AbilityTargets.Enemies) units.AddRange(enemies.Units);
		if (targetIdentifier == AbilityTargets.Adjacent) {
			if(1 < enemies.Units.Count) units.Add(enemies.Units[1]);
			if(selfIndex + 1 < allies.Units.Count) units.Add(allies.Units[selfIndex+1]);
		}
		if(targetIdentifier == AbilityTargets.RandomEnemy) units.Add(enemies.Units[Calc.RandomRange(0,enemies.Units.Count)]);
		if(targetIdentifier == AbilityTargets.RandomAlly) units.Add(enemies.Units[Calc.RandomRange(0,allies.Units.Count)]);

		if (targetIdentifier == AbilityTargets.All) {
			units.AddRange(allies.Units);
			units.AddRange(enemies.Units);
		}

		return units;
	}
}