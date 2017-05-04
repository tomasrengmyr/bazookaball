using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterController : MonoBehaviour {
	//Explosion
	public float radius = 5.0F;
	public float power = 10.0F;

    // Force thresholds for bullet
	public int Bullet_Max_Force = 4000;
	public int Bullet_Min_Force = 1000;
    public float Bullet_Max_Buffer_ms = 2.5f;

	//Shoot code
	public GameObject Bullet_Emitter;
	public GameObject Bullet;
    public RawImage BazookBar;

	public float speed;
	public float jumpForce;

	public string fireButtonTag;
	public string escapeButtonTag;
	public string jumpButtonTag;
	public string horizontalTag;
	public string verticalTag;

	bool onGround = true;
	bool canDoubleJump = false;

	private bool isFiringAway = false;

	private bool MouseDown = false;
	private float MouseDownFirstTime;

	private float LoadTime = 0;
	private float LoadPower = 0;

	//public Text PowerText;
	private CapsuleCollider capsuleCollider;
	private Rigidbody rigidBody;

	public GameObject gameMenu;
	bool gameMenuVisible = false;


	void Awake () {
		capsuleCollider = this.GetComponent<CapsuleCollider> ();
		rigidBody = this.GetComponent<Rigidbody> ();
	}

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		SetPowerText ();
	}

	void FixedUpdate () {
		if (isFiringAway) {
            isFiringAway = false;
            float BufferedTime = (LoadTime > Bullet_Max_Buffer_ms ? Bullet_Max_Buffer_ms : LoadTime);
			float power = (BufferedTime / Bullet_Max_Buffer_ms) * Bullet_Max_Force;
            power =  power < Bullet_Min_Force ? Bullet_Min_Force : power;
            Shoot(power);
		}

		float translation = Input.GetAxis(verticalTag) * speed;
		float strafe = Input.GetAxis(horizontalTag) * speed;
		translation *= Time.deltaTime;
		strafe *= Time.deltaTime;
		transform.Translate (strafe, 0f, translation);

		//Kolla så gubben är på marken så man kan hoppa igen
		RaycastHit hit;

		Vector3 physicsCenter = this.transform.position + capsuleCollider.center;

		Debug.DrawRay(physicsCenter, Vector3.down, Color.red, 1);
		if (Physics.Raycast (physicsCenter, Vector3.down, out hit, 1.0f)) {
			if (hit.transform.gameObject.tag != "Player") {
				onGround = true;
			}
		} else {
			onGround = false;
		}
		//Debug.Log (onGround);
		if(Input.GetButtonDown(InputSettings.INPUT_CANCEL))
			Cursor.lockState = CursorLockMode.None;
		//jump code

		if (Input.GetButtonDown (jumpButtonTag) && !onGround && canDoubleJump) {
			rigidBody.AddForce (Vector3.up * jumpForce);
			canDoubleJump = false;
		}
		else if (Input.GetButtonDown (jumpButtonTag) && onGround) {
			rigidBody.AddForce (Vector3.up * jumpForce);
			canDoubleJump = true;
		}


		if (Input.GetButtonDown(fireButtonTag)) {
			if (!MouseDown) {
				MouseDownFirstTime = Time.realtimeSinceStartup;
			}
			MouseDown = true;
		}
		if (MouseDown) {

			float currentProgress = ((Time.realtimeSinceStartup - MouseDownFirstTime) / Bullet_Max_Buffer_ms);
			// TODO set these base position values from existing object position. 
			float currentPosition = -260 + 170;
			if (currentProgress < 1) {
				currentPosition = -260f + (170f * currentProgress);
			}
			BazookBar.transform.localPosition = new Vector3 (
				currentPosition, 
				BazookBar.transform.localPosition.y, 
				1);
		}
		if (Input.GetButtonUp(fireButtonTag)) {
			BazookBar.transform.localPosition = new Vector3 (
				-260, 
				BazookBar.transform.localPosition.y, 
				1);

			MouseDown = false;
			LoadTime = Time.realtimeSinceStartup - MouseDownFirstTime;
			isFiringAway = true;
		}
	}
    void OnGUI () {
		var w = BazookBar.uvRect.width + 2;
		Rect current = BazookBar.uvRect;
		if (MouseDown) {

		}

    }

	// Update is called once per frame
	void Update () {
		

		if (Input.GetButtonDown (escapeButtonTag)) {
			//SceneManager.LoadScene(0);
			gameMenuVisible = !gameMenuVisible;
			gameMenu.SetActive(gameMenuVisible);

		}
	}


    void Shoot (float force) {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate (Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
		Temporary_Bullet_Handler.GetComponent<BulletHitDitection>().setPower(GetPower());
		Temporary_Bullet_Handler.transform.Rotate (Vector3.left * 90);
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody> ();
		Debug.Log ("name: " + this.name);

		//Check what player and camera to use

		Camera cam = this.name == "Player2Char" ?
			GameObject.FindWithTag ("PLAYER_TWO").GetComponent<Camera> () :
			GameObject.FindWithTag ("PLAYER_ONE").GetComponent<Camera> ();


		Temporary_RigidBody.AddForce (cam.transform.forward * Mathf.FloorToInt(force));
        Destroy (Temporary_Bullet_Handler, 10.0f);
    }

	void SetPowerText(){
		//PowerText.text = LoadPower.ToString ();
	}

	public float GetPower(){
		return LoadTime;
	}

	public void SetLoadPower(float _loadPower){
		LoadPower = _loadPower;
	}

}
