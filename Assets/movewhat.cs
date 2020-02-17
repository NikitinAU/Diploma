using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class movewhat : MonoBehaviour
{
    public FixedJoystick leftJoystick;
    public FixedButton Button;
    protected ThirdPersonUserControl ctrl;
    // Start is called before the first frame update
    void Start()
    {
        ctrl = GetComponent<ThirdPersonUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        ctrl.m_Jump = Button.Pressed;
        ctrl.HInput = leftJoystick.Horizontal;
        ctrl.VInput = leftJoystick.Vertical;
    }
}
