using UnityEngine;
using System.Collections;

public class PressurePad : MonoBehaviour {

	[SerializeField] GameObject self;
	[SerializeField] GameObject cubeToMove;

	[SerializeField] float translateX;
	[SerializeField] float translateY;
	[SerializeField] float rotate;

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
		to = new Vector3(cubeToMove.transform.position.x + translateX, cubeToMove.transform.position.y + translateY, cubeToMove.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision){
		if (collision.transform.tag == "Player" || collision.transform.tag == "Weight"){
			if (!activated){
				StopAllCoroutines();
				StartCoroutine(MoveCubeTo(2.0f));
				StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * rotate, 2.0f)) ;
				activated = true;
				self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y/2, self.transform.localScale.z);
				StartCoroutine(Wait ());
			}
		}
	}

	void OnCollisionExit(Collision collision){
		if ((collision.transform.tag == "Player" || collision.transform.tag == "Weight")){
			if (activated && type == State.weighted){
				StopAllCoroutines();
				StartCoroutine(MoveCubeFrom(3.0f));
				StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * -rotate, 2.0f)) ;
				activated = false;
				self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y*2, self.transform.localScale.z);
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
