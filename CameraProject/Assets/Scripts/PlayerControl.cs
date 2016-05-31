using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	[SerializeField] GameObject self;
	[SerializeField] float minY;
	Rigidbody2D rigidBody;
	
	public bool alive = true;
	
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
			if (self.transform.position.y < minY){
				alive = false;
				Main m = (Main)GameObject.Find("Main").GetComponent("Main");
				m.reason = "The Player Fell Off The Map";
				return;
			}
			if (Input.GetKey ("left") && canMoveLeft) {
				self.transform.position += new Vector3 (-0.1f, 0.0f, 0.0f);
			}
			if (Input.GetKey ("right") && canMoveRight) {
				self.transform.position += new Vector3 (0.1f, 0.0f, 0.0f);
			}
			if (Input.GetKeyDown ("up") && state == State.normal) {
				state = State.jumping;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 35.0f);
			}
		}
        if (alive == false)
        {

        }
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
