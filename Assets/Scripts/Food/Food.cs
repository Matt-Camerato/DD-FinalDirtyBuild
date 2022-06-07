using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour {
	public FoodData data;
	public TooltipHandler TooltipHandler;	
	public TMP_Text Name_Text;

	public GameObject AttackBuffObj;
	public TMP_Text AttackBuff_Text;

	public GameObject HealthBuffObj;
	public TMP_Text HealthBuff_Text;

	public Image Sprite;

	bool assigned = false;

	void LateUpdate() {
		Rerender();
	}

	void Rerender() {
		if (assigned) {
			Name_Text.text = data.Name;
			TooltipHandler.Description = data.Description;

			AttackBuffObj.SetActive(data.DamageBuff != 0);
			HealthBuffObj.SetActive(data.HealthBuff != 0);

			AttackBuff_Text.text = Sign(data.DamageBuff) + Mathf.Abs(data.DamageBuff).ToString();
			HealthBuff_Text.text = Sign(data.HealthBuff) + Mathf.Abs(data.HealthBuff).ToString();

			Sprite.sprite = data.Sprite;
		}
	}

	public void SetData(FoodData data) {
		assigned = true;
		this.data = data;
	}

	public string Sign(int num) => num < 0 ? "-" : "+";
}