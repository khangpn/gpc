using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	public string destroyTag = "";

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == destroyTag) {
			kill (other);
		}
		if (destroyTag == "" && other.tag == "Player") {
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			player.Die();
		}
	}

	private void kill(Collider2D other) {
		if (other.gameObject.transform.parent) {
			Destroy (other.gameObject.transform.parent.gameObject);
		} else {
			Destroy (other.gameObject);
		}
	}
}
