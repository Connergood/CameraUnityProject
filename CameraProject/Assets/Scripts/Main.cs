using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	GameObject player;
	Camera mainCamera;
	PlayerControl pc;
	CameraControls cc;
	GameObject text;
	public string reason;
	GameObject theReason;
    public bool thereIsAGrabbedObject = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
        mainCamera = (Camera)GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		pc = (PlayerControl)player.GetComponent<PlayerControl>();
		cc = (CameraControls)mainCamera.GetComponent("CameraControls");
        text = GameObject.FindGameObjectWithTag("GameOver");
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
			cc.active = false;
			text.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 1, -2.0f);
			theReason.transform.position = new Vector3(text.transform.position.x, text.transform.position.y - 2, text.transform.position.z);
			TextMesh tm = theReason.GetComponent<TextMesh>();
            tm.text = reason + System.Environment.NewLine + "Press 'R' To Restart";
            tm.color = Color.red;
            GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bg.transform.position = new Vector3(text.transform.position.x, text.transform.position.y - 1, -1.0f);
            bg.transform.localScale = new Vector3(9, 9);
            bg.GetComponent<Renderer>().material.color = Color.black;
		}

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    public void setGrabbable(bool b)
    {
        thereIsAGrabbedObject = b;
    }

    public bool GetGrabbable()
    {
        return thereIsAGrabbedObject;
    }

    public void lockThingsInPlace(bool locked)
    {
        pc.playerHidden = !locked;
        cc.active = !locked;
    }
}
