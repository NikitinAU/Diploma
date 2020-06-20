using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
public class MoveCamera : MonoBehaviour
{
    public Animator[] UIAnimator;
    GameObject maincamera;
    public Button pause;
    public bool done=false;
    private void Start()
    {
       maincamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (done == false)
        {
            StartCoroutine(HoldUp());
        }
    }
    IEnumerator HoldUp()
    {
        pause.enabled = false;
        maincamera.GetComponent<ControlCamera>().MoveSmooth = true;
        Cutscene(true);
        maincamera.GetComponent<PositionConstraint>().constraintActive = false;
        maincamera.GetComponent<ControlCamera>().MoveToNewView(1);
        yield return new WaitForSeconds(5f);
        maincamera.GetComponent<ControlCamera>().MoveToNewView(0);
        yield return new WaitForSeconds(5f);
        Cutscene(false);
        maincamera.GetComponent<LookAtConstraint>().constraintActive = true;
        maincamera.GetComponent<LookAtConstraint>().constraintActive = false;
        maincamera.GetComponent<PositionConstraint>().constraintActive = true;
        maincamera.GetComponent<ControlCamera>().MoveSmooth = false;
        pause.enabled = true;
    }
    void Cutscene(bool fadein)
    {
        for (int i = 0; i<UIAnimator.Length;i++)
        {
            UIAnimator[i].SetBool("DoFade", fadein);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        done = true;
        StopAllCoroutines();
    }
}
