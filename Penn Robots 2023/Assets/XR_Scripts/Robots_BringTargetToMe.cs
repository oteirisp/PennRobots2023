using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robots_BringTargetToMe : MonoBehaviour
{
    public GameObject ObjectToMove;
    public GameObject ThingToMoveTo;

    public void ComeToMe()
    {
        ObjectToMove.transform.position = ThingToMoveTo.transform.position;
        ObjectToMove.transform.rotation = ThingToMoveTo.transform.rotation;
        ObjectToMove.transform.RotateAround(ObjectToMove.transform.position, ObjectToMove.transform.right, -90);
    }
}
