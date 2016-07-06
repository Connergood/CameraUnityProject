using UnityEngine;
using System.Collections;

public class BoilerPlatform : MonoBehaviour {

    public float speed = 3;
    public float targetX;
    public float directionX;
    public float targetY;
    public float directionY;
    public bool start;

    Vector2 initialPos;

    // Use this for initialization
    void Start()
    {
        start = false;
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == true)
        {
            transform.position = new Vector2(directionX * Mathf.PingPong(Time.time * speed, targetX) + initialPos.x, directionY * Mathf.PingPong(Time.time * speed, targetY) + initialPos.y);
            if (directionX == 0)
            {
                transform.position = new Vector2(initialPos.x, transform.position.y);
            }
            if (directionY == 0)
            {
                transform.position = new Vector2(transform.position.x, initialPos.y);
            }
        }
    }
}
