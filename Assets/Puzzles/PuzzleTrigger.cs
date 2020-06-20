using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PuzzleTrigger : MonoBehaviour
{
    public Puzzle puzzle;
    public MovePuzzleObstacle obj;
    public float activationDistance = 1.5f;
    private void Start()
    {
        if (activationDistance < 1f)
            activationDistance = 1f;
    }
    public void PuzzleSolved(string str)
    {
        if (str == puzzle.words[0])
            obj.MoveBoth();
       else if (str == puzzle.words[1])
            obj.ReturnToSavedPos();
        else if (str == puzzle.words[2])
            obj.MoveRight();
        else
            obj.MoveLeft();
    }
}
