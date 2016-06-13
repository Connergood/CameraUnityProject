using UnityEngine;
using System.Collections;

public class PressurePad : MonoBehaviour {

	[SerializeField] GameObject self;
	[SerializeField] GameObject cubeToMove;
    [SerializeField] bool isActivatedByCamera;

	[SerializeField] float translateX;
	[SerializeField] float translateY;
	[SerializeField] float rotate;

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
                StartCoroutine(MoveCubeTo(2.0f));
                StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * rotate, 2.0f));
                StartCoroutine(Wait());
                MeshRenderer mr = self.GetComponent<MeshRenderer>();
                Material m = mr.material;
                m.color = Color.red;
            }
        } else if (isActivatedByCamera)
        {
            if (!(Mathf.Abs(camera.transform.position.x - self.transform.position.x) < 2.5 &&
                Mathf.Abs(camera.transform.position.y - self.transform.position.y) < 2.5) &&
                    type == State.weighted)
            {
                activated = false;
                StopAllCoroutines();
                StartCoroutine(MoveCubeFrom(3.0f));
                StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * -rotate, 2.0f));
                MeshRenderer mr = self.GetComponent<MeshRenderer>();
                Material m = mr.material;
                m.color = Color.magenta;
                StartCoroutine(Wait());
            }
        }
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.transform.tag == "Player" || collision.transform.tag == "Weight"){
			if (!activated && !isActivatedByCamera){
				StopAllCoroutines();
				StartCoroutine(MoveCubeTo(2.0f));
				StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * rotate, 2.0f)) ;
				activated = true;
				StartCoroutine(Wait ());
				SpriteRenderer sr = self.GetComponentInChildren<SpriteRenderer>();
                sr.color = new Color(255, 0, 0);
            }
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if ((collision.transform.tag == "Player" || collision.transform.tag == "Weight")){
			if (activated && type == State.weighted){
				StopAllCoroutines();
				StartCoroutine(MoveCubeFrom(3.0f));
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
