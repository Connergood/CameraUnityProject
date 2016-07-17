using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    GameObject player;
    public bool press;
    public float interactDist = .5f;
    string says;
	// Use this for initialization
	void Start () {
	    if (press)
        {
            says = "Press Q";
        } else
        {
            says = "Hold Q";
        }
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    if((player.transform.position - transform.position).magnitude < interactDist)
        {
            //GameObject.Find("Action Text").GetComponent<TextMesh>().text = says;
        }
	}
}
