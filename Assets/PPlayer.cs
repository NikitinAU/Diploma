using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPlayer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI playerGoldInfo, playerHealthInfo;
    public int gold = 100;
    public int CP;
    public Quest quest;
    QuestManager qm;
    public void Start()
    {
        qm = FindObjectOfType<QuestManager>();
        UpdatePlayerInfoBox();
    }
    public void doTask(int progress)
    {
        quest.goal.TalkedToTarget(progress);
        if (quest.goal.IsReached())
        {
            gold += quest.goldReward;
            qm.ShowQuestCompletionPanel();
        }
        UpdatePlayerInfoBox();
    }

    public void UpdatePlayerInfoBox()
    {
        playerGoldInfo.text = gold.ToString() + " G";
        playerHealthInfo.text = quest.mark.ToString();
    }
    public void QuestToNull()
    {
        quest = null;
    }
}