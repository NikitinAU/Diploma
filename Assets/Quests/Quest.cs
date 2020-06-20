using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest
{
    public int index;
    public bool isActive;
    public string title, description;
    public int goldReward;
    public int mark=5;
    public QuestGoal goal;
    public QuestObj[] objects;
}
