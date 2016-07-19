using UnityEngine;
using System.Collections;

public class BoilerSpace : MonoBehaviour {

    int coalUsed;
    GameObject boiler;
    public GameObject[] platOnes;
    public GameObject platTwo;
    public GameObject DT1;
    public GameObject DT2;
    public GameObject DT3;
    public GameObject BlockedPassage;
    GameObject Player;

    // Use this for initialization
    void Start () {
        coalUsed = 0;
        boiler = GameObject.FindGameObjectWithTag("Boiler");
        Player = GameObject.Find("Player");
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
            DT1.transform.position = Player.transform.position;
        }
        else if(coalUsed == 2)
        {
            platTwo.GetComponent<BoilerPlatform>().start = true;
            DT2.transform.position = Player.transform.position;
        } else if (coalUsed == 3)
        {
            DT3.transform.position = Player.transform.position;
            Destroy(BlockedPassage);
        }

    }
}
