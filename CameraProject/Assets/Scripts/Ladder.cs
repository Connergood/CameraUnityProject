using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

    GameObject Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Player.transform.position.x - transform.position.x) < .5f && Mathf.Abs(Player.transform.position.y - transform.position.y) < transform.localScale.y/2)
        {
            if(Input.GetButton("Action"))
            {
                Player.GetComponent<PlayerControl>().onLadder = true;
            }
        }
	}
}
