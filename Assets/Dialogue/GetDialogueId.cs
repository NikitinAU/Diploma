using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetDialogueId : MonoBehaviour
{
    DialogueManager dm;
    public int id;
    public Image image;
    public bool good;
    Color c;
    PPlayer p;
    private void Start()
    {
        p = FindObjectOfType<PPlayer>();
        c = image.color;
        c.a = 0;
        image.color = c;
        dm = FindObjectOfType<DialogueManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        c.a = 255;
        dm.curTrigger = id;
        if (this.gameObject.GetComponent<DialogueTrigger>().dialogueNoQuest.sentences.Length !=0)
            image.color =c;
        if (p.quest.isActive)
        {
            if (p.quest.objects[dm.curObj] == this.gameObject.GetComponent<QuestObj>())
                image.color = c;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        c.a = 0;
        image.color = c;
    }
}
