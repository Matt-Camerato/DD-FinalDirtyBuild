using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitTempDisplay : MonoBehaviour {
	public UnitData data;
	public TMP_Text Health_Tracker;
	public TMP_Text Attack_Tracker;
	public TMP_Text Name_Tracker;
	public TooltipHandler Description_Tooltip;
	public Image image;

	bool assigned = false;

	public void SetData(UnitData data) {
		assigned = true;
		this.data = data;
	}

	void LateUpdate() {
		Rerender();
	}

	public void Rerender() {
		if (assigned) {
			if (Health_Tracker) Health_Tracker.text = data.Health.ToString();
			if (Attack_Tracker) Attack_Tracker.text = data.Damage.ToString();
			image.sprite = data.sprite;
			Name_Tracker.text = data.Name;
			if (Description_Tooltip) Description_Tooltip.Description = data.Description;
		}
	}
}