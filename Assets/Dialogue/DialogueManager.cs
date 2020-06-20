using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public int curTrigger = 0;
    public QuestManager qm;
    public DialogueTrigger[] triggers;
    public Image image;
    public TMPro.TextMeshProUGUI name, dialogue;
    public Animator animator;
    public Button next;
    public Button[] AnswerButton;
    public TMPro.TextMeshProUGUI[] text;
    GameObject player;
    int qPos=0;
    public int curObj=0;
    bool good = false, give = false;
    private Queue<string> sentences; //fifo collection
    void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
        InitTriggers();
    }
    void InitTriggers()
    {
        for (int i=0;i<triggers.Length;i++)
        {
            triggers[i].gameObject.AddComponent<SphereCollider>();
            triggers[i].gameObject.GetComponent<SphereCollider>().radius = triggers[i].activationDistance;
            triggers[i].gameObject.GetComponent<SphereCollider>().isTrigger = true;
            triggers[i].gameObject.AddComponent<GetDialogueId>();
            triggers[i].gameObject.GetComponent<GetDialogueId>().id = i;
            triggers[i].gameObject.GetComponent<GetDialogueId>().image = image;
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        SwapElements(true);
        animator.SetBool("isOpen", true);
        name.text = dialogue.name;
        AssignAnswerButtons();
        sentences.Clear();
        
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        
        if (sentences.Count == 0)
        {
            EndDialogue(); return;
        }
        /*if (sentences.Count == 1)
        {
            next.gameObject.GetComponent<Text>().text= "Конец";
        }*/
        if (sentences.Count == qPos)
        {
            SwapElements(false);
        }
        else
        {
            SwapElements(true);
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogue.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("End");
        if (good==true)
        {
            player.GetComponent<PPlayer>().doTask(triggers[curTrigger].gameObject.GetComponent<QuestObj>().givesProgress);
            curObj++;
            good = false;
            if (player.GetComponent<PPlayer>().quest.goal.IsReached())
            {
                player.GetComponent<PPlayer>().QuestToNull();
                curObj = 0;
            }
        }
        
    }
    void SwapElements(bool active)
    {
        next.gameObject.SetActive(active);
        for (int i = 0; i < AnswerButton.Length; i++)
        {
            AnswerButton[i].gameObject.SetActive(!active);
        }
    }
    public void TriggerDialogue()
    //------------BIG MESS BIG MESS BIG MESS BIG MESS BIG MESS BIG MESS -------------------
    {
        float distance = Vector3.Distance(triggers[curTrigger].gameObject.transform.position, player.transform.position);
        if (distance <= triggers[curTrigger].activationDistance)
        {
            if ((player.GetComponent<PPlayer>().quest.isActive == true) && (player.GetComponent<PPlayer>().quest.goal.goalType == GoalType.Talk))
            {
                if (player.GetComponent<PPlayer>().quest.objects[curObj].index == triggers[curTrigger].gameObject.GetComponent<QuestObj>().index)
                {
                    good = true;
                    triggers[curTrigger].gameObject.GetComponent<GetDialogueId>().good = good;
                }
            }
            if ((player.GetComponent<PPlayer>().quest.isActive == true) && good)
            {
                StartDialogue(triggers[curTrigger].dialogueQuest);
            }
            else
                StartDialogue(triggers[curTrigger].dialogueNoQuest);
        }
    }
    public void RightAnswer()
    {
        RemoveButtonListeners();
        qPos = 0;
        qm.SaveMark();
        string s = "Correct!";
        sentences.Enqueue(s);
        DisplayNextSentence();
    }
    public void WrongAnswer()
    {
        RemoveButtonListeners();
        qPos = 0;
        string s = "Incorrect!";
        player.GetComponent<PPlayer>().quest.goal.curAmount-=1;
        sentences.Enqueue(s);
        curObj--;
        player.GetComponent<PPlayer>().quest.mark-=1;
        DisplayNextSentence();
    }
    public void RestartQuestAnswer()
    {
        RemoveButtonListeners();
        qPos = 0;
        curObj =-1;
        player.GetComponent<PPlayer>().quest.goal.curAmount=-1;
        string s = "Ok";
        sentences.Enqueue(s);
        DisplayNextSentence();
    }
    void AssignAnswerButtons()
    {
        if(triggers[curTrigger].hasAnswer==true)
        {
            qPos = triggers[curTrigger].qPos;
            text[triggers[curTrigger].rightPos].text = triggers[curTrigger].rightAnswer;
            text[triggers[curTrigger].wrongPos].text = triggers[curTrigger].wrongAnswer;
            text[triggers[curTrigger].restartPos].text = triggers[curTrigger].restartAnswer;
            AnswerButton[triggers[curTrigger].rightPos].onClick.AddListener(RightAnswer);
            AnswerButton[triggers[curTrigger].wrongPos].onClick.AddListener(WrongAnswer);
            AnswerButton[triggers[curTrigger].restartPos].onClick.AddListener(RestartQuestAnswer);
        }
    }
    void RemoveButtonListeners()
    {
        AnswerButton[triggers[curTrigger].rightPos].onClick.RemoveListener(RightAnswer);
        AnswerButton[triggers[curTrigger].wrongPos].onClick.RemoveListener(WrongAnswer);
        AnswerButton[triggers[curTrigger].restartPos].onClick.RemoveListener(RestartQuestAnswer);
    }
}
