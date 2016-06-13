using UnityEngine;
using System.Collections;

public class GrabAbleObject : MonoBehaviour {

    Vector2 difference;
    GameObject Player;
    [SerializeField] bool isGrabbable = false;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
        state = State.unlocked;
	}
	
    enum State
    {
        locked,
        unlocked
    }

    State state;

    // Update is called once per frame
    void Update()
    {
        if (isGrabbable)
        {
            if (Input.GetKeyDown(KeyCode.Q) &&
                !Player.GetComponent<PlayerControl>().playerHidden &&
                Mathf.Abs(difference.x) < 1 + transform.localScale.x/2 &&
                Mathf.Abs(difference.y) < 1 + transform.localScale.y/2)
            {
                state = State.locked;
                print("I am locked");
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                state = State.unlocked;
            }

            if (state == State.locked)
            {
                transform.position = new Vector3(Player.transform.position.x + difference.x, Player.transform.position.y + difference.y, 0.00f);
            }
            else
            {
                difference = new Vector2(transform.position.x - Player.transform.position.x,
                                                  transform.position.y - Player.transform.position.y);
            }
        }
    }
}