using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float moveSpeed = 8.0f;
	public float sprintSpeed = 12.0f;
	public float mouseSensitivity = 2.0f;
	public bool invertMouse = false;

	float verticalRotation = 0f;
	bool sprinting = false;

	bool mouseLocked = true;

	CharacterController cc;
	// Use this for initialization
	void Start () {
		// Just to make sure the script has been called
		cc = GetComponent<CharacterController>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		Debug.Log (" === FirstPersonController initialised === ");
	}
	
	// Update is called once per frame
	void Update () {

		#region Camera Movement
		// Rotation Left & Right
		this.transform.Rotate (0, Input.GetAxis ("Mouse X") * mouseSensitivity, 0);
		// Rotation Up & Down
		float invert = invertMouse ? 1 : -1;
		verticalRotation = Mathf.Clamp(verticalRotation + Input.GetAxis("Mouse Y") * mouseSensitivity * invert, -60, 60);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		#endregion

		#region Player Movement
		// Sprinting

		// FIXME: Hardcoded Sprint Button. IRGS!
		sprinting = Input.GetKey(KeyCode.LeftShift) ? true: false;
		float movementSpeed = sprinting ? sprintSpeed : moveSpeed;

		// Basic movement

		Vector3 speed = new Vector3 (Input.GetAxis ("Horizontal") * movementSpeed, 0, Input.GetAxis ("Vertical") * movementSpeed);

		// HACK: To make sure diagonal movement is not faster than straight movement. Not sure if there is a nicer way to accomplish this
		if (speed.magnitude > movementSpeed)
			speed = speed.normalized * movementSpeed;

		speed = this.transform.rotation * speed;
		cc.SimpleMove(speed);

		#endregion

		#region Testregion
		// To unlock the Mouse and be able to exit Game Mode in the Editor
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (mouseLocked) {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				mouseLocked = false;
			} else {
				mouseLocked = true;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				mouseLocked = true;
			}
		}
		#endregion

	}
}
