using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour
{
	public AudioClip[] clips;
	public float timeGap = 5f;
	private float lastTimePlayed;
	int currentClip = 0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > lastTimePlayed + clips [currentClip].length + timeGap) {
			currentClip = Random.Range (0, clips.Length);
			audio.clip = clips [currentClip];
			audio.Play ();
			lastTimePlayed = Time.time;
		}
	}
}
