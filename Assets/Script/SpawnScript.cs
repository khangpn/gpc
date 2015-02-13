using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	public GameObject[] obstacles;
	public float spawnMin = 1;
	public float spawnMax = 2;
	public bool active = true;

	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	public void Spawn()
	{
		if (this.active) 
		{
			Instantiate (obstacles [Random.Range (0, obstacles.Length)], transform.position, Quaternion.identity);
		}
		Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}

	public void Pause()
	{
		active = false;
	}

	public void Play()
	{
		this.active = true;
	}
}
