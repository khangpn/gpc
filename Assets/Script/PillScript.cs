using UnityEngine;
using System.Collections;

public class PillScript : MonoBehaviour {
	void OnCollisionEnter2D (Collision2D hit) {
		if (hit.gameObject.name == "Player") {
			PlayerController playerControl = hit.gameObject.GetComponent<PlayerController>();
			playerControl.ConsumePill(this.gameObject);
		}
	}
}
