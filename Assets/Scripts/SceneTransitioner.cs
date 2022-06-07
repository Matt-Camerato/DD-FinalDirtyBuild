using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour {
	public int index;
	public void Go() {
		SceneManager.LoadScene(index);
	}
}