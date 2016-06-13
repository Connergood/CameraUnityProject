using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	[SerializeField] GameObject self;
	[SerializeField] float minY;
	Rigidbody2D rigidBody;

    public bool alive = true;
    public bool onLadder = false;
    public bool playerHidden = false;
	
	public enum State{
		normal,
		jumping
	}
	public State state;
	
	public bool canMoveLeft;
	public bool canMoveRight;
	
	// Use this for initialization
	void Start () {
        rigidBody = self.GetComponent<Rigidbody2D>();
		canMoveLeft = true;
		canMoveRight = true;
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
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
                    if (Input.GetKey("left") && canMoveLeft)
                    {
                        self.transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
                        self.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    if (Input.GetKey("right") && canMoveRight)
                    {
                        self.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
                        self.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    if (Input.GetKeyDown("up") && state == State.normal)
                    {
                        state = State.jumping;
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 35.0f);
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
            } else
            {
                rigidBody.isKinematic = true;
                if (Input.GetKeyUp("down"))
                {
                    playerHidden = false;
                }
            }
		}
        if (alive == false)
        {

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
        transform.position = ObjPos;
    }

    void OnCollisionEnter2D(Collision2D collision){
		if (collision.transform.tag == "Wall") {
			if (collision.transform.position.x < self.transform.position.x - self.transform.localScale.x) {
				canMoveLeft = false;
			} else if (collision.transform.position.x > self.transform.position.x + self.transform.localScale.x) {
				canMoveRight = false;
			}
		}
		
		if ((collision.transform.tag == "Ground" || collision.transform.tag == "PressurePad" || collision.transform.tag == "Weight") && state == State.jumping){
			state = State.normal;
		}
	}
	
	void OnCollisionExit2D(Collision2D collision){
		if (collision.transform.tag == "Wall"){
			if (collision.transform.position.x - 1 < self.transform.position.x - self.transform.localScale.x){
				canMoveLeft = true;
			} else if (collision.transform.position.x + 1 > self.transform.position.x + self.transform.localScale.x){
				canMoveRight = true;
			}
		}
	}
}
