using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {

    public bool active = false;
    GameObject dialogueBox;
    GameObject nameText;
    GameObject dialogueText;
    public List<string> speech;
    GameObject[] image;
    public float textDelay = .01f;
    float timeLeft;
    string textToDisplay;
    int letterIndex = 0;
    int index = 0;

    enum SpeechState
    {
        writing,
        finished
    }

    SpeechState state;

    // Use this for initialization
    void Start () {
        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        nameText = GameObject.FindGameObjectWithTag("Speaker");
        dialogueText = GameObject.FindGameObjectWithTag("Dialogue");
        timeLeft = textDelay;
    }
	
	// Update is called once per frame
	void Update () {
        nameText.GetComponent<TextMesh>().text = speech[(index*2)];
        timeLeft -= Time.deltaTime; 
        if(timeLeft <= 0.0f && letterIndex < speech[(index*2)+1].Length)
        {
            textToDisplay += speech[(index * 2) + 1][letterIndex];
            timeLeft = textDelay;
            letterIndex++;
        }
        dialogueText.GetComponent<TextMesh>().text = textToDisplay;
        for (int i = 0; i < image.Length; i++)
        {
           
            if (i == index)
            {
                image[i].transform.position = new Vector3(image[i].transform.position.x, image[i].transform.position.y, -12.0f);
            } else if (image[i] != image[index])
            {
                image[i].transform.position = new Vector3(image[i].transform.position.x, image[i].transform.position.y, -11.0f);
            }
        }

        if(!active)
        {
            GameObject.FindGameObjectWithTag("Canvas").transform.position = new Vector2(50, 50);
            letterIndex = 0;
            textToDisplay = "";
            
        } else
        {
            GameObject.FindGameObjectWithTag("Canvas").transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        }
        
    }

    public void ActivateDialogue(List<string> nameSpeech, GameObject[] images)
    {
        active = true;
        speech = nameSpeech;
        index = 0;
        image = images;
    }

    public void OnClick()
    {
        index++;
        letterIndex = 0;
        textToDisplay = "";
        if (speech.Count < (index * 2) + 1)
        {
            index = 0;
            letterIndex = 0;
            textToDisplay = "";
            active = false;
            //GameObject.Find("Main").GetComponent<Main>().lockThingsInPlace(false);
        }
    }
}
