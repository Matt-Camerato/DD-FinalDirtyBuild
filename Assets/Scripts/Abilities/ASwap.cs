using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ASwap", menuName = "MDigi/Abilities/Swap", order = 0)]
public class ASwap : Ability {
	public float PauseTime = .2f;
	public enum  Swapping {
		Placement,
		Attack,
		Health
	}
	public enum placing {
		none,
		front,
		behind,
		last,
		first
	}
	[SerializeField] Swapping swapType;
	[SerializeField,Tooltip("Only if Swap Type is Placement")] placing place;

	public override IEnumerator Perform(List<Unit> units, int index) {
		// Gotta have some effects or something here dude
		UnitData me = units[index].GetData();
		switch (swapType)
		{
			case Swapping.Placement:
				switch(place) {
					case placing.front:
						if(index + 1 < units.Count){
							units[index].SetData(units[0].GetData());
							units[index - 1].SetData(me); 
						}
					break;
					case placing.behind:
						if(index + 1 < units.Count){
							units[index].SetData(units[index + 1].GetData());
							units[index + 1].SetData(me);
						}
					break;
					case placing.last:
						units[index].SetData(units[units.Count - 1].GetData());
						units[units.Count - 1].SetData(me); 
					break;
					case placing.first:
					units[index].SetData(units[0].GetData());
						units[0].SetData(me); 
					break;
				}
			break;
			case Swapping.Attack:
				int highestAttack= 0;
				int i = index;
				foreach (Unit unit in units) {
					if(unit.Damage > highestAttack){
						highestAttack = unit.Damage;
						i = units.FindIndex((value) => value == unit);
					}
				}
				if(i != index){
					units[i].Damage = me.Damage;
				}
				me.Damage = highestAttack;
			break;
			case Swapping.Health:
				int highestHealth = 0;
				var k = index;
				foreach (Unit unit in units) {
					if(unit.Health > highestHealth){
						highestHealth = unit.Health;
						k = units.FindIndex((value) => value == unit);
					}
				}
				if(k != index){
					units[k].Health = me.Health;
				}
				me.Health = highestHealth;
			break;
		}
		yield return new WaitForSeconds(PauseTime);
	}
}