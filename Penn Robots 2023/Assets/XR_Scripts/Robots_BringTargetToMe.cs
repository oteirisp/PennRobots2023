using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robots_BringTargetToMe : MonoBehaviour
{
    public GameObject ObjectToMove;
    public GameObject ThingToMoveTo;

    private bool startRotate;
    private Quaternion startRotationController;
    private Quaternion startRotationTarget;

    public void ComeToMe()
    {
        ObjectToMove.transform.position = ThingToMoveTo.transform.position;
        ObjectToMove.transform.rotation = ThingToMoveTo.transform.rotation;
        ObjectToMove.transform.RotateAround(ObjectToMove.transform.position, ObjectToMove.transform.right, -90);
    }


    public void RemoteRotateStart()
    {
        startRotationController = ThingToMoveTo.transform.rotation;
        startRotationTarget = ObjectToMove.transform.rotation;

        startRotate = true;
    }
    public void RemoteRotateStop()
    {
        startRotate = false;

    }

    private void Update()
    {
        if (!startRotate)
        {
            return;
        }
        else
        {
            Quaternion differenceRotation = startRotationController * Quaternion.Inverse(ThingToMoveTo.transform.rotation);
            Quaternion finalRotation = differenceRotation * startRotationTarget;
            ObjectToMove.transform.rotation = finalRotation;
        }
    }
}
