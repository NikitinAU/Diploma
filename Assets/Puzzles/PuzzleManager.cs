using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PuzzleManager : MonoBehaviour
{
    bool[,] boardSpacer;
    char[] alphabet = { ' ' };
    char[,] puzzleBoard;
    public Font f;
    public PuzzleTrigger[] pt;
    public Button action, prefab, activate;
    GameObject player;
    public TMPro.TextMeshProUGUI text, msgTxt;
    public GameObject panel, msgPanel;
    public int curPuzzle=0;
    string sentence="";
    List<Button> created = new List<Button>();
    void Start()
    {
        alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        prefab.gameObject.AddComponent<Text>().text="";
        prefab.gameObject.GetComponent<Text>().font = f;
        prefab.gameObject.GetComponent<Text>().color = new Color(0, 0,0);
        prefab.gameObject.GetComponent<Text>().fontSize = 50;
        prefab.gameObject.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < pt.Length; i++)
        {
            puzzleBoard = new char[pt[i].puzzle.y_size, pt[i].puzzle.x_size];
            boardSpacer = new bool[pt[i].puzzle.y_size, pt[i].puzzle.x_size];
            ClearSpacer(i);
            pt[i].gameObject.AddComponent<SphereCollider>();
            pt[i].gameObject.GetComponent<SphereCollider>().radius = pt[i].activationDistance;
            pt[i].gameObject.GetComponent<SphereCollider>().isTrigger = true;
            pt[i].gameObject.AddComponent<GetPuzzleID>();
            pt[i].gameObject.GetComponent<GetPuzzleID>().id = i;
        }
        action.onClick.AddListener(ShufflePuzzle);
        activate.onClick.AddListener(CheckSentence);
    }
    public void ShufflePuzzle()
    {
        float distance = Vector3.Distance(pt[curPuzzle].gameObject.transform.position, player.transform.position);
        if (distance<=pt[curPuzzle].activationDistance) 
        {
            for (int i = 0; i < pt[curPuzzle].puzzle.y_size; i++)
            {
                for (int j = 0; j < pt[curPuzzle].puzzle.x_size; j++)
                {
                    puzzleBoard[i, j] = alphabet[Random.Range(0, alphabet.Length)];
                }
            }
            Debug.Log("random shit generated");
            for (int i = 0; i < pt[curPuzzle].puzzle.words.Length; i++)
            {
                bool ok = false;
                int x = 0, y = 0, rot = 0;
                int failsafe = 0;
                while (!ok)
                {
                    failsafe++;
                    x = Random.Range(0, pt[curPuzzle].puzzle.x_size - pt[curPuzzle].puzzle.words[i].Length + 1);
                    y = Random.Range(0, pt[curPuzzle].puzzle.y_size - pt[curPuzzle].puzzle.words[i].Length + 1);
                    rot = Random.Range(0, 2);//0 - h, 1 - v
                    if (!WillIntersect(x, y, i, rot))
                        ok = true;
                    if (failsafe >= 10000)
                    {
                        Debug.Log("failsaved at");
                        ok = true;
                    }
                }
                Debug.Log(failsafe + " tries " + i + " word " + "rot" + rot + " at " + x + " " + y);
                AddToSpacer(x, y, i, rot);
                AddToBoard(x, y, i, rot);
            }
            DisplayBoard();
        }
    }
    void DisplayBoard()
    {
        action.gameObject.SetActive(false);
        panel.SetActive(true);
        float size_h = panel.GetComponent<RectTransform>().rect.width;
        float size_v = panel.GetComponent<RectTransform>().rect.height;
        float spacing_h = size_h / pt[curPuzzle].puzzle.x_size;
        float spacing_v = size_v / pt[curPuzzle].puzzle.y_size;
        prefab.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(spacing_h, spacing_v);
        float cur_x = -472.5f, 
            cur_y= 426.5f;
        for (int i = 0; i < pt[curPuzzle].puzzle.y_size; i++)
        {
            for (int j = 0; j < pt[curPuzzle].puzzle.x_size; j++)
            {
                Button n = Instantiate(prefab);
                n.transform.SetParent(panel.transform);
                n.transform.localScale = new Vector3(1f, 1f, 1f);
                n.GetComponent<Text>().text = puzzleBoard[i,j].ToString();
                n.transform.localPosition = new Vector3(cur_x, cur_y, 0);
                n.onClick.AddListener(() => ComposeSentence(n.GetComponent<Text>().text));
                n.onClick.AddListener(() => RemoveButton(n));
                created.Add(n);
                n.gameObject.SetActive(true);
                cur_x += spacing_h;
            }
            cur_x = -472.5f;
            cur_y -= spacing_v;
        }
        
    }
    void ClearSpacer(int i)
    {
        for (int j = 0; j < pt[i].puzzle.y_size; j++)
        {
            for (int k = 0; k < pt[i].puzzle.x_size; k++)
                boardSpacer[j, k] = false;
        }
    }
    bool WillIntersect(int x, int y, int pos, int rot)
    {
        if (rot == 0)
        {
            for (int i = x; i < x + pt[curPuzzle].puzzle.words[pos].Length; i++)
            {
                if (boardSpacer[y, i] == true)
                    return true;
            }
        }
        else
        {
            for (int i = y; i < y + pt[curPuzzle].puzzle.words[pos].Length; i++)
            {
                if (boardSpacer[i, x] == true)
                    return true;
            }
        }
        return false;
    }
    void AddToSpacer(int x, int y, int pos, int rot)
    {
        if (rot == 0)
        {
            for (int i = x; i < x + pt[curPuzzle].puzzle.words[pos].Length; i++)
            {
                boardSpacer[y, i] = true;
            }
        }
        else
        {
            for (int i = y; i < y + pt[curPuzzle].puzzle.words[pos].Length; i++)
            {
                boardSpacer[i, x] = true;
            }
        }
    }
    void AddToBoard(int x, int y, int pos, int rot)
    {
        int count = 0;
        if (rot == 0)
        {
            for (int i = x; i < x + pt[curPuzzle].puzzle.words[pos].Length; i++)
            {
                char[] ar = pt[curPuzzle].puzzle.words[pos].ToCharArray();
                puzzleBoard[y, i] = ar[count];
                count++;
            }
        }
        else
        {
            for (int i = y; i < y + pt[curPuzzle].puzzle.words[pos].Length; i++)
            {
                char[] ar = pt[curPuzzle].puzzle.words[pos].ToCharArray();
                puzzleBoard[i, x] = ar[count];
                count++;
            }
        }
    }
    void ComposeSentence(string letter)
    {
        sentence += letter;
        text.text = sentence;
    }
    void RemoveButton(Button btn)
    {
        btn.gameObject.SetActive(false);
    }
    void CheckSentence()
    {

        ClearSpacer(curPuzzle);
        foreach(Button n in created)
        {
            Destroy(n.gameObject);
        }
        created.Clear();
        action.gameObject.SetActive(true);
        panel.SetActive(false);
        bool exists=false;
        for (int i=0;i< pt[curPuzzle].puzzle.words.Length;i++)
        {
            if(sentence == pt[curPuzzle].puzzle.words[i])
            {
                exists = true;
            }
        }
        if (exists==true)
        {
            msgTxt.text = "что-то произошло...";
            pt[curPuzzle].PuzzleSolved(sentence);
        }
        else
        {
            msgTxt.text = "Ничего не случилось";
        }
        msgPanel.SetActive(true);
        sentence = "";
        text.text = sentence;
    }
}
