using UnityEngine;
using System.Collections.Generic;
using Thuleanx.Utils;

public class UnitUIGenerator : MonoBehaviour {
	public GameObject UnitPrefab;
	public List<UnitTemplate> Templates;
	public List<UIDropSlot> Slots;
	public List<UIDropSlot> BackupSlots;

	void Awake() {
		if (UnitListStaticRef.Starters == null || UnitListStaticRef.Starters.Count == 0) 
			GenerateUnits();
		else LoadUnits();
	}

	public void GenerateUnits() {
		UnitListStaticRef.FoodToApply = 0;
		// for (int i = 0; i < Slots.Count; i++) {
		for (int i = Slots.Count-1; i >= 0; i--) {
			if (Slots[i].currentItem) Destroy(Slots[i].currentItem.gameObject);
			Slots[i].currentItem = null;

			int r = Calc.RandomRange(0, Templates.Count);
			UnitTemplate template = Templates[r];
			UnitData data = template.GenerateData();
			GameObject obj = GameObject.Instantiate(
				UnitPrefab,
				Slots[i].transform.position,
				Quaternion.identity,
				Slots[i].transform
			);
			obj.transform.localPosition = Vector3.zero;
			UIDragItem Item = obj.GetComponent<UIDragItem>();
			Item.Link(Slots[i]);
			UnitTempDisplay Display = obj.GetComponent<UnitTempDisplay>();
			Display.SetData(data);
		}
		for (int i = 0; i < BackupSlots.Count; i++) {
			if (BackupSlots[i].currentItem) Destroy(BackupSlots[i].currentItem.gameObject);
			BackupSlots[i].currentItem = null;
			int r = Calc.RandomRange(0, Templates.Count);
			UnitTemplate template = Templates[r];
			UnitData data = template.GenerateData();
			GameObject obj = GameObject.Instantiate(
				UnitPrefab, 
				BackupSlots[i].transform.position,
				Quaternion.identity,
				BackupSlots[i].transform
			);
			obj.transform.localPosition = Vector3.zero;
			UIDragItem Item = obj.GetComponent<UIDragItem>();
			Item.Link(BackupSlots[i]);
			UnitTempDisplay Display = obj.GetComponent<UnitTempDisplay>();
			Display.SetData(data);
		}
	}

	public void LoadUnits() {
		int j = 0;
		for (int i = Slots.Count-1; i >= 0 && j < UnitListStaticRef.Starters.Count; i--, j++) {
			GameObject obj = GameObject.Instantiate(
				UnitPrefab, 
				Slots[i].transform.position,
				Quaternion.identity,
				Slots[i].transform
			);
			obj.transform.localPosition = Vector3.zero;
			UIDragItem Item = obj.GetComponent<UIDragItem>();
			Item.Link(Slots[i]);
			UnitTempDisplay Display = obj.GetComponent<UnitTempDisplay>();
			Display.SetData(UnitListStaticRef.Starters[j]);
		}
		for (int i = 0; i < BackupSlots.Count && j < UnitListStaticRef.Starters.Count; i++, j++) {
			GameObject obj = GameObject.Instantiate(
				UnitPrefab, 
				BackupSlots[i].transform.position,
				Quaternion.identity,
				BackupSlots[i].transform
			);
			obj.transform.localPosition = Vector3.zero;
			UIDragItem Item = obj.GetComponent<UIDragItem>();
			Item.Link(BackupSlots[i]);
			UnitTempDisplay Display = obj.GetComponent<UnitTempDisplay>();
			Display.SetData(UnitListStaticRef.Starters[j]);
		}
	}

	void LateUpdate() {
		UnitListStaticRef.Starters = new List<UnitData>();
		for (int i = Slots.Count - 1; i >= 0; i--) {
			UIDropSlot slot = Slots[i];
		// foreach (UIDropSlot slot in Slots) {
			if (slot.currentItem) {
				UnitListStaticRef.Starters.Add(
					slot.currentItem.GetComponent<UnitTempDisplay>().data
				);
			}
		}
		for (int i = 0; i < BackupSlots.Count; i++) {
			UIDropSlot slot = BackupSlots[i];
			if (slot.currentItem) {
				UnitListStaticRef.Starters.Add(
					slot.currentItem.GetComponent<UnitTempDisplay>().data
				);
			}
		}
	}
}