using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ControlCamera : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
    Transform curView;
    public bool MoveSmooth = false;
    void Start()
    {
        curView = views[0];
    }

    void Update()
    {
       
       if (MoveSmooth == true)
        {
            transform.position = Vector3.Lerp(transform.position, curView.position, Time.deltaTime * transitionSpeed);

            Vector3 curAngle = new Vector3(Mathf.LerpAngle(transform.rotation.eulerAngles.x, curView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, curView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, curView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));
            transform.eulerAngles = curAngle;
        }
    }
    public void MoveToNewView(int newView)
    {
        curView = views[newView];
    }
}
