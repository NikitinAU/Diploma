using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public QuestGiver[] questGivers;
    public TMPro.TextMeshProUGUI title, description, reward, completed, showMark;
    public MarkManager mm;
    public SphereCollider notifyier;
    public GameObject QCP, QP, LvlCP;
    PPlayer p;
    public int curQuestIndex=0;
    GameObject player,gsm;

    private void Start()
    {
        int qID = FindQuestIDByCurrentQuestIndex(curQuestIndex);
        for (int i=0;i<questGivers.Length;i++)
        {
            SetQuestState(i, false);
        }
        SetQuestState(qID, true);
        player = GameObject.FindGameObjectWithTag("Player");
        gsm = GameObject.FindGameObjectWithTag("GameSaveManager");
        p = player.GetComponent<PPlayer>();
    }
    public void SetQuestState(int questID, bool state)
    {
        questGivers[questID].enabled = state;
        curQuestIndex = questGivers[questID].quest.index;
        notifyier.GetComponent<NotifyAboutAvaliableQuest>().active = true;
        SetNotifier(questID);
    }
    public void OpenQWindow()
    {
        int qID = GetQuestID();
        notifyier.GetComponent<NotifyAboutAvaliableQuest>().Activated();
        float distance = Vector3.Distance(questGivers[qID].gameObject.transform.position, player.transform.position);
        if ((questGivers[qID].quest.isActive == false) && (distance <= questGivers[qID].activationDistance) &&(curQuestIndex == questGivers[qID].quest.index))
        {
            title.text = questGivers[qID].quest.title;
            description.text = questGivers[qID].quest.description;
            reward.text = questGivers[qID].quest.goldReward.ToString() + " G";
            QP.SetActive(true);
        }
    }
    public void ShowQuestCompletionPanel()
    {
        int qID = GetQuestID();
        completed.text = questGivers[qID].quest.goldReward.ToString() + "G";
        QCP.SetActive(true);
    }
    public void QuestEnded()
    {
        QCP.SetActive(false);
        int qID = FindQuestIDByCurrentQuestIndex(curQuestIndex);
        Debug.Log("" + qID+" " + (questGivers.Length - 1));
        if (qID != (questGivers.Length-1))
        {
            SetQuestState(qID, false);
            SetQuestState(qID + 1, true);
        }
        else
            LvlOver();
    }
    int GetQuestID()
    {
        for (int i = 0; i < questGivers.Length; i++)
        {
            if (questGivers[i].isActiveAndEnabled == true)
            {
                return i;
            }
        }
        return -1; // i avoided a lot of problems somehow, but this is gonna make a lot of problems
    }
    public void Accept()
    {
        int qID = GetQuestID();
        
        QP.SetActive(false);
        questGivers[qID].quest.isActive = true;
        p.quest = questGivers[qID].quest;
    }
    public void SetNotifier(int qID)
    {
        notifyier.transform.position = questGivers[qID].transform.position;
        notifyier.radius = questGivers[qID].activationDistance;
    }
    int FindQuestIDByCurrentQuestIndex(int curQuestInd)
    {
        for (int i=0;i<questGivers.Length;i++)
        {
           if(questGivers[i].quest.index == curQuestInd)
           {
                Debug.Log("" +curQuestInd+" "+ i);
                curQuestIndex = curQuestInd;
                return i;
           }
        }
        return 0;
    }
    public void GivePlayerQuestByIndex(int pQuestIndex)
    {
        int qID = FindQuestIDByCurrentQuestIndex(pQuestIndex);
        p.quest = questGivers[qID].quest;
    }
    public void SaveMark()
    {
        mm.Marks.Add(p.quest.mark);
    }
    void LvlOver()
    {
        mm.CountLvlOverallMark();
        LvlCP.gameObject.SetActive(true);
        showMark.text ="Ваша оценка: " + mm.LvlOverallMark;
    }
}
