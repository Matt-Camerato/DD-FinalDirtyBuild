using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class Tooltip : MonoBehaviour {
	public static Tooltip Instance = null;

	public RectTransform rectTransform {get; private set; }
	TooltipHandler attachedHandler;

	[SerializeField] TMP_Text descriptionText;
	[SerializeField] Vector2 offset;

	void Awake() {
		rectTransform = GetComponent<RectTransform>();
		Instance = this;
	}

	void Update() {
		if (!attachedHandler) gameObject.SetActive(false);
	}

	void LateUpdate() {
		Reposition();
	}

	public void AttachUIObject(TooltipHandler handler) {
		attachedHandler = handler;
		gameObject.SetActive(true);
		descriptionText.text = handler.Description;
	}

	public void DettachUIObject(TooltipHandler handler) {
		if (attachedHandler == handler) {
			attachedHandler = null;
			gameObject.SetActive(false);
		}
	}
	
	void Reposition() {
		if (attachedHandler) {
			Vector2 anchor = attachedHandler.rectTransform.position;
			anchor += offset;

			rectTransform.position = anchor;
		}
	}
}