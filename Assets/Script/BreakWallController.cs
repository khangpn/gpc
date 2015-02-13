using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BreakWallController : MonoBehaviour {
	public Texture2D[] wallStates;
	private float timer = 0.0f;

	void Update() {
		if (timer < 1f)
			timer += Time.deltaTime;
	}

	public void Crack() {
		Debug.Log(wallStates.Length);
		if( timer >= 0.5f) {
			if (wallStates.Length == 0) {
				Destroy (this.gameObject);
			} else {
				renderer.material.mainTexture = wallStates[0];
				List<Texture2D> statesList = new List<Texture2D>(wallStates);
				statesList.RemoveAt(0);
				wallStates = statesList.ToArray();
			}
			timer = 0.0f;
		}
	}
}
