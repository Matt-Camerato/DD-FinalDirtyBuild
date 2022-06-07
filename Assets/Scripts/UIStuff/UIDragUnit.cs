using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragUnit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Transform currentParent;
	private Canvas canvas;		   
	private GraphicRaycaster graphicRaycaster;

	/// <summary>
	/// IBeginDragHandler
	/// Method called on drag begin.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!canvas)
		{
			canvas = GetComponentInParent<Canvas>();
			graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
		}
		currentParent = transform.parent; //save parent before drag
		transform.SetParent(canvas.transform, true); //change parent to canvas
		transform.SetAsLastSibling(); //set it as last child to be rendered on top of UI
	}

	/// <summary>
	/// IDragHandler
	/// Method called on drag continuously.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnDrag(PointerEventData eventData)
	{
		// Continue moving object around screen.
		transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0); // Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.
	}

	/// <summary>
	/// IEndDragHandler
	/// Method called on drag end.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnEndDrag(PointerEventData eventData)
	{
		//reset parent
		transform.SetParent(currentParent);
		//center unit position
		//transform.localPosition = Vector3.zero;
	}

	/*
	public void Link(UIDropSlot slot)
	{
		if (slot.currentItem) slot.currentItem.currentSlot = null;
		slot.currentItem = this;
		if (this.currentSlot) this.currentSlot.currentItem = null;
		this.currentSlot = slot;
	}
	*/
}