using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 5.0f;
	public float jumpSpeed = 500.0f;
	public float gravity = -10f;
	public GUIStyle style = new GUIStyle();
	
	private bool isJumping = false;
	private Vector2 gravityDir;
	private string playerDir = "down";
	private Vector3 normalScale;
	private float normalJump;
	private string playerForm = "normal";
	private bool facingRight = true;
	private bool facingDown = true;
	private bool finish = false;

	static int swingState = Animator.StringToHash("Player_Hammer_Swing");  

	Animator anim;


	// Use this for initialization
	void Start () {
		rigidbody2D.gravityScale = 0;
		SetForceDir ("down");
		normalScale = transform.localScale;
		normalJump = jumpSpeed;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//Add custom gravity to the environment
		rigidbody2D.AddForce (gravityDir);
		anim.SetFloat ("speed", Mathf.Abs (Input.GetAxis ("Horizontal"))); // Set moving speed for animation
		Movement ();
		Transform ();
	}

	/**Handle transformation
	 * So far player can switch freely from on form to any others
	 */
	void Transform () {
		if (Input.GetKey(KeyCode.Alpha1)) {
			ToNormal();
		}
		if (Input.GetKey(KeyCode.Alpha2)) {
			ToSpider();
		}
		if (Input.GetKey(KeyCode.Alpha3)) {
			ToMini();
		}
		if (Input.GetKey(KeyCode.Alpha4)) {
			ToHammer();
		}
	}

	IEnumerator ToSpiderAnimation() {
		yield return new WaitForSeconds(0.2f);
		anim.SetBool("spider_transform", false);
		anim.SetBool("spider", true);
	}

	IEnumerator ToHammerAnimation() {
		yield return new WaitForSeconds(0.2f);
		anim.SetBool("hammer_transform", false);
		anim.SetBool("hammer", true);
	}
	
	IEnumerator SwingAnimation() {
		playerForm = "hammer_swing";
		anim.SetBool("hammer_swing", true);
		yield return new WaitForSeconds(0.3f);
		anim.SetBool("hammer_swing", false);
		playerForm = "hammer";
	}

	private void Swing() {
		StartCoroutine(SwingAnimation());
	}

	private void ToNormal() {
		Grow();
		SetForceDir("down");
		anim.SetBool("hammer", false);
		anim.SetBool("spider", false);
		playerForm = "normal";
	}

	private void ToSpider() {
		Grow();
		anim.SetBool("hammer", false);
		anim.SetBool("spider_transform", true);
		StartCoroutine(ToSpiderAnimation());
		playerForm = "spider";
	}

	private void ToMini() {
		SetForceDir("down");
		anim.SetBool("hammer", false);
		anim.SetBool("spider", false);
		Shrink();
		playerForm = "shrink";
	}

	private void ToHammer() {
		Grow();
		SetForceDir("down");
		anim.SetBool("spider", false);
		anim.SetBool("hammer_transform", true);
		StartCoroutine(ToHammerAnimation());
		playerForm = "hammer";
	}

	void Movement(){
		/** Handle movement */
		if (Input.GetKey(KeyCode.LeftArrow)) {
			facingRight = false;
			FlipHorrizontal();
			transform.Translate(new Vector3(-moveSpeed,0) * Time.deltaTime);
			
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			facingRight = true;
			FlipHorrizontal();
			transform.Translate(new Vector3(moveSpeed,0) * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.UpArrow) && !this.isJumping) {
			rigidbody2D.AddForce(Vector2.up * jumpSpeed);
			this.isJumping = true;

			anim.SetBool("jump", true);
			anim.SetBool("land", false);
			//Debug.Log(playerForm); // Debug player form
			SetForceDir("down"); //NOTE: If the spider jumps, it will fall out of the wall
		}
		if (Input.GetKey (KeyCode.Space) && playerForm == "hammer") {
			Swing();
		}
	}

	private void FlipHorrizontal() {
		Vector3 theScale = transform.localScale;
		if (facingRight) {
			theScale.x = Mathf.Abs (theScale.x);
		} else {
			theScale.x = Mathf.Abs (theScale.x) * (-1);
		}
		transform.localScale = theScale;
	}

	private void FlipVertical() {
		Vector3 theScale = transform.localScale;
		if (facingDown) {
			theScale.y = Mathf.Abs (theScale.y);
		} else {
			theScale.y = Mathf.Abs (theScale.y) * (-1);
		}
		transform.localScale = theScale;
	}
	
	void OnCollisionEnter2D(Collision2D hit) {
		if (hit.gameObject.name == "Ground") {
			this.isJumping = false;
			anim.SetBool("jump", false);
			anim.SetBool("land", true);

			if (playerForm == "spider") {
				SetForceDir("down");
				facingDown = true;
				FlipVertical();
			}
		}
		if (hit.gameObject.name == "Roof") {
			this.isJumping = false;
			anim.SetBool("jump", false);
			anim.SetBool("land", true);

			if (playerForm == "spider") {
				SetForceDir("up");
				facingDown = false;
				FlipVertical();
			}
		}
		if (hit.gameObject.tag == "Obstacle") {
			if(playerForm == "hammer_swing") {
				Crack(hit);
			} else {
				Die ();
			}
		}
	}

	void OnCollisionStay2D (Collision2D hit) {
		if (hit.gameObject.name == "BreakableWall" && playerForm == "hammer_swing") {
			Crack(hit);
		}
	}

	void OnGUI () {
		if(finish) 
			GUI.Label (new Rect (Screen.width/2, Screen.height/2, 300, 30), "Congratulation you finished Level 1", style);
	}

	private void Crack(Collision2D hit) {
		hit.gameObject.GetComponent<BreakWallController>().Crack();
	}
	
	private void SetForceDir(string dir) {
		if (dir == "up") {
			jumpSpeed = Mathf.Abs (jumpSpeed) * (-1f);
			gravity = Mathf.Abs (gravity);
			gravityDir = new Vector2(0, gravity);
		} 
		if (dir == "down") {
			jumpSpeed = Mathf.Abs(jumpSpeed);
			gravity = Mathf.Abs(gravity) * (-1f);
			gravityDir = new Vector2(0, gravity);
		}
		if (dir == "side") {
		}

	}

	private void Shrink() {
		if (playerForm != "shrink"){
			transform.localScale = new Vector3 (normalScale.x / 2, normalScale.y / 2);
			jumpSpeed = normalJump / 2f;
		}
	}

	private void Grow() {
		if (playerForm != "normal") {
			transform.localScale = normalScale;
			jumpSpeed = normalJump;
		}
	}

	public void Die() {
		PlayerPrefs.SetString("previousLevel", Application.loadedLevelName);
		Application.LoadLevel("end"); //if player is not hammer, he will die.
	}
}
