﻿using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	[SerializeField] GameObject self;

	// Use this for initialization
	void Start () {
		self.transform.position = new Vector3 (0.0f, 0.0f, -10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("a")) {
			self.transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
		}
		if (Input.GetKey("d")) {
			self.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
		}
		if (Input.GetKey("w")) {
			self.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
		}
		if (Input.GetKey("s")) {
			self.transform.position += new Vector3(0.1f, -0.1f, 0.0f);
		}
	}
}
