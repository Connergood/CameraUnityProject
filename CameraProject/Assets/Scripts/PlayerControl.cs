using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    [SerializeField]
    GameObject self;
    [SerializeField]
    float minY;
    Rigidbody2D rigidBody;

    public bool alive = true;
    public bool onLadder = false;
    public bool playerHidden = false;

    Vector3 lockedPos;
    // Use this for initialization
    void Start()
    {
        rigidBody = self.GetComponent<Rigidbody2D>();
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (!playerHidden)
            {
                if (!onLadder)
                {
                    rigidBody.isKinematic = false;
                    if (self.transform.position.y < minY)
                    {
                        alive = false;
                        Main m = (Main)GameObject.Find("Main").GetComponent("Main");
                        m.reason = "The Player Fell Off The Map";
                        return;
                    }
                }
                else
                {
                    rigidBody.isKinematic = true;
                    if (Input.GetKey("left") || Input.GetKey("right"))
                    {
                        onLadder = false;
                    }
                    if (Input.GetKey("up"))
                    {
                        self.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
                    }
                    if (Input.GetKey("down"))
                    {
                        self.transform.position += new Vector3(0.0f, -0.1f, 0.0f);
                    }
                }
            }
            else
            {
                rigidBody.isKinematic = true;
                transform.position = lockedPos;
                if (!Input.GetButton("Action"))
                {
                    playerHidden = false;
                }
            }
        }
    }


    public void HidePlayer(string ObjName, Vector2 ObjPos)
    {
        playerHidden = true;
        //Animation Calls
        switch (ObjName)
        {
            case "Locker":
                break;
            case "Bed":
                break;
            case "Cart":
                break;
        }
        lockedPos = ObjPos;
    }
    void OnCollisionStay2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "Platform")
        {
            self.transform.parent = hit.transform;
        }
        else {
            self.transform.parent = null;
        }
    }
    void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "Platform")
        {
            self.transform.parent = null;
        }
    }
}
