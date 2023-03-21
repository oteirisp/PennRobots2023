using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Intel.RealSense;
using WebSocketSharp;


public class JeffInClass_Simple : MonoBehaviour
{
    public Transform theThingToMove;
    public Transform whereToMoveTo;
    public float howFarInFront = 0.2f;

    private bool DrawingOn = false;

    public float DistTolerance = 0.2f;
    private Vector3 lastPlotPosition;
    private float currentDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(DrawingOn == true)
        {
            //UnityEngine.Debug.Log("drawing is on!");

            currentDistance = Vector3.Distance(lastPlotPosition, whereToMoveTo.position);

            if(currentDistance > DistTolerance)
            {
                //plot some point
                UnityEngine.Debug.Log("plot a point!");


                Vector3 editedPosition = whereToMoveTo.position;
                editedPosition = editedPosition + (whereToMoveTo.forward * howFarInFront);


                GameObject plotThisThing = Instantiate(theThingToMove.gameObject, editedPosition, whereToMoveTo.rotation);
                plotThisThing.transform.RotateAround(plotThisThing.transform.position, plotThisThing.transform.right, -90);


                //set last plot position to current position
                lastPlotPosition = whereToMoveTo.position;
            }

        }

    }


    public void StartDrawing()
    {
        DrawingOn = true;
    }
    public void StopDrawing()
    {
        DrawingOn = false;
    }



    public void BringTargetToMe()
    {
        UnityEngine.Debug.Log("i called bring target to me!");

        Vector3 editedPosition = whereToMoveTo.position;
        editedPosition = editedPosition + (whereToMoveTo.forward * howFarInFront);

        theThingToMove.position = editedPosition;
        theThingToMove.rotation = whereToMoveTo.rotation;

        theThingToMove.transform.RotateAround(theThingToMove.transform.position, theThingToMove.transform.right, -90);

    }


}