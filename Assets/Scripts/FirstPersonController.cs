using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float movementSpeed = 8.0f;
	public float mouseSensitivity = 2.0f;
	public bool invertMouse = false;

	float verticalRotation = 0f;

	CharacterController cc;
	// Use this for initialization
	void Start () {
		// Just to make sure the script has been called
		cc = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
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
		// Basic movement
		Vector3 speed = new Vector3 (Input.GetAxis ("Horizontal") * movementSpeed, 0, Input.GetAxis ("Vertical") * movementSpeed);
		speed = this.transform.rotation * speed;
		cc.SimpleMove(speed);
		#endregion

		#region Testregion
		// To unlock the Mouse and be able to exit Game Mode in the Editor
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Debug.Log("Esc pressed!");
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		#endregion

	}
}
