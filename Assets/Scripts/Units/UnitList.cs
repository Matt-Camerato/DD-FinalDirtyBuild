using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitList : MonoBehaviour {
	public float DistPerUnit = 1f;
	public List<Unit> Units = new List<Unit>();

	void Awake() {
		AddAll();
		Organize();
	}

	void LateUpdate() {
		Organize();
	}

	void AddAll() {
		foreach (Transform child in transform) {
			if (child.GetComponent<Unit>()) {
				Unit unit = child.GetComponent<Unit>();
				if (!Units.Contains(unit))
					Units.Add(child.GetComponent<Unit>());
			}
		}
	}

	void Organize() {
		int i = 0;
		foreach (Unit unit in Units) {
			unit.transform.position = transform.position + Vector3.right * DistPerUnit * i;
			i++;
		}
	}

	public bool Consolidate() {
		bool ret = false;
		while (Units.Count > 0 && Units[0].Health == 0) {
			Destroy(Pop());
			ret = true;
		}
		return ret;
	}

	public Unit Pop() {
		Unit unit = Units[0]; 
		Units.RemoveAt(0);
		return unit;
	}

	public Unit GetFirst() => Units.Count > 0 ? Units[0] : null;
	public bool Empty() => Units.Count == 0;

	public void AddUnit(Unit unit) => Units.Add( unit );

	public int Count => Units.Count;
}