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

    public RawImage BazookBar;

	public float speed;
	public float jumpForce;

	private string fireButtonTag;
	private string escapeButtonTag;
	private string jumpButtonTag;
	private string horizontalTag;
	private string verticalTag;

	public enum PlayerInputControl {
		Keyboard = 0,
		Joystick = 1
	}
	public PlayerInputControl playerInputControl;

	bool onGround = true;
	bool canDoubleJump = false;

	private bool isFiringAway = false;

	private bool MouseDown = false;
	private float MouseDownFirstTime;

	private float LoadTime = 0;

	//public Text PowerText;
	private CapsuleCollider capsuleCollider;
	private Rigidbody rigidBody;

	public GameObject gameMenu;
	bool gameMenuVisible = false;

	public Camera playerCamera;

	public BulletController bulletController;

	void Awake () {
		capsuleCollider = this.GetComponent<CapsuleCollider> ();
		rigidBody = this.GetComponent<Rigidbody> ();

	}

	private void setupPlayerControls () {
		fireButtonTag = playerInputControl == PlayerInputControl.Joystick ? InputSettings.P2_FIRE : InputSettings.P1_FIRE;
		escapeButtonTag = playerInputControl == PlayerInputControl.Joystick ? InputSettings.P2_CANCEL : InputSettings.P1_CANCEL;
		jumpButtonTag = playerInputControl == PlayerInputControl.Joystick ? InputSettings.P2_JUMP : InputSettings.P1_JUMP;
		horizontalTag = playerInputControl == PlayerInputControl.Joystick ? InputSettings.P2_HORIZONTAL : InputSettings.P1_HORIZONTAL;
		verticalTag = playerInputControl == PlayerInputControl.Joystick ? InputSettings.P2_VERTICAL : InputSettings.P1_VERTICAL;
	}

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		if (rigidBody != null) {
			rigidBody.freezeRotation = true;
		}
		setupPlayerControls ();
	}

	void FixedUpdate () {
		

		float translation = Input.GetAxis(verticalTag) * speed;
		float strafe = Input.GetAxis(horizontalTag) * speed;
		translation *= Time.deltaTime;
		strafe *= Time.deltaTime;
		transform.Translate (strafe, 0f, translation);

		if (isFiringAway) {
			isFiringAway = false;
			float BufferedTime = (LoadTime > Bullet_Max_Buffer_ms ? Bullet_Max_Buffer_ms : LoadTime);
			float power = (BufferedTime / Bullet_Max_Buffer_ms) * Bullet_Max_Force;
			power =  power < Bullet_Min_Force ? Bullet_Min_Force : power;
			//Shoot(power);
			bulletController.CmdShoot (power, GetPower (), playerCamera.transform.forward);
		}
	}

    void OnGUI () {
		Rect current = BazookBar.uvRect;
    }

	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown (escapeButtonTag)) {
			//SceneManager.LoadScene(0);
			gameMenuVisible = !gameMenuVisible;
			gameMenu.SetActive(gameMenuVisible);

		}

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

		if(Input.GetButtonDown(escapeButtonTag))
			Cursor.lockState = CursorLockMode.None;
		
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


    

	public float GetPower(){
		return LoadTime;
	}

}
