using UnityEngine;
using System.Collections.Generic;
using Thuleanx.Utils;

[RequireComponent(typeof(UIDropSlot))]
public class SelfGenerator : MonoBehaviour {
	public GameObject UnitPrefab;
	public UIDropSlot Slot {get; private set; }
	public List<UnitTemplate> Templates;

	void Awake() {
		Slot = GetComponent<UIDropSlot>();
		Generate();
	}

	void Generate() {
		int r = Calc.RandomRange(0, Templates.Count);
		UnitTemplate template = Templates[r];
		UnitData data = template.GenerateData();
		GameObject obj = GameObject.Instantiate(
			UnitPrefab, 
			Slot.transform.position,
			Quaternion.identity,
			Slot.transform
		);
		obj.transform.localPosition = Vector3.zero;
		UIDragItem Item = obj.GetComponent<UIDragItem>();
		Item.Link(Slot);
		UnitTempDisplay Display = obj.GetComponent<UnitTempDisplay>();
		Display.SetData(data);
	}
}