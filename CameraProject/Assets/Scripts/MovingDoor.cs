using UnityEngine;
using System.Collections;

public class MovingDoor : MonoBehaviour {

    public float initialVelocityX;
    public float InitialVelocityY;
    Rigidbody2D myBody;

	// Use this for initialization
	void Start () {
        myBody = this.gameObject.GetComponent<Rigidbody2D>();
        myBody.velocity = new Vector2(initialVelocityX, InitialVelocityY);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D hit)
    {
        Vector2 norm = hit.contacts[0].normal;
        print(norm);
        if (Mathf.Abs(norm.x) > Mathf.Abs(norm.y))
        {
            if (norm.x > 0)
            {
                myBody.velocity = new Vector2(initialVelocityX, myBody.velocity.y);
            } else
            {
                myBody.velocity = new Vector2(-initialVelocityX, myBody.velocity.y);
            }
        } else
        {
            if(norm.y > 0)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, InitialVelocityY);
            } else
            {
                myBody.velocity = new Vector2(myBody.velocity.x, -InitialVelocityY);
            }
        }
        print(myBody.velocity);
    }
}
