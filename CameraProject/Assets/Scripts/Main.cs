using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	GameObject player;
	[SerializeField] Camera mainCamera;
	PlayerControl pc;
	CameraControls cc;
	[SerializeField] GameObject text;
	public string reason;
	GameObject theReason;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		pc = (PlayerControl)player.GetComponent ("PlayerControl");
		cc = (CameraControls)mainCamera.GetComponent("CameraControls");
		theReason =  new GameObject();
		theReason.AddComponent<TextMesh>();
		TextMesh tm = theReason.GetComponent<TextMesh>();
		theReason.transform.position = new Vector3(text.transform.position.x, text.transform.position.y - 5, text.transform.position.z);
		tm.color = Color.black;
		tm.anchor = TextAnchor.MiddleCenter;
		tm.alignment = TextAlignment.Center;
		tm.fontSize = text.GetComponent<TextMesh>().fontSize*3/4;
	}
	
	// Update is called once per frame
	void Update () {
		if (pc.alive == false) {
			pc.canMoveLeft = false;
			pc.canMoveRight = false;
			cc.active = false;
			text.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 1, 15.0f);
			theReason.transform.position = new Vector3(text.transform.position.x, text.transform.position.y - 2, text.transform.position.z);
			TextMesh tm = theReason.GetComponent<TextMesh>();
			tm.text = reason;

		}
	}
}
