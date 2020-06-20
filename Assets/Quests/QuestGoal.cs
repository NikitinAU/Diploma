using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public int reqAmount, curAmount;
    public bool IsReached()
    {
        return (curAmount >= reqAmount);
    }

    public void TalkedToTarget(int progress)
    {
        if(goalType == GoalType.Talk)
        curAmount+=progress;
    }
    public string StringGoalType()
    {
        switch (goalType)
        {
            case GoalType.Defeat: return "Defeat";
            case GoalType.Gather: return "Gather";
            case GoalType.Learn: return "Learn";
            case GoalType.Find: return "Find";
            case GoalType.Talk: return "Talk";
            case GoalType.GoTo: return "GoTo";
            default: return "None";
        }
    }
    public void SetGoalTypeFromString(string Type)
    {
        switch(Type)
        {
            case "Defeat": { goalType = GoalType.Defeat; break; }
            case "Gather": { goalType = GoalType.Gather; break; }
            case "Learn": { goalType = GoalType.Learn; break; }
            case "Find": { goalType = GoalType.Find; break; }
            case "Talk": { goalType = GoalType.Talk; break; }
            case "GoTo": { goalType = GoalType.GoTo; break; }
            default: { goalType = GoalType.None; break; }
        }
    }
}

public enum GoalType
{
    None,
    Defeat,
    Gather,
    Learn,
    Find,
    Talk,
    GoTo
}
