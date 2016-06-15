using UnityEngine;
using System.Collections;

public class InvisibleWallTrigger : MonoBehaviour {

    [SerializeField] GameObject[] wallsToBringIn;
    [SerializeField] GameObject[] wallsToRemove;
    bool activateWalls = false;
    public bool removeWalls = false;
    GameObject Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (activateWalls)
        {
            for (int i = 0; i < wallsToBringIn.Length; i++)
            {
                if(wallsToBringIn[i] != null)
                {
                    wallsToBringIn[i].GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
        } else
        {
            for (int i = 0; i < wallsToBringIn.Length; i++)
            {
                if (wallsToBringIn[i] != null)
                {
                    wallsToBringIn[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
        }
        if (removeWalls)
        {
            for (int i = 0; i < wallsToRemove.Length; i++)
            {
                if (wallsToRemove[i] != null)
                {
                    wallsToRemove[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
        }

        if((Player.transform.position - transform.position).magnitude < 1)
        {
            activateWalls = true;
        }

    }
}
