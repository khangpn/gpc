using UnityEngine;
using System.Collections;

public class FinishPoint : MonoBehaviour {
	public string nextStage = "";

	void OnTriggerEnter2D (Collider2D hit) {
		if (hit.gameObject.tag == "Player") 
			Application.LoadLevel(nextStage);
	}
}
