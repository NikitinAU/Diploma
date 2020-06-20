using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    PPlayer p;
    SaveGame s;
    void Start()
    {
        p = FindObjectOfType<PPlayer>();
        s = FindObjectOfType<SaveGame>();
        //p.gameObject.transform.position = this.transform.position;
        //s.SaveXML();
    }

    
}
