using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public Transform[] menuObjects;
	Rect btn_Game;
	Rect btn_Exit;
	Rect lbl_help;
	Vector3[] objectsOriginPositions;
	
	// Use this for initialization
	void Awake ()
	{
		btn_Game = new Rect ((Screen.width * 0.5F) - 60, (Screen.height * 0.5F) - 30, 120, 30);
		btn_Exit = new Rect ((Screen.width * 0.5F) - 60, (Screen.height * 0.5F) + 10, 120, 30);
		lbl_help = new Rect ((Screen.width * 0.5F) - 80, (Screen.height * 0.5F) + 60, 160, 300);
		
		objectsOriginPositions = new Vector3[menuObjects.Length];
		for (int i = 0; i < menuObjects.Length; i++) {
			objectsOriginPositions [i] = menuObjects [i].transform.localPosition;
			menuObjects [i].transform.localPosition = new Vector3 (0, 0, Random.Range (100, 2000));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < menuObjects.Length; i++) {
			menuObjects [i].transform.localPosition = Vector3.Lerp (menuObjects [i].transform.localPosition, objectsOriginPositions [i], 3F * Time.deltaTime);
			menuObjects [i].LookAt (camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 7)));
		}
	}
	
	void OnGUI ()
	{
		if (GUI.Button (btn_Game, "START GAME")) {
			Application.LoadLevel ("Game");
		}
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_EDITOR
		if (GUI.Button (btn_Exit, "EXIT")) {
			Application.Quit ();
		}
#endif
		GUI.Label (lbl_help, "controls:\nforward - W/UP \nbackward - S/DOWN \nturn to left - A/LEFT \n" +
			"turn to right - D/RIGHT \nrun - SHIFT/LMB \ntake a look - SPACE/RMB \nexitToMenu - ESC \n\n" +
			"Eat swiborgrsies. Try not to get caught. And remember, They are not happy with your presence..");
	}
}
