using UnityEngine;
using System.Collections;

public class mCamFollow : MonoBehaviour {

	[SerializeField] GameObject self;
	[SerializeField] Camera camera;
	[SerializeField] Vector3 initialPos;
	[SerializeField] GameObject player;

	public enum State
	{
		onCamera,
		offCamera
	}
	public State state;

	void Start(){
		state = State.offCamera;
		self.transform.position = initialPos;
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.onCamera)
		{
			Vector2 d = vToCamera();
			this.transform.position = new Vector3(this.transform.position.x + d.x, this.transform.position.y + d.y, this.transform.position.z);
		}
		cameraCheck();
	}
	
	Vector2 vToCamera()
	{
		Vector2 vTowards = camera.transform.position - this.transform.position;
		vTowards.Normalize();
		vTowards /= 15;
		return vTowards;
	}

	public void cameraCheck()
	{
		Vector3 screenPoint = camera.WorldToViewportPoint(self.transform.position);
		if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
		{
			state = State.onCamera;
		}
		else
		{
			state = State.offCamera;
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Player")//((self.transform.position.x - player.transform.position.x) <=1 && (self.transform.position.y - player.transform.position.y) <= 1)
		{
			PlayerControl pc = (PlayerControl)player.GetComponent("PlayerControl");
			pc.alive = false;
			Main m = (Main)GameObject.Find("Main").GetComponent("Main");
			m.reason = "A Monster Got The Player";
		}
	}
	

}
