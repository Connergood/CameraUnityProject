using UnityEngine;
using System.Collections;

public class mSneak : MonoBehaviour {

	[SerializeField] GameObject self;
	Camera camera;
	GameObject player;
    [SerializeField] float speed = 25.0f;

    public enum State
	{
		onCamera,
		offCamera
	}
	public State state;

	bool active;
	//Use this for initialization
	void Start (){
		active = false;
		state = State.offCamera;
		player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		cameraCheck();
		if (state == State.onCamera)
		{
			active = true;
		}
		if (state == State.offCamera && active == true && !player.GetComponent<PlayerControl>().playerHidden)
		{
			Vector2 d = vToPlayer();
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(d.x * speed, d.y * speed);
		} else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
	}
	
	Vector2 vToPlayer()
	{
		Vector2 vTowards = player.transform.position - this.transform.position;
		vTowards.Normalize();
		vTowards /= 20;
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
