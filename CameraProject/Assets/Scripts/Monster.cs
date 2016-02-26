using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	
	public GameObject self;
	public Camera camera;
	public GameObject player;
	
	public enum State
	{
		onCamera,
		offCamera
	}
	public State state;

	public Monster () {

		player = GameObject.FindWithTag("Player");
		//self = GameObject.FindWithTag ("Monster");
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
