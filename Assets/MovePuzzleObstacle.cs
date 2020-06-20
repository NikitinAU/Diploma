using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuzzleObstacle : MonoBehaviour
{
    public GameObject RightRock,LeftRock;
    Vector3 savedPos, rSavedPos, lSavedPos;
    private void Start()
    {
        savedPos = this.gameObject.transform.position;
        rSavedPos = RightRock.transform.position;
        lSavedPos = LeftRock.transform.position;
    }
    public void MoveRight()
    {
        ReturnToSavedPos();
        RightRock.transform.position = new Vector3(RightRock.transform.position.x, RightRock.transform.position.y -200, RightRock.transform.position.z);
    }
    public void MoveLeft()
    {
        ReturnToSavedPos();
        LeftRock.transform.position = new Vector3(LeftRock.transform.position.x, RightRock.transform.position.y -200, LeftRock.transform.position.z);
    }
    public void MoveBoth()
    {
        ReturnToSavedPos();
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y -200, this.transform.position.z);
    }
   public void ReturnToSavedPos()
    {
        this.transform.position= savedPos;
        RightRock.transform.position = rSavedPos;
        LeftRock.transform.position= lSavedPos;
    }

}
