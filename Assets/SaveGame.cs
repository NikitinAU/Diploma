using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
public class SaveGame : MonoBehaviour
{
    //--------Saved to PlayerPrefs------
    public SettingsMenu settings;
    public MarkManager mm;
    //--------Saved to XML File---------
    public PPlayer p;
    public GameObject player, playerView;
    public QuestManager qm;
    public DialogueManager dm;
    public GameObject msgBoard;
    public TMPro.TextMeshProUGUI text;
    private void Start()
    {
        LoadPrefs();
        if (File.Exists(Application.dataPath + "/Save.txt"))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.dataPath + "/Save.txt");
            XmlNodeList lvl = xmlDoc.GetElementsByTagName("curScene");
            int curID = int.Parse(lvl[0].InnerText);
            if (curID==SceneManager.GetActiveScene().buildIndex)
            {
                LoadXML();
            }
        }
    }
    public void SavePrefs()
    {
        float volume;
        bool result = settings.audioMixer.GetFloat("Volume", out volume);
        if (result)
            PlayerPrefs.SetFloat("Volume", volume);
        else
            PlayerPrefs.SetFloat("Volume", 0);
        PlayerPrefs.SetInt("GraphicsLevel", QualitySettings.GetQualityLevel());
        int inv = settings.inversed ? 1 : 0;
        PlayerPrefs.SetInt("Inversed", inv);
        PlayerPrefs.SetInt("MarksGot", mm.Marks.Count);
        for (int i=0;i<mm.Marks.Count;i++)
        {
            PlayerPrefs.SetInt("Quest" + i + "Mark", mm.Marks[i]);
        }
        PlayerPrefs.SetFloat("Level" + SceneManager.GetActiveScene().buildIndex + "Mark", mm.LvlOverallMark);
        PlayerPrefs.SetInt("PlayerGold", p.gold);
        PlayerPrefs.SetInt("PlayerMark", p.quest.mark);
    }
    public void SaveXML()
    {
        //---------------------THATS NOT HOW YOU DO SECURE SAVE BUT FUCK IT---------------------------
        XmlDocument xmlDoc = new XmlDocument();

        XmlElement root = xmlDoc.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");
        {
            //-------------Some Meta Info----------------
            XmlElement metaInfo = xmlDoc.CreateElement("MetaInfo");
            root.AppendChild(metaInfo);
            {
                XmlElement curScene = xmlDoc.CreateElement("curScene");
                curScene.InnerText = SceneManager.GetActiveScene().buildIndex.ToString();
                metaInfo.AppendChild(curScene);
            }

            XmlElement playerInfo = xmlDoc.CreateElement("PlayerInfo");
            root.AppendChild(playerInfo);
            //------------------Player Info--------------------------
            {

                XmlElement playerQuest = xmlDoc.CreateElement("PlayerQuest");
                playerInfo.AppendChild(playerQuest);
                {
                    XmlElement playerQuestIndex = xmlDoc.CreateElement("PlayerQuestIndex");
                    playerQuestIndex.InnerText = p.quest.index.ToString();
                    playerQuest.AppendChild(playerQuestIndex);

                    XmlElement playerQuestGoalCurProgress = xmlDoc.CreateElement("PlayerQuestGoalCurProgress");
                    playerQuestGoalCurProgress.InnerText = p.quest.goal.curAmount.ToString();
                    playerQuest.AppendChild(playerQuestGoalCurProgress);

                    XmlElement playerQuestIsActive = xmlDoc.CreateElement("PlayerQuestIsActive");
                    playerQuestIsActive.InnerText = p.quest.isActive.ToString();
                    playerQuest.AppendChild(playerQuestIsActive);
                }
                XmlElement playerPosition = xmlDoc.CreateElement("PlayerPosition");
                playerInfo.AppendChild(playerPosition);
                {
                    XmlElement playerPositionX = xmlDoc.CreateElement("PlayerPositionX");
                    playerPositionX.InnerText = player.transform.position.x.ToString();
                    playerPosition.AppendChild(playerPositionX);

                    XmlElement playerPositionY = xmlDoc.CreateElement("PlayerPositionY");
                    playerPositionY.InnerText = player.transform.position.y.ToString();
                    playerPosition.AppendChild(playerPositionY);

                    XmlElement playerPositionZ = xmlDoc.CreateElement("PlayerPositionZ");
                    playerPositionZ.InnerText = player.transform.position.z.ToString();
                    playerPosition.AppendChild(playerPositionZ);
                }
                XmlElement playerRotation = xmlDoc.CreateElement("PlayerRotation");
                playerInfo.AppendChild(playerRotation);
                {
                    XmlElement playerAngleX = xmlDoc.CreateElement("PlayerAngleX");
                    playerAngleX.InnerText = player.transform.eulerAngles.x.ToString();
                    playerRotation.AppendChild(playerAngleX);

                    XmlElement playerAngleY = xmlDoc.CreateElement("PlayerAngleY");
                    playerAngleY.InnerText = player.transform.eulerAngles.y.ToString();
                    playerRotation.AppendChild(playerAngleY);

                    XmlElement playerAngleZ = xmlDoc.CreateElement("PlayerAngleZ");
                    playerAngleZ.InnerText = player.transform.eulerAngles.z.ToString();
                    playerRotation.AppendChild(playerAngleZ);
                }
                XmlElement playerViewPosition = xmlDoc.CreateElement("PlayerViewPosition");
                playerInfo.AppendChild(playerViewPosition);
                {
                    XmlElement playerViewPositionX = xmlDoc.CreateElement("PlayerViewPositionX");
                    playerViewPositionX.InnerText = playerView.transform.position.x.ToString();
                    playerViewPosition.AppendChild(playerViewPositionX);

                    XmlElement playerViewPositionY = xmlDoc.CreateElement("PlayerViewPositionY");
                    playerViewPositionY.InnerText = playerView.transform.position.y.ToString();
                    playerViewPosition.AppendChild(playerViewPositionY);

                    XmlElement playerViewPositionZ = xmlDoc.CreateElement("PlayerViewPositionZ");
                    playerViewPositionZ.InnerText = playerView.transform.position.z.ToString();
                    playerViewPosition.AppendChild(playerViewPositionZ);
                }
            }
            //-----------Quest Manager Info-----------------
            XmlElement questManagerInfo = xmlDoc.CreateElement("QuestManagerInfo");
            root.AppendChild(questManagerInfo);
            {
                XmlElement curQuestIndex = xmlDoc.CreateElement("CurQuestIndex");
                curQuestIndex.InnerText = qm.curQuestIndex.ToString();
                questManagerInfo.AppendChild(curQuestIndex);

                XmlElement notifyierState = xmlDoc.CreateElement("NotifyierState");
                notifyierState.InnerText = qm.notifyier.GetComponent<NotifyAboutAvaliableQuest>().active.ToString();
                questManagerInfo.AppendChild(notifyierState);
            }
            //----------Dialogue Manager Info--------------
            XmlElement dialogueManagerInfo = xmlDoc.CreateElement("DialogueManagerInfo");
            root.AppendChild(dialogueManagerInfo);
            {
                XmlElement curDialogueObj = xmlDoc.CreateElement("CurDialogueObj");
                curDialogueObj.InnerText = dm.curObj.ToString();
                dialogueManagerInfo.AppendChild(curDialogueObj);
            }
        }
        xmlDoc.AppendChild(root);
        xmlDoc.Save(Application.dataPath + "/Save.txt");
        if (File.Exists(Application.dataPath + "/Save.txt"))
        {
           Debug.Log("Saved, holy shit\nIt's here! " + Application.dataPath + "/Save.txt");
        }
        SavePrefs();
    }
    public void LoadPrefs()
    {
        if (PlayerPrefs.HasKey("Volume"))
            settings.SetVolume(PlayerPrefs.GetFloat("Volume"));
        if (PlayerPrefs.HasKey("GraphicsLevel"))
            settings.SetQuality(PlayerPrefs.GetInt("GraphicsLevel"));
        if (PlayerPrefs.GetInt("Inversed") == 1)
            settings.UICtrlPos(true);
        if(PlayerPrefs.GetInt("MarksGot")!=0)
        {
            for(int i=0;i< PlayerPrefs.GetInt("MarksGot");i++)
            {
                mm.Marks.Add(PlayerPrefs.GetInt("Quest" + i + "Mark"));
            }
            mm.LvlOverallMark = PlayerPrefs.GetFloat("Level" + SceneManager.GetActiveScene().buildIndex + "Mark");
        }
        p.gold = PlayerPrefs.GetInt("PlayerGold");
        p.quest.mark = PlayerPrefs.GetInt("PlayerMark");
        p.UpdatePlayerInfoBox();
    }
    public void LoadXML()
    {
        LoadPrefs();
        if (File.Exists(Application.dataPath + "/Save.txt"))
        {
            Debug.Log("File Exists!");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Application.dataPath + "/Save.txt");
            //Just get all the info (.Parse()(any type works))


            

            XmlNodeList playerQuestIndex = xmlDoc.GetElementsByTagName("PlayerQuestIndex");
            p.quest.index = int.Parse(playerQuestIndex[0].InnerText);

            qm.GivePlayerQuestByIndex(p.quest.index);

            XmlNodeList playerQuestGoalCurProgress = xmlDoc.GetElementsByTagName("PlayerQuestGoalCurProgress");
            p.quest.goal.curAmount = int.Parse(playerQuestGoalCurProgress[0].InnerText);

            XmlNodeList playerQuestIsActive = xmlDoc.GetElementsByTagName("PlayerQuestIsActive");
            p.quest.isActive = bool.Parse(playerQuestIsActive[0].InnerText);

            XmlNodeList playerPositionX = xmlDoc.GetElementsByTagName("PlayerPositionX");
            XmlNodeList playerPositionY = xmlDoc.GetElementsByTagName("PlayerPositionY");
            XmlNodeList playerPositionZ = xmlDoc.GetElementsByTagName("PlayerPositionZ");
            player.transform.position = new Vector3(float.Parse(playerPositionX[0].InnerText), float.Parse(playerPositionY[0].InnerText), float.Parse(playerPositionZ[0].InnerText));

            XmlNodeList playerAngleX = xmlDoc.GetElementsByTagName("PlayerAngleX");
            XmlNodeList playerAngleY = xmlDoc.GetElementsByTagName("PlayerAngleY");
            XmlNodeList playerAngleZ = xmlDoc.GetElementsByTagName("PlayerAngleZ");
            player.transform.eulerAngles = new Vector3(float.Parse(playerAngleX[0].InnerText), float.Parse(playerAngleY[0].InnerText), float.Parse(playerAngleZ[0].InnerText));

            XmlNodeList playerViewPositionX = xmlDoc.GetElementsByTagName("PlayerViewPositionX");
            XmlNodeList playerViewPositionY = xmlDoc.GetElementsByTagName("PlayerViewPositionY");
            XmlNodeList playerViewPositionZ = xmlDoc.GetElementsByTagName("PlayerViewPositionZ");
            playerView.transform.position= new Vector3(float.Parse(playerViewPositionX[0].InnerText), float.Parse(playerViewPositionY[0].InnerText), float.Parse(playerViewPositionZ[0].InnerText));

            XmlNodeList questManagerCurQuestIndex = xmlDoc.GetElementsByTagName("CurQuestIndex");
            qm.curQuestIndex = int.Parse(questManagerCurQuestIndex[0].InnerText);

            XmlNodeList notifyierState = xmlDoc.GetElementsByTagName("NotifyierState");
            qm.notifyier.GetComponent<NotifyAboutAvaliableQuest>().active = bool.Parse(notifyierState[0].InnerText);

            XmlNodeList dialogueManagerCurObj = xmlDoc.GetElementsByTagName("CurDialogueObj");
            dm.curObj = int.Parse(dialogueManagerCurObj[0].InnerText);
        }
        else
        {
            text.text = "файл не найден!";
            msgBoard.SetActive(true);
        }
    }
    public void GoToMainmenu()
    {
        SavePrefs();
        SceneManager.LoadScene(0);
    }
}
