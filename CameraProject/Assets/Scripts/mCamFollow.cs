using UnityEngine;
using System.Collections;

public class mCamFollow : MonoBehaviour {

	[SerializeField] GameObject self;
    public float speed = 25.0f;
	Camera camera;
	GameObject player;
    Vector2[] vectors = new Vector2[8];
    Vector2 chosenPoint;
    int chosenElement;
    bool reachedPoint = false;
    bool beenSeen = false;

    public enum State
	{
		onCamera,
		offCamera
	}
	public State state;

	void Start(){
		state = State.offCamera;
		player = GameObject.FindWithTag("Player");
        camera = (Camera)GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (beenSeen == true)
		{
            Points();
            if ((chosenPoint == new Vector2(0,0)) || (reachedPoint == true)) // if point hasn't been selected or if point has been reached
            {
                choosePoint();
            }
            chosenPoint = vectors[chosenElement]; //update current position of point
			Vector2 d = vToPoint();
			this.GetComponent<Rigidbody2D>().velocity = new Vector3(speed * d.x, speed * d.y);
		} else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        }
		cameraCheck();
	}

    public void Points()
    {
        // find bounds of camera
        float height = 2 * camera.orthographicSize;
        float width = height * camera.aspect;

        // find 8 points around border of camera
        Vector2 topLeftCorner = new Vector2(camera.transform.position.x - (width/2), camera.transform.position.y - (height / 2));
        Vector2 topRightCorner = new Vector2(camera.transform.position.x + (width / 2), camera.transform.position.y - (height / 2));
        Vector2 bottomLeftCorner = new Vector2(camera.transform.position.x - (width / 2), camera.transform.position.y + (height / 2));
        Vector2 bottomRightCorner = new Vector2(camera.transform.position.x + (width / 2), camera.transform.position.y + (height / 2));
        Vector2 topMidPoint = new Vector2(camera.transform.position.x, camera.transform.position.y - (height / 2));
        Vector2 bottomMidPoint = new Vector2(camera.transform.position.x, camera.transform.position.y + (height / 2));
        Vector2 leftMidPoint = new Vector2(camera.transform.position.x - (width / 2), camera.transform.position.y);
        Vector2 rightMidPoint = new Vector2(camera.transform.position.x + (width / 2), camera.transform.position.y);

        // add to array
        vectors[0] = topLeftCorner;
        vectors[1] = topRightCorner;
        vectors[2] = bottomLeftCorner;
        vectors[3] = bottomRightCorner;
        vectors[4] = topMidPoint;
        vectors[5] = bottomMidPoint;
        vectors[6] = leftMidPoint;
        vectors[7] = rightMidPoint;
    }

    public void choosePoint()
    {
        // randomly pick point to seek
        int r = Random.Range(0, vectors.Length);
        chosenElement = r;
        chosenPoint = vectors[r];
        reachedPoint = false; 
    }

    //seek point
    Vector2 vToPoint()
    {
        Vector2 vTowards = new Vector3(chosenPoint.x, chosenPoint.y, 0) - this.transform.position;
        if(vTowards.magnitude <= 1)
        {
            reachedPoint = true;
        }
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
            if(beenSeen == false)
            {
                beenSeen = true;
            }
		}
		else
		{
			state = State.offCamera;
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision)
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
