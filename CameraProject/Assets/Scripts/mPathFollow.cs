using UnityEngine;
using System.Collections;

public class mPathFollow : MonoBehaviour {


    [SerializeField] GameObject self;
    Camera camera;
    GameObject player;
    float timeToWait;
    public float playerDist;
    public GameObject target;
    public float pathFollowSpeed = 25.0f;
    public float playerChaseSpeed = 25.0f;

    public enum State
    {
        seekWaypoint,
        chasePlayer,
        wait
    }
    public State state;

    bool active;
    //Use this for initialization
    void Start()
    {
        active = false;
        state = State.seekWaypoint;
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeToWait = target.GetComponent<waypoint>().timeTillNext;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.seekWaypoint)
        {
            Vector2 d = Seek(target);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(d.x * pathFollowSpeed, d.y * pathFollowSpeed);
            if ((target.transform.position - self.transform.position).magnitude < .5f)
            {
                state = State.wait;
            }
        } else if (state == State.wait)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            timeToWait -= Time.deltaTime;
            if (timeToWait <= 0.0f)
            {
                target = target.GetComponent<waypoint>().Next;
                timeToWait = target.GetComponent<waypoint>().timeTillNext;
                state = State.seekWaypoint;
            }
        } else if (state == State.chasePlayer)
        {
            if ((target.transform.position - player.transform.position).magnitude > playerDist)
            {
                state = State.wait;
            }
            Vector2 d = Seek(player);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(d.x * playerChaseSpeed, d.y * playerChaseSpeed);
        }

        if((target.transform.position - player.transform.position).magnitude <= playerDist)
        {
            state = State.chasePlayer;
        }
    }

    Vector2 Seek(GameObject s)
    {
        Vector2 vTowards = s.transform.position - this.transform.position;
        vTowards.Normalize();
        vTowards /= 20;
        return vTowards;
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
