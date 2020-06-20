using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotifyAboutAvaliableQuest : MonoBehaviour
{
    public GameObject text;
    public Image image;
    public bool active = true;
    private void Start()
    {
        text.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (active == true)
        {
            text.SetActive(true);
            image.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
        image.enabled = false;
    }
    public void Activated()
    {
        active = false;
        text.SetActive(false);
        image.enabled = false;
    }
    public void MakeActive()
    {
        active = true;
    }
}
