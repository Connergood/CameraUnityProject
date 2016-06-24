using UnityEngine;
using System.Collections;

public class LockedObject : MonoBehaviour {

    [SerializeField]
    int KeysRequired = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            if(hit.gameObject.GetComponent<PlayerControl>().keyCount >= KeysRequired)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
