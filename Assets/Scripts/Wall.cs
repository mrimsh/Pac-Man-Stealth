using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		renderer.material.SetTextureScale ("_MainTex", new Vector2 (transform.localScale.x * 0.1f, transform.localScale.y * 0.1f));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
