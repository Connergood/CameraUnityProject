using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	[SerializeField] GameObject self;

	public bool active = true;
    float moveX;
    float moveY;
    public float maxX = 50.0f;
    public float maxY = 50.0f;
    public float minX = -50.0f;
    public float minY = -50.0f;
    public float speed = 5.0f;

	// Use this for initialization
	void Start () {
        self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y, -15.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //Camera movement.... Yay!
        if (active)
        {
            /*if (Input.GetKey("a")) {
				self.transform.position += new Vector3 (-0.1f, 0.0f, 0.0f);
			}
			if (Input.GetKey ("d")) {
				self.transform.position += new Vector3 (0.1f, 0.0f, 0.0f);
			}
			if (Input.GetKey ("w")) {
				self.transform.position += new Vector3 (0.0f, 0.1f, 0.0f);
			}
			if (Input.GetKey ("s")) {
				self.transform.position += new Vector3 (0.0f, -0.1f, 0.0f);
			}*/
            moveX = Input.GetAxis("Horizontal Camera");
            moveY = Input.GetAxis("Vertical Camera");
        }
	}

    void FixedUpdate()
    {
        self.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, moveY * speed);
        if(maxX < transform.position.x)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        if (minX > transform.position.x)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        if (maxY < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        if (minY > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
    }
}
