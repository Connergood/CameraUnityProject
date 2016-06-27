using UnityEngine;
using System.Collections;

public class MovingDoor : MonoBehaviour {

    public float initialVelocityX;
    public float InitialVelocityY;
    public bool dontMoveX = false;
    public bool dontMoveY = false;
    Vector2 vel;

    Rigidbody2D myBody;

	// Use this for initialization
	void Start () {
        myBody = this.gameObject.GetComponent<Rigidbody2D>();
        myBody.velocity = new Vector2(initialVelocityX, InitialVelocityY);
        if (dontMoveX && dontMoveY)
        {
            myBody.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        } else if (dontMoveX)
        {
            myBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        } else if (dontMoveY)
        {
            myBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        vel = new Vector2(initialVelocityX, InitialVelocityY);
	}
	
	// Update is called once per frame
	void Update () {
        myBody.velocity = vel;
	}

    void OnCollisionEnter2D(Collision2D hit)
    {
        Vector2 norm = hit.contacts[0].normal;
        if (Mathf.Abs(norm.x) > Mathf.Abs(norm.y))
        {
            if (norm.x > 0)
            {
                myBody.velocity = vel =  new Vector2(initialVelocityX, myBody.velocity.y);
            } else
            {
                myBody.velocity = vel = new Vector2(-initialVelocityX, myBody.velocity.y);
            }
        } else
        {
            if(norm.y > 0)
            {
                myBody.velocity = vel = new Vector2(myBody.velocity.x, InitialVelocityY);
            } else
            {
                myBody.velocity = vel = new Vector2(myBody.velocity.x, -InitialVelocityY);
            }
        }
    }
}
