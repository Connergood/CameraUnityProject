using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }
}
