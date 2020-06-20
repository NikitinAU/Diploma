using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    QuestManager qm;
    bool enabled = true;
    private void Start()
    {
        qm = FindObjectOfType<QuestManager>();
    }
    private void Update()
    {
        if (qm.questGivers[0].enabled == false && enabled == true)
        {
            MoveDown();
            enabled = false;
        }
    }
    public void MoveDown()
    { 
        this.transform.Translate(0, -5, 0);
    }
}
