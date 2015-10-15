using UnityEngine;
using System.Collections;

public class PressurePad : MonoBehaviour {

	[SerializeField] GameObject self;
	[SerializeField] GameObject cubeToMove;

	[SerializeField] float translateX;
	[SerializeField] float translateY;
	[SerializeField] float rotate;

	public enum Type{
		weighted, //Don't quite know how to implement this yet
		oneTime
	}
	public Type type;

	private bool activated;

	Vector3 initialCubeLocation;

	// Use this for initialization
	void Start () {
		initialCubeLocation = cubeToMove.transform.position;
		activated = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision){
		if (collision.transform.tag == "Player" && type == Type.oneTime){
			if (!activated){
				/*Vector3 location = new Vector3(initialCubeLocation.x + translateX,
				                               initialCubeLocation.y + translateY,
				                               initialCubeLocation.z);
				cubeToMove.transform.position = location;*/
				StartCoroutine(MoveCube(new Vector3(translateX, translateY, 0.0f), 3.0f));
				StartCoroutine(RotateCube(new Vector3(0.0f, 0.0f, 1.0f) * rotate, 3.0f)) ;
				//cubeToMove.transform.RotateAround(cubeToMove.transform.position, new Vector3(0.0f, 0.0f, 1.0f),rotate);
				activated = true;
				self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y/2, self.transform.localScale.z);
			}
		}
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

	IEnumerator MoveCube(Vector3 distance, float inTime)
	{
		Vector3 from = cubeToMove.transform.position;
		Vector3 to = cubeToMove.transform.position + distance;
		for(float t = 0f ; t < 1f ; t += Time.deltaTime/inTime)
		{
			cubeToMove.transform.position = Vector3.Lerp(from, to, t);
			yield return null ;
		}
	}
}
