using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene(string name)
    {
        StartCoroutine(ChangeLevel(name));
    }

    IEnumerator ChangeLevel(string name)
    {
        float fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }
}
