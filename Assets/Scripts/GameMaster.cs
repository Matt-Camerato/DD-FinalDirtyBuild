using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Thuleanx.Utils;

public class GameMaster : MonoBehaviour {
	public UnitList Left, Right;
	public GameObject UnitPrefab;
	public float StartPauseTime = 1f;
	public float PauseTime;
	public float EndPauseTime = 2f;
	public UnityEvent OnAttack;
	public UnityEvent OnEnd;

	public int MaxUnits = 6;

	void Start() {
		if (UnitListStaticRef.Starters != null) {
			int cnt = MaxUnits;
			foreach (UnitData data in UnitListStaticRef.Starters) {
				GameObject obj = GameObject.Instantiate(UnitPrefab);
				Unit unit = obj.GetComponent<Unit>();
				unit.SetData(data);
				Left.AddUnit(unit);
				if (--cnt == 0) break;
			}
		}
		StartCoroutine(GameLoop());
	}

	IEnumerator GameLoop() {
		// we start
		yield return ActivateAll(AbillityTriggerEvents.Spawn);
		yield return new WaitForSeconds(StartPauseTime);

		int cnt = Left.Count + Right.Count;
		float speedMult = 1;
		while (!Left.Empty() && !Right.Empty()) {
			yield return ActivateAll(AbillityTriggerEvents.TurnStart);
			yield return Turn();

			bool consolidated = false;
			while (true) {
				Unit unit = null;
				foreach (Unit candidate in Left.Units) if (candidate.Health <= 0)	unit = candidate;
				foreach (Unit candidate in Right.Units) if (candidate.Health <= 0)	unit = candidate;
				if (!unit) break;
				consolidated = true;

				yield return Activate(unit, AbillityTriggerEvents.Death);
				Debug.Log(Left.Units.Remove(unit) + " " + Right.Units.Remove(unit));
				// Left.Units.Remove(unit);
				// Right.Units.Remove(unit);
				DestroyImmediate(unit.gameObject);
			}
			if (consolidated) speedMult = 1;
			else speedMult *= 1.2f;

			if (consolidated) 
				yield return new WaitForSeconds(PauseTime);
			else
				yield return new WaitForSeconds(PauseTime * (Left.Count + Right.Count) / cnt / speedMult);
			yield return ActivateAll(AbillityTriggerEvents.TurnEnd);

		}
		yield return new WaitForSeconds(EndPauseTime);
		OnEnd?.Invoke();
	}

	IEnumerator Turn() {
		Unit left = Left.GetFirst() , right = Right.GetFirst();
		yield return Activate(Left.GetFirst(), AbillityTriggerEvents.OnAttack);
		yield return Activate(Right.GetFirst(), AbillityTriggerEvents.OnAttack);
		left.Health -= right.Damage;
		right.Health -= left.Damage;
		OnAttack?.Invoke();
		yield return Activate(Left.GetFirst(), AbillityTriggerEvents.OnDamaged);
		yield return Activate(Right.GetFirst(), AbillityTriggerEvents.OnDamaged);
	}

	IEnumerator ActivateAll(AbillityTriggerEvents eve) {
		yield return Activate(Left.Units, eve);
		yield return Activate(Right.Units, eve);
	}

	IEnumerator Activate(List<Unit> units, AbillityTriggerEvents eve) {
		foreach (Unit unit in units)
			yield return Activate(unit, eve);
	}

	IEnumerator Activate(Unit unit, AbillityTriggerEvents eve) {
		foreach (Ability ability in unit.Abilities) {
			if (ability.TriggerEvent == eve) {
				UnitList allies = Left;
				UnitList enemies = Right;

				if (enemies.Units.Contains(unit))  {
					UnitList tmp = enemies;
					enemies = allies;
					allies = tmp;
				}

				int index = allies.Units.FindIndex((value) => value == unit);
				yield return ability.Perform(Ability.FindAllTargets(ability.Targets, allies, enemies, index),index);
			}
		}
	}
}