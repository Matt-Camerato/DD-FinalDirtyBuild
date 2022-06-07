using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	Front,
	Back,
	EnemyFront,
	EnemyBack,
	Allies,
	Enemies,
	All
}

public abstract class Ability : ScriptableObject {
	public AbillityTriggerEvents TriggerEvent;
	public AbilityTargets Targets;

	public abstract IEnumerator Perform(List<Unit> units);
	public static List<Unit> FindAllTargets(AbilityTargets targetIdentifier, UnitList allies, UnitList enemies, int selfIndex) {
		List<Unit> units = new List<Unit>();

		if (targetIdentifier == AbilityTargets.Self) units.Add(allies.Units[selfIndex]);

		if (targetIdentifier == AbilityTargets.Behind && selfIndex + 1 < allies.Units.Count ) units.Add(allies.Units[selfIndex + 1]);
		if (targetIdentifier == AbilityTargets.Ahead && selfIndex - 1 >= 0) units.Add(allies.Units[selfIndex - 1]);

		if (targetIdentifier == AbilityTargets.Front && !allies.Empty()) units.Add(allies.Units[0]);
		if (targetIdentifier == AbilityTargets.Back && !allies.Empty()) units.Add(allies.Units[allies.Count - 1]);

		if (targetIdentifier == AbilityTargets.EnemyFront && !enemies.Empty()) units.Add(enemies.Units[0]);
		if (targetIdentifier == AbilityTargets.EnemyBack && !enemies.Empty()) units.Add(enemies.Units[enemies.Count - 1]);

		if (targetIdentifier == AbilityTargets.Allies) units.AddRange(allies.Units);
		if (targetIdentifier == AbilityTargets.Enemies) units.AddRange(enemies.Units);

		if (targetIdentifier == AbilityTargets.All) {
			units.AddRange(allies.Units);
			units.AddRange(enemies.Units);
		}

		return units;
	}
}