using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Thuleanx.Utils;

public class FoodGenerator : MonoBehaviour {
	public UIDropSlot Slot {get; private set; }
	public GameObject FoodPrefab;
	public List<FoodTemplate> Templates;
	public bool isAtk;
	public PointBuy pointBuy;
	void Awake() {
		Slot = GetComponent<UIDropSlot>();
		Generate();
	}

	void Generate() {
		int r = Calc.RandomRange(0, Templates.Count);
		FoodTemplate template = Templates[isAtk? 0:1];

		FoodData fdata = template.Generate();
		GameObject obj = GameObject.Instantiate(
			FoodPrefab, 
			Slot.transform.position,
			Quaternion.identity,
			Slot.transform
		);
		obj.transform.localPosition = Vector3.zero;
		FoodUIDrag Item = obj.GetComponent<FoodUIDrag>();
		Item.pointBuy = pointBuy;
		Item.Link(Slot);
		if(pointBuy.points <= 0) Item.enabled = false;
		Food Display = obj.GetComponent<Food>();
		Display.SetData(fdata);
		regenerating = false;
	}

	private bool regenerating = false;

	void Update(){
		if(pointBuy.points <= 0 && Slot.currentItem != null) Destroy(Slot.currentItem.gameObject);

		if(Slot.currentItem != null || regenerating || pointBuy.points <= 0) return;

		StartCoroutine(Regenerate());
		regenerating = true;
	}

	IEnumerator Regenerate(){
		yield return new WaitForSeconds(0.6f);
		Generate();
	}
}