using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueNoQuest, dialogueQuest;
    public float activationDistance = 2f;
    public bool hasAnswer;
    public string rightAnswer, wrongAnswer, restartAnswer;
    public int rightPos, wrongPos, restartPos, qPos;
    private void Start()
    {
        if (activationDistance < 1)
            activationDistance = 1;
    }
}

    