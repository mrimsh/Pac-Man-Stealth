using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float speed = 1.5f;
	GameObject player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		RaycastHit hit;
		Physics.Raycast (transform.localPosition, player.transform.localPosition - transform.localPosition, out hit);
		Debug.DrawRay (transform.localPosition, player.transform.localPosition - transform.localPosition);
		if (hit.collider.gameObject == player) {
			transform.LookAt (player.transform.localPosition);
		} else {
			float mod;
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
}
