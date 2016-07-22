using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class EndLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(ChangeLevel());
        }
    }

    IEnumerator ChangeLevel()
    {
        float fadeTime = GameObject.Find("Main").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
