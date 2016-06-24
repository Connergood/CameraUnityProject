using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

    public string reasonForDeath = "Player Fell To Their Death";


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        print(hit);
        if(hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerControl>().alive = false;
            GameObject.Find("Main").GetComponent<Main>().reason = reasonForDeath;
        }
    }
}
