using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 0.8f,
	runSpeed = 1.7f;
	PlayerController player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		RaycastHit hit;
		float speed = 0;
		Physics.Raycast (transform.localPosition, player.transform.localPosition - transform.localPosition, out hit);
		Debug.DrawRay (transform.localPosition, player.transform.localPosition - transform.localPosition);
		if (
				hit.collider.gameObject == player.gameObject && 
				((Vector3.Angle (hit.collider.transform.localPosition - transform.localPosition, transform.forward) < 15.0F) ||
				player.isRunning)) {
			transform.LookAt (player.transform.localPosition);
			speed = runSpeed;
		} else {
			float mod;
			speed = moveSpeed;
			mod = transform.localRotation.eulerAngles.y % 90;
			if (mod < 45) {
				transform.localRotation = Quaternion.Euler (0, transform.localRotation.eulerAngles.y - mod, 0);
			} else {
				transform.localRotation = Quaternion.Euler (0, transform.localRotation.eulerAngles.y + mod, 0);
			}
			Debug.DrawRay (transform.localPosition, transform.forward, Color.green);
			if (Physics.Raycast (transform.localPosition, transform.forward, out hit)) {
				if (hit.distance < 2) {
					transform.localRotation = Quaternion.Euler (0, transform.localRotation.y + Random.Range (90, 270), 0);
				}
			}
		}
		rigidbody.velocity = transform.localRotation * Vector3.forward * speed * 200f * Time.fixedDeltaTime;
	}
	
	void OnCollisionEnter (Collision collision)
	{
		switch (collision.gameObject.tag) {
		case "Wall":
			transform.Rotate (new Vector3 (0, 90, 0));
			break;
		case "Player":
			player.Hurt ();
			break;
		}
	}
}
