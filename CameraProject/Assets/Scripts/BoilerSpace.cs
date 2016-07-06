using UnityEngine;
using System.Collections;

public class BoilerSpace : MonoBehaviour {

    int coalUsed;
    GameObject boiler;
    GameObject[] platOnes;
    GameObject platTwo;

	// Use this for initialization
	void Start () {
        coalUsed = 0;
        boiler = GameObject.FindGameObjectWithTag("Boiler");
        platOnes = GameObject.FindGameObjectsWithTag("PlatOne");
        platTwo = GameObject.FindGameObjectWithTag("PlatTwo");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Coal")
        {
            if (hit.gameObject.GetComponent<IsActive>().active == true)
            {
                hit.gameObject.GetComponent<IsActive>().active = false;
                coalUsed += 1;
                boiler.GetComponent<TextMesh>().text = coalUsed.ToString();
            }
        }

        if (coalUsed == 1)
        {
            for(int i = 0; i < platOnes.Length; i++)
            {
                platOnes[i].GetComponent<BoilerPlatform>().start = true;
            }
        }
        else if(coalUsed == 2)
        {
            platTwo.GetComponent<BoilerPlatform>().start = true;
        }
    }
}
