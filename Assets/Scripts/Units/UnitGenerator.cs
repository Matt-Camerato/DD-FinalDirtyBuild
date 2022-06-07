using UnityEngine;
using System.Collections.Generic;
using Thuleanx.Utils;

public class UnitGenerator : MonoBehaviour {
	public GameObject UnitPrefab;
	public List<UnitTemplate> Templates;
	public List<FoodTemplate> Foods;
	public int Number = 10;

	void Awake() {
		List<UnitData> datas = new List<UnitData>();
		while (Number-->0) {
			int r = Calc.RandomRange(0, Templates.Count);
			UnitTemplate template = Templates[r];
			datas.Add(template.GenerateData());
		}
		for (int i = 0; i < UnitListStaticRef.FoodToApply; i++) {
			int r = Calc.RandomRange(0, Foods.Count);
			FoodTemplate template = Foods[r];
			FoodData fdata = template.Generate();
			int j = Calc.RandomRange(0, datas.Count);
			datas[j] = fdata.Apply(datas[j]);
		}
		foreach (UnitData unitData in datas) {
			GameObject obj = GameObject.Instantiate(UnitPrefab);
			Unit unit = obj.GetComponent<Unit>();
			unit.SetData(unitData);
			GetComponent<UnitList>()?.AddUnit(unit);
			unit.transform.SetParent(transform);
		}
	}
}