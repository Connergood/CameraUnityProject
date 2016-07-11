using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    GameObject Player;
    DoorMain main;
    public int firstLayer = 1;
    public int secondLayer = 2;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        main = GameObject.Find("DoorMain").GetComponent<DoorMain>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pPos = Player.transform.position;
        Vector2 mPos = transform.position;
        if((pPos - mPos).magnitude < .5f)
        {
            if (Input.GetButtonDown("Action"))
            {
                if(main.PlayerInLayer == firstLayer)
                {
                    main.PlayerInLayer = secondLayer;
                } else
                {
                    main.PlayerInLayer = firstLayer;
                }
            }
        }
	}
}
