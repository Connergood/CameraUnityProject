using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject DialogueCanvas;
    public bool activated;
    public List<string> dialogue;
    public GameObject[] speakerImages;
    GameObject player;
    GameObject doorMain;
    [SerializeField] GameObject EventObject;
    [SerializeField] bool keyActivated;
    [SerializeField] int layer;
    public bool lockPlayer = true;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doorMain = GameObject.FindGameObjectWithTag("DoorMain");
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            if (layer == doorMain.GetComponent<DoorMain>().PlayerInLayer)
            {
                if (keyActivated == false)
                {
                    if (EventObject == null)
                    {
                        if ((this.transform.position - player.transform.position).magnitude < 1.0f)
                        {
                            this.activated = true;
                            DialogueCanvas.GetComponent<Dialogue>().ActivateDialogue(dialogue, speakerImages);
                            for (int i = 0; i < dialogue.Count; i++)
                            {
                                dialogue[i] = dialogue[i].Replace("@", System.Environment.NewLine);
                            }
                            //GameObject.Find("Main").GetComponent<Main>().lockThingsInPlace(lockPlayer);
                        }
                    }
                    else if (EventObject != null)
                    {
                        if (EventObject.GetComponent<CameraObject>().isEvent == true)
                        {
                            if ((this.transform.position - player.transform.position).magnitude < 1.0f)
                            {
                                this.activated = true;
                                DialogueCanvas.GetComponent<Dialogue>().ActivateDialogue(dialogue, speakerImages);
                                for (int i = 0; i < dialogue.Count; i++)
                                {
                                    dialogue[i] = dialogue[i].Replace("@", System.Environment.NewLine);
                                }
                            }
                        }
                    }
                }
                else if (keyActivated == true)
                {
                    if (player.GetComponent<PlayerControl>().keyCount == 0)
                    {
                        if ((this.transform.position - player.transform.position).magnitude < 1.0f)
                        {
                            this.activated = true;
                            DialogueCanvas.GetComponent<Dialogue>().ActivateDialogue(dialogue, speakerImages);
                            for (int i = 0; i < dialogue.Count; i++)
                            {
                                dialogue[i] = dialogue[i].Replace("@", System.Environment.NewLine);
                            }
                        }
                    }
                }
            }
        }
    }
}
