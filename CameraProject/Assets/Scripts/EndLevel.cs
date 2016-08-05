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
        //sfloat fadeTime = GameObject.Find("Main").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(0.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
