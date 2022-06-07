using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UIDraggableCanvas : MonoBehaviour {
	public static UIDraggableCanvas Instance;
	public Canvas canvas {get; private set; }

	void Awake() {
		Instance = this;
		canvas = GetComponent<Canvas>();
	}
}