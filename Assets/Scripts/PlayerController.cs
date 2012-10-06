using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 2f;
	public float cameraDistanceNormal = -1.5f;
	public float cameraDistanceOnStick = -2.0f;
	public float mouseSpeed = 3f;
	private bool isStickedToWall;
	private Camera playerCamera;

	private float TargetRotation {
		get {
			return _targetRotation;
		}
		set {
			_targetRotation = value;
			if (_targetRotation > 360) {
				_targetRotation -= 360;
			} else if (_targetRotation < 0) {
				_targetRotation += 360;
			}
		}
	}

	private float _targetRotation;
	
	// Use this for initialization
	void Start ()
	{
		playerCamera = Camera.mainCamera;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localRotation = Quaternion.Slerp (
			transform.localRotation, 
			Quaternion.Euler (new Vector3 (0, TargetRotation, 0)),
			15f * Time.deltaTime);
		if (isStickedToWall) {
			playerCamera.transform.RotateAround (transform.localPosition, Vector3.up, Input.GetAxis ("Mouse X") * mouseSpeed);
		} else {
			playerCamera.transform.localPosition = Vector3.Lerp (
			playerCamera.transform.localPosition,
			new Vector3 (0, 1, cameraDistanceNormal
			),
			Time.deltaTime * 15f
			);
			playerCamera.transform.localRotation = Quaternion.Slerp (
				playerCamera.transform.localRotation,
				Quaternion.Euler (15f, 0, 0),
				Time.deltaTime * 15f
			);
		}
		
		if (Input.GetButtonDown ("TurnLeft")) {
			TargetRotation -= 90;
		} else if (Input.GetButtonDown ("TurnRight")) {
			TargetRotation += 90;
		}

		isStickedToWall = Input.GetButton ("WallStick");
	}
	
	void FixedUpdate ()
	{
		float currentSpeed = 0f;
		if (isStickedToWall) {
		} else {
			currentSpeed = Input.GetAxis ("Vertical") * moveSpeed;
		}
		rigidbody.velocity = transform.localRotation * Vector3.forward * currentSpeed * 200f * Time.fixedDeltaTime;
		
// Mouse Look Moving
//
//		Vector3 currentDirection = Vector3.zero;
//		
//		if (!isStickedToWall) {
//			currentDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
//		}
//		
//		rigidbody.velocity = transform.localRotation * currentDirection * 200f * Time.fixedDeltaTime;
	}
}
