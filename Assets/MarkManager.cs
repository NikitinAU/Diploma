using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkManager : MonoBehaviour
{
    public List<int> Marks;
    public float LvlOverallMark;
    private void Start()
    {
        Marks = new List<int>();
    }
    public void CountLvlOverallMark()
    {
        if (Marks.Count != 0)
        {
            float divider = 0;
            foreach (int mark in Marks)
            {
                LvlOverallMark += mark;
                divider+=1f;
            }
            LvlOverallMark /= divider;
        }
    }   

}
