using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour{

    public GameObject self;
    public Vector3 initialPos;
    public Camera camera;
    public GameObject player;

    public enum State
    {
        onCamera,
        offCamera
    }

    public State state;

	// Use this for initialization
	public Monster () {
        self.transform.position = initialPos; 
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

    public void checkCollision()
    {
        if ((self.transform.position.x - player.transform.position.x) <=1 && (self.transform.position.y - player.transform.position.y) <= 1)
        {
            player.alive = false;
        }
    }
}
