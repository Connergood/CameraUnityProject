using UnityEngine;
using System.Collections;

public class mSneak : Monster {

    bool active = false;

	// Use this for initialization
	void Start () {
        self.transform.position = initialPos;
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.onCamera)
        {
            active = true;
        }
        if (state == State.offCamera && active == true)
        {
            Vector2 d = vToPlayer();
            self.transform.position = new Vector3(self.transform.position.x + d.x, self.transform.position.y + d.y, self.transform.position.z);
        }
        cameraCheck();
        checkCollision();
    }

    Vector2 vToPlayer()
    {
        Vector2 vTowards = player.transform.position - self.transform.position;
        vTowards.Normalize();
        vTowards /= 20;
        return vTowards;
    }
}
