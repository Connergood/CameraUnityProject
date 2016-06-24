using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject[] ThingsToHide;
    public GameObject[] ThingsToReveal;
    public bool inDoor = false;
    GameObject Player;
    public GameObject[] DoorsThatAttachToTheSameRooms;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pPos = Player.transform.position;
        Vector2 mPos = transform.position;
        if((pPos - mPos).magnitude < .5f)
        {
            if (Input.GetButtonDown("Action"))
            {
                inDoor = !inDoor;
            }
        }
        if (inDoor)
        {
            for (int i = 0; i < ThingsToHide.Length; i++)
            {
                ThingsToHide[i].SetActive(false);
            }
            for (int i = 0; i < ThingsToReveal.Length; i++)
            {
                ThingsToReveal[i].SetActive(true);
            }
        } else
        {
            for (int i = 0; i < ThingsToHide.Length; i++)
            {
                ThingsToHide[i].SetActive(true);
            }
            for (int i = 0; i < ThingsToReveal.Length; i++)
            {
                ThingsToReveal[i].SetActive(false);
            }
        }
        for (int i = 0; i < DoorsThatAttachToTheSameRooms.Length; i++)
        {
            DoorsThatAttachToTheSameRooms[i].GetComponent<Door>().inDoor = inDoor;
        }
	}
}
