using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	public GUIStyle style = new GUIStyle();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel(PlayerPrefs.GetString("previousLevel", "level_tutorial"));	
		}
	}

	void OnGUI() {
		GUI.Label (new Rect (Screen.width/2.5f, Screen.height/3, 300, 100), "Game Over", style);
		GUI.Label (new Rect (Screen.width/5, Screen.height/2, 300, 30), "PRESS [SPACEBAR] TO RESTART", style);
	}
}
