using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	[SerializeField] GameObject self;
	Rigidbody rigidBody;

	public enum State{
		normal,
		jumping
	}
	public State state;

	// Use this for initialization
	void Start () {
		self.transform.position = new Vector3 (0.0f, 0.0f, 16.0f);
		rigidBody = self.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("left")) {
			self.transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
		}
		if (Input.GetKey("right")) {
			self.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
		}
		if (Input.GetKeyDown("up") && state == State.normal) {
			state = State.jumping;
			rigidBody.AddForce(Vector3.up *350.0f);
		}
	}

	void OnCollisionEnter(Collision collision){
		if ((collision.transform.tag == "Ground" || collision.transform.tag == "PressurePad") && state == State.jumping){
			state = State.normal;
		}
	}
}
