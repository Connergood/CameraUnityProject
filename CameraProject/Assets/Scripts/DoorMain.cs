using UnityEngine;
using System.Collections;

public class DoorMain : MonoBehaviour {

    public GameObject[] Layer1;
    public GameObject[] Layer2;
    public GameObject[] Layer3;
    public GameObject[] Layer4;

    GameObject[] Doors;

    public int PlayerInLayer = 1;

    // Use this for initialization
    void Start () {
        Doors = GameObject.FindGameObjectsWithTag("Door");
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerInLayer == 1)
        {
            for (int i = 0; i < Layer1.Length; i++)
            {

                BoxCollider2D[] boxes = Layer1[i].GetComponentsInChildren<BoxCollider2D>();
                SpriteRenderer[] sprites = Layer1[i].GetComponentsInChildren<SpriteRenderer>();
                MeshRenderer[] meshes = Layer1[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = true;
                }
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = true;
                }
                for (int j = 0; j < meshes.Length; j++)
                {
                    meshes[j].enabled = true;
                }
            }
        }
        else {
            for (int i = 0; i < Layer1.Length; i++)
            {
                //ThingsToReveal[i].SetActive(true);

                BoxCollider2D[] boxes = Layer1[i].GetComponentsInChildren<BoxCollider2D>();
                SpriteRenderer[] sprites = Layer1[i].GetComponentsInChildren<SpriteRenderer>();
                MeshRenderer[] meshes = Layer1[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = false;
                }
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = false;
                }
                for (int j = 0; j < meshes.Length; j++)
                {
                    meshes[j].enabled = false;
                }
            }
        }
        if (PlayerInLayer == 2)
        {
            for (int i = 0; i < Layer2.Length; i++)
            {

                BoxCollider2D[] boxes = Layer2[i].GetComponentsInChildren<BoxCollider2D>();
                SpriteRenderer[] sprites = Layer2[i].GetComponentsInChildren<SpriteRenderer>();
                MeshRenderer[] meshes = Layer2[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = true;
                }
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = true;
                }
                for (int j = 0; j < meshes.Length; j++)
                {
                    meshes[j].enabled = true;
                }
            }
        }
        else {
            for (int i = 0; i < Layer2.Length; i++)
            {
                //ThingsToReveal[i].SetActive(true);

                BoxCollider2D[] boxes = Layer2[i].GetComponentsInChildren<BoxCollider2D>();
                SpriteRenderer[] sprites = Layer2[i].GetComponentsInChildren<SpriteRenderer>();
                MeshRenderer[] meshes = Layer2[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = false;
                }
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = false;
                }
                for (int j = 0; j < meshes.Length; j++)
                {
                    meshes[j].enabled = false;
                }
            }
        }
        if (PlayerInLayer == 3)
        {
            for (int i = 0; i < Layer3.Length; i++)
            {

                BoxCollider2D[] boxes = Layer3[i].GetComponentsInChildren<BoxCollider2D>();
                SpriteRenderer[] sprites = Layer3[i].GetComponentsInChildren<SpriteRenderer>();
                MeshRenderer[] meshes = Layer3[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = true;
                }
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = true;
                }
                for (int j = 0; j < meshes.Length; j++)
                {
                    meshes[j].enabled = true;
                }
            }
        }
        else {
            for (int i = 0; i < Layer3.Length; i++)
            {
                //ThingsToReveal[i].SetActive(true);

                BoxCollider2D[] boxes = Layer3[i].GetComponentsInChildren<BoxCollider2D>();
                SpriteRenderer[] sprites = Layer3[i].GetComponentsInChildren<SpriteRenderer>();
                MeshRenderer[] meshes = Layer3[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = false;
                }
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = false;
                }
                for (int j = 0; j < meshes.Length; j++)
                {
                    meshes[j].enabled = false;
                }
            }
        }
        if (PlayerInLayer == 4)
        {
            for (int i = 0; i < Layer4.Length; i++)
            {

                BoxCollider2D[] boxes = Layer4[i].GetComponentsInChildren<BoxCollider2D>();
                SpriteRenderer[] sprites = Layer4[i].GetComponentsInChildren<SpriteRenderer>();
                MeshRenderer[] meshes = Layer4[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = true;
                }
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = true;
                }
                for (int j = 0; j < meshes.Length; j++)
                {
                    meshes[j].enabled = true;
                }
            }
        }
        else {
            for (int i = 0; i < Layer4.Length; i++)
            {
                //ThingsToReveal[i].SetActive(true);

                BoxCollider2D[] boxes = Layer4[i].GetComponentsInChildren<BoxCollider2D>();
                SpriteRenderer[] sprites = Layer4[i].GetComponentsInChildren<SpriteRenderer>();
                MeshRenderer[] meshes = Layer4[i].GetComponentsInChildren<MeshRenderer>();
                for (int j = 0; j < boxes.Length; j++)
                {
                    boxes[j].enabled = false;
                }
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = false;
                }
                for (int j = 0; j < meshes.Length; j++)
                {
                    meshes[j].enabled = false;
                }
            }
        }

        for (int i = 0; i < Doors.Length; i++)
        {
            if(Doors[i].GetComponent<Door>().firstLayer == PlayerInLayer || Doors[i].GetComponent<Door>().secondLayer == PlayerInLayer)
            {
                Doors[i].SetActive(true);
            } else
            {
                Doors[i].SetActive(false);
            }
        }
    }
}
