using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	[SerializeField] GameObject self;
	[SerializeField] Vector3 initialPosition;
	[SerializeField] float minY;
	Rigidbody rigidBody;
	ArrayList fallZones;
	
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
        self.transform.position = initialPosition;
        rigidBody = self.GetComponent<Rigidbody>();
		canMoveLeft = true;
		canMoveRight = true;
		alive = true;
		//fallZones.Add(self.transform.child
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
				rigidBody.AddForce (Vector3.up * 450.0f);
			}
		}
        if (alive == false)
        {

        }
	}
	
	void OnCollisionEnter(Collision collision){
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
	
	void OnCollisionExit(Collision collision){
		if (collision.transform.tag == "Wall"){
			if (collision.transform.position.x - 1 < self.transform.position.x - self.transform.localScale.x){
				canMoveLeft = true;
			} else if (collision.transform.position.x + 1 > self.transform.position.x + self.transform.localScale.x){
				canMoveRight = true;
			}
		}
	}
}
