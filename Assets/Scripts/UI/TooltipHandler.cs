using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class TooltipHandler : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler {
	public RectTransform rectTransform {get; private set; }
	public string Description;

	void Awake() {
		rectTransform = GetComponent<RectTransform>();
	}

	public void OnSelect(BaseEventData eventData) => Register();
	public void OnDeselect(BaseEventData eventData) => Deregister();
	public void OnPointerEnter(PointerEventData eventData) => Register();
	public void OnPointerExit(PointerEventData eventData) => Deregister();

	public void Register() {
		Tooltip.Instance.AttachUIObject(this);
	} 
	public void Deregister() {
		Tooltip.Instance.DettachUIObject(this);
	} 
}