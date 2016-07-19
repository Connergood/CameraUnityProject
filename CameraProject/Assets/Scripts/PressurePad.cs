using UnityEngine;
using System.Collections;

public class PressurePad : MonoBehaviour {

	[SerializeField] GameObject self;
	[SerializeField] GameObject cubeToMove;
    [SerializeField] bool isActivatedByCamera;
    [SerializeField] bool isOnlyActivatedByWeight = false;

    [SerializeField] float translateX;
	[SerializeField] float translateY;
	[SerializeField] float rotate;

    [SerializeField] bool isActionItemSwitch = false;
    [SerializeField] float distanceFromSwitch = 1.5f;

    [SerializeField] float speedOfTransition = 3.0f;

    GameObject camera;

	Vector3 to;

	public enum State{
		weighted,
		oneTime
	}
	public State type;

	private bool activated;

	Vector3 initialCubeLocation;

	// Use this for initialization
	void Start () {
		initialCubeLocation = cubeToMove.transform.position;
		activated = false;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
		to = new Vector3(cubeToMove.transform.position.x + translateX, cubeToMove.transform.position.y + translateY, cubeToMove.transform.position.z);
        if (isActivatedByCamera)
        {
            self.GetComponent<Renderer>().material.color = Color.magenta;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!activated && isActivatedByCamera)
        {
            if (Mathf.Abs(camera.transform.position.x - self.transform.position.x) < 2.5 &&
                Mathf.Abs(camera.transform.position.y - self.transform.position.y) < 2.5)
            {
                activated = true;
                StopAllCoroutines();
                StartCoroutine(MoveCubeTo(speedOfTransition));
                StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * rotate, 2.0f));
                StartCoroutine(Wait());
                SpriteRenderer mr = self.GetComponent<SpriteRenderer>();
                mr.color = Color.red;
            }
        } else if (isActivatedByCamera)
        {
            if (!(Mathf.Abs(camera.transform.position.x - self.transform.position.x) < 2.5 &&
                Mathf.Abs(camera.transform.position.y - self.transform.position.y) < 2.5) &&
                    type == State.weighted)
            {
                activated = false;
                StopAllCoroutines();
                StartCoroutine(MoveCubeFrom(speedOfTransition));
                StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * -rotate, 2.0f));
                SpriteRenderer mr = self.GetComponent<SpriteRenderer>();
                mr.color = Color.white;
                StartCoroutine(Wait());
            }
        }
        if (isActionItemSwitch && !activated)
        {
            if (Input.GetButton("Action") &&
                !GameObject.Find("Player").GetComponent<PlayerControl>().playerHidden &&
                Mathf.Abs(GameObject.Find("Player").transform.position.x - transform.position.x) < distanceFromSwitch + transform.localScale.x / 2 &&
                Mathf.Abs(GameObject.Find("Player").transform.position.y - transform.position.y) < distanceFromSwitch + transform.localScale.y / 2)
            {
                StopAllCoroutines();
                StartCoroutine(MoveCubeTo(speedOfTransition));
                StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * rotate, 2.0f));
                activated = true;
                StartCoroutine(Wait());
                SpriteRenderer sr = self.GetComponentInChildren<SpriteRenderer>();
                sr.color = new Color(255, 0, 0);
            }
        } else if (isActionItemSwitch && type == State.weighted && activated && !Input.GetButton("Action") ) {
            StopAllCoroutines();
            StartCoroutine(MoveCubeFrom(speedOfTransition));
            StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * -rotate, 2.0f));
            activated = false;
            SpriteRenderer sr = self.GetComponentInChildren<SpriteRenderer>();
            sr.color = new Color(255, 255, 255);
            StartCoroutine(Wait());
        } else if (isActionItemSwitch && type == State.weighted && 
            (Mathf.Abs(GameObject.Find("Player").transform.position.x - transform.position.x) > distanceFromSwitch + transform.localScale.x / 2 ||
                Mathf.Abs(GameObject.Find("Player").transform.position.y - transform.position.y) > distanceFromSwitch + transform.localScale.y / 2))
        {
            StopAllCoroutines();
            StartCoroutine(MoveCubeFrom(speedOfTransition));
            StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * -rotate, 2.0f));
            activated = false;
            SpriteRenderer sr = self.GetComponentInChildren<SpriteRenderer>();
            sr.color = new Color(255, 255, 255);
            StartCoroutine(Wait());
        }
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (((collision.transform.tag == "Player" && !isOnlyActivatedByWeight) || collision.transform.tag == "Weight") && !isActionItemSwitch){
			if (!activated && !isActivatedByCamera){
				StopAllCoroutines();
				StartCoroutine(MoveCubeTo(speedOfTransition));
				StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * rotate, 2.0f)) ;
				activated = true;
				StartCoroutine(Wait ());
				SpriteRenderer sr = self.GetComponentInChildren<SpriteRenderer>();
                sr.color = new Color(255, 0, 0);
            }
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if (((collision.transform.tag == "Player" && !isOnlyActivatedByWeight) || collision.transform.tag == "Weight") && !isActionItemSwitch)
        {
			if (activated && type == State.weighted){
				StopAllCoroutines();
				StartCoroutine(MoveCubeFrom(speedOfTransition));
				StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * -rotate, 2.0f)) ;
				activated = false;
                SpriteRenderer sr = self.GetComponentInChildren<SpriteRenderer>();
                sr.color = new Color(255, 255, 255);
                StartCoroutine(Wait ());
			}
		}
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (3.0f);
	}

	IEnumerator RotateCube(Vector3 byAngles, float inTime)
	{
		Quaternion fromAngle = cubeToMove.transform.rotation ;
		Quaternion toAngle = Quaternion.Euler(cubeToMove.transform.eulerAngles + byAngles) ;
		for(float t = 0f ; t < 1f ; t += Time.deltaTime/inTime)
		{
			cubeToMove.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t) ;
			yield return null ;
		}
	}

	//End Location Initial Position
	IEnumerator MoveCubeFrom(float inTime)
	{
		Vector3 start = cubeToMove.transform.position;
		for(float t = 0f ; t < 1f ; t += Time.deltaTime/inTime)
		{
			cubeToMove.transform.position = Vector3.Lerp(start, initialCubeLocation, t);
			yield return null ;
		}
	}
	//End Location To
	IEnumerator MoveCubeTo(float inTime)
	{
		Vector3 start = cubeToMove.transform.position;
		for(float t = 0f ; t < 1f ; t += Time.deltaTime/inTime)
		{
			cubeToMove.transform.position = Vector3.Lerp(start, to, t);
			yield return null ;
		}
	}
}
