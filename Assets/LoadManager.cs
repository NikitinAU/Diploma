using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
public class LoadManager : MonoBehaviour
{
    public SettingsMenu settings;
    public LvlSelectionScreen lss;
    public Button loadBtn;
    // Start is called before the first frame update
    
    void Start()
    {
        
        LoadPrefs();
        if (!File.Exists(Application.dataPath + "/Save.txt"))
        {
            loadBtn.gameObject.SetActive(false);
        }
        else
        {
            loadBtn.onClick.AddListener(LoadSave);
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
        Debug.Log(""+volume);
    }
    public void LoadPrefs()
    {
        if (PlayerPrefs.HasKey("Volume"))
            settings.SetVolume(PlayerPrefs.GetFloat("Volume"));
        if (PlayerPrefs.HasKey("GraphicsLevel"))
            settings.SetQuality(PlayerPrefs.GetInt("GraphicsLevel"));
        if (PlayerPrefs.GetInt("Inversed") == 1)
            settings.UICtrlPos(true);
        
    }
    void LoadSave()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/Save.txt");

        XmlNodeList lvl = xmlDoc.GetElementsByTagName("curScene");
        int curID = int.Parse(lvl[0].InnerText);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + curID);
    }
}
