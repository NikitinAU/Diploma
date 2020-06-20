using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public float activationDistance=2;
    
    private void Start()
    {
        if (activationDistance < 1)
            activationDistance = 1;
    }
}
