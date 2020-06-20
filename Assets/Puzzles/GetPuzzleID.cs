using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPuzzleID : MonoBehaviour
{
    public int id;
    GameObject pm;
    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("PuzzleManager");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        pm.GetComponent<PuzzleManager>().curPuzzle = id;
    }
}
