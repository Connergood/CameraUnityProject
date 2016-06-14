using UnityEngine;
using System.Collections;

public class HiddenPlace : MonoBehaviour {

    GameObject Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 playerPos = Player.transform.position;
        Vector2 myPos = transform.position;
        if((playerPos - myPos).magnitude < .75f)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Player.GetComponent<PlayerControl>().HidePlayer(gameObject.name, myPos);
            }
        }
	}
}
