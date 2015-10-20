using UnityEngine;
using System.Collections;

public class mCamFollow : Monster {
	
	// Use this for initialization
	public mCamFollow () {
		//self.transform.position = initialPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.onCamera)
		{
			Vector2 d = vToCamera();
			this.transform.position = new Vector3(this.transform.position.x + d.x, this.transform.position.y + d.y, this.transform.position.z);
		}
		cameraCheck();
		//checkCollision();
	}
	
	Vector2 vToCamera()
	{
		Vector2 vTowards = GetComponent<Camera>().transform.position - this.transform.position;
		vTowards.Normalize();
		vTowards /= 15;
		return vTowards;
	}
	

}