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
				Vector3 location = new Vector3(initialCubeLocation.x + translateX,
				                               initialCubeLocation.y + translateY,
				                               initialCubeLocation.z);
				cubeToMove.transform.position = location;
				cubeToMove.transform.RotateAround(cubeToMove.transform.position, new Vector3(0.0f, 0.0f, 1.0f),rotate);
				activated = true;
				self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y/2, self.transform.localScale.z);
			}
		}
	}
}
