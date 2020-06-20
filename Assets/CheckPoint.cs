using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    public SaveGame s;
    public TMPro.TextMeshProUGUI notify;
    Color c; PPlayer p;
    public int index;
    private void Start()
    {
        p = FindObjectOfType<PPlayer>();
        c = notify.color;
        c.a = 0;
        notify.color = c;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (p.CP != index)
        {
            s.SaveXML();
            StartCoroutine(TextFade());
            p.CP = index;
        }
    }
    IEnumerator TextFade()
    {
        c.a = 255;
        notify.color = c;
        yield return new WaitForSeconds(1f);
        for(int i=255;i>=0;i--)
        {
            c.a = i;
            notify.color = c;
            yield return null;
        }
    }
}
