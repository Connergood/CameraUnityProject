using UnityEngine;
using System.Collections;

public class RailShots : MonoBehaviour {

    public float distanceToTravel = 30.0f;
    public float heightOfRoom = 6.0f;
    public float rateOfFire = 2.0f;
    public float speedOfRail = 5.0f;
    public int spots = 4;
    float timeSinceLastFire = 0.0f;
    public GameObject rail;
    Vector3 position;


    // Use this for initialization
    void Start () {
        position = transform.position;
        fire();
	}

    // Update is called once per frame
    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire > rateOfFire)
        {
            fire();
            timeSinceLastFire = 0;
        }
    }

    void fire(){
        int rails = Random.Range(1, 3);
        int pos = Random.Range(1, 2*spots+1);
        switch (rails)
        {
            case 1:
                GameObject rail0 = Instantiate(rail);
                rail0.transform.position = new Vector3(position.x, (position.y - heightOfRoom / 2) + heightOfRoom / pos, 0);
                rail0.GetComponent<Rigidbody2D>().velocity = new Vector3(speedOfRail, 0, 0);
                DestroyObject(rail0, Mathf.Abs(distanceToTravel / speedOfRail));
                break;
            case 2:
                GameObject rail1 = Instantiate(rail);
                GameObject rail2 = Instantiate(rail);
                int x = spots + 3; 
                rail1.transform.position = new Vector3(position.x, position.y - heightOfRoom / 2 + heightOfRoom / pos, 0);
                rail2.transform.position = new Vector3(position.x, position.y - heightOfRoom / 2 + heightOfRoom / pos + 3, 0);
                rail1.GetComponent<Rigidbody2D>().velocity = new Vector3(speedOfRail, 0, 0);
                rail2.GetComponent<Rigidbody2D>().velocity = new Vector3(speedOfRail, 0, 0);
                DestroyObject(rail1, Mathf.Abs(distanceToTravel / speedOfRail));
                DestroyObject(rail2, Mathf.Abs(distanceToTravel / speedOfRail));
                break;
            case 3:
                GameObject rail3 = Instantiate(rail);
                GameObject rail4 = Instantiate(rail);
                GameObject rail5 = Instantiate(rail);
                int y = Random.Range(1, spots);
                int z = Random.Range(1, spots);
                rail3.transform.position = new Vector3(position.x, position.y - heightOfRoom / 2 + heightOfRoom / pos, 0);
                rail4.transform.position = new Vector3(position.x, position.y - heightOfRoom / 2 + heightOfRoom / z, 0);
                rail5.transform.position = new Vector3(position.x, position.y - heightOfRoom / 2 + heightOfRoom / y, 0);
                rail3.GetComponent<Rigidbody2D>().velocity = new Vector3(speedOfRail, 0, 0);
                rail4.GetComponent<Rigidbody2D>().velocity = new Vector3(speedOfRail, 0, 0);
                rail5.GetComponent<Rigidbody2D>().velocity = new Vector3(speedOfRail, 0, 0);
                DestroyObject(rail3, Mathf.Abs(distanceToTravel / speedOfRail));
                DestroyObject(rail4, Mathf.Abs(distanceToTravel / speedOfRail));
                DestroyObject(rail5, Mathf.Abs(distanceToTravel / speedOfRail));
                break;
        }

    }
}