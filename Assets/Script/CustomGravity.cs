using UnityEngine;
using System.Collections;

public class CustomGravity : MonoBehaviour {
	public float gravity = -10f;

	public void Attract(Transform body) {
		Vector2 gravityUp = new Vector2(0, gravity);
		Vector2 bodyUp = body.up;

		body.rigidbody2D.AddForce (gravityUp);
	}
}
