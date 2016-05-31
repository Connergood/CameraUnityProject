using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject DialogueCanvas;
    public bool activated;
    public List<string> dialogue;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            if ((this.transform.position - player.transform.position).magnitude < 1.0f)
            {
                this.activated = true;
                DialogueCanvas.GetComponent<Dialogue>().ActivateDialogue(dialogue);
            }

        }
    }

}
