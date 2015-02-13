using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D hit) {
		Debug.Log ("hittttttttt");
		if (hit.gameObject.tag == "Player") {
			foreach (Transform child in transform) {
				child.gameObject.SetActive(true);
			}
		}
	}
}
