using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Vector2 margin, smoothing;
	private Transform player;
	public BoxCollider2D bounds;
	private Vector3 _min, _max;

	public bool isFollowing { get; set;}

	// Use this for initialization
	void Start () {
		_min = bounds.bounds.min;
		_max = bounds.bounds.max;
		isFollowing = true;
		this.player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		var x = transform.position.x;
		var y = transform.position.y;

		if (isFollowing) {
			if(Mathf.Abs(x - player.position.x) > margin.x)
				x = Mathf.Lerp (x, player.position.x, smoothing.x * Time.deltaTime);

			if(Mathf.Abs(y - player.position.y) > margin.y)
				y = Mathf.Lerp (y, player.position.y, smoothing.y * Time.deltaTime);
		}

		var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);
		x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		y = Mathf.Clamp (y, _min.y + cameraHalfWidth, _max.y - camera.orthographicSize);

		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
