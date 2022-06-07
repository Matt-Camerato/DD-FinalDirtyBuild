using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodUIDrag : UIDragItem {
	Food food;
	public PointBuy pointBuy;

	void Awake() {
		food = GetComponent<Food>();
	}

	public override void OnEndDrag(PointerEventData eventData) {
		var results = new List<RaycastResult>();
		graphicRaycaster.Raycast(eventData, results);

		// Check all hits.
		foreach (var hit in results) {
			var slot = hit.gameObject.GetComponent<UIDropSlot>();

			if (slot) {
				if (slot.SlotFilled && slot.currentItem.GetComponent<UnitTempDisplay>()) {
					UnitTempDisplay display = slot.currentItem.GetComponent<UnitTempDisplay>();
					display.data = food.data.Apply(display.data);
					Destroy(gameObject);

					UnitListStaticRef.FoodToApply++;
				}
			}
		}

		// Changing parent back to slot.
		transform.SetParent(currentSlot.transform);
		// And centering item position.
		transform.localPosition = Vector3.zero;
	}

	void OnDestroy() {
		currentSlot.currentItem = null;
		pointBuy.points--;
		if(pointBuy.points < 0) pointBuy.points = 0;
	}
}