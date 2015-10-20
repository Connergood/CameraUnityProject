using UnityEngine;
using System.Collections;

public class mSneak : Monster {

	bool active;
	//Use this for initialization
	void Start (){
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
		cameraCheck();
		//checkCollision();
		if (state == State.onCamera)
		{
			active = true;
		}
		if (state == State.offCamera && active == true)
		{
			Vector2 d = vToPlayer();
			this.transform.position = new Vector3(this.transform.position.x + d.x, this.transform.position.y + d.y, this.transform.position.z);
		}
	}
	
	Vector2 vToPlayer()
	{
		Vector2 vTowards = player.transform.position - this.transform.position;
		vTowards.Normalize();
		vTowards /= 20;
		return vTowards;
	}
}
