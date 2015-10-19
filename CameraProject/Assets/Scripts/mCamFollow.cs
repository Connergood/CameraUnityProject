using UnityEngine;
using System.Collections;

public class mCamFollow : Monster {

	// Use this for initialization
	public mCamFollow () {
        self.transform.position = initialPos;
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.onCamera)
        {
            Vector2 d = vToCamera();
            self.transform.position = new Vector3(self.transform.position.x + d.x, self.transform.position.y + d.y, self.transform.position.z);
        }
        cameraCheck();
        checkCollision();
    }

    Vector2 vToCamera()
    {
        Vector2 vTowards = camera.transform.position - self.transform.position;
        vTowards.Normalize();
        vTowards /= 15;
        return vTowards;
    }


}
