using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlSelectionScreen : MonoBehaviour
{
    public Button[] lvlBtns;
    public int curId;
    private void Start()
    {
        foreach (Button btn in lvlBtns)
        {
            btn.gameObject.SetActive(false);
        }
        ShowlvlSelectionScreens();

    }
    public void ShowlvlSelectionScreens()
    {
        for (int i=1;i<=SceneManager.sceneCount;i++)
        {
            lvlBtns[i-1].gameObject.SetActive(true);
            curId = i;
            lvlBtns[i-1].onClick.AddListener(LoadScene);
        }
    }
    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + curId);
    }
}
