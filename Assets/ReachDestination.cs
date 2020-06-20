using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachDestination : MonoBehaviour
{
    PPlayer p;
    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<PPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        p.doTask(this.gameObject.GetComponent<QuestObj>().givesProgress);
    }
}
