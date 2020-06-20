using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public FixedJoystick fixedJoystic;
    public GameObject next1, next2;
    public Slider volumeSlider;
    public Button action;
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown qualityDD;
    public bool inversed;
   public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        volumeSlider.value = volume;
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        qualityDD.value = qualityIndex;
    }
    public void UICtrlPos(bool inverse)
    {
        inversed = inverse;
        Vector3 posBuff = fixedJoystic.transform.position;
        fixedJoystic.transform.position = action.transform.position;
        action.transform.position = posBuff;
        posBuff = next1.transform.position;
        next1.transform.position = next2.transform.position;
        next2.transform.position = posBuff;
    }
}
