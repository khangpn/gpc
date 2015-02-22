using UnityEngine;
using System.Collections;

public class ObstacleMoving : MonoBehaviour {
	Vector2 moveDirection = Vector2.zero;
	public float speed = 8.0F;
	public bool run = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			moveDirection.x = -(speed * Time.deltaTime);
			transform.Translate (moveDirection);
		}
	}

	public void Run()
	{
		this.run = true;
	}

	public void Pause()
	{
		this.run = false;
	}
}
