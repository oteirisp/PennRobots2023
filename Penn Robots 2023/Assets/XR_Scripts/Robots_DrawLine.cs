using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Robots_DrawLine : MonoBehaviour
{
    //PLACE OBJECT, BASE FUNCTIONALITY
    public GameObject ObjectToFollow;
    public GameObject PrefabObjectToPlace;
    public float PlaceDistanceForward = 0.2f;
    public float DistanceTolerance = 0.01f;
    private GameObject ObjectInstantiated;
    private Vector3 endPoint;
    private Vector3 previousPosition;
    private bool DrawingOn = false;


    //DRAW LINE, EXTRA FUNCTIONALITY
    public GameObject PrefabLineRenderer;
    public float LineRendererWidth = 0.001f;
    private Color lineColor;
    private GameObject myDoodleLineRend;
    private LineRenderer doodleLine;
    private bool newDrawVerticies = false;
    private List<Vector3> currVerticies;


    void Start()
    {
        //DRAW LINE, EXTRA FUNCTIONALITY
        currVerticies = new List<Vector3>();
        lineColor = Color.yellow;
    }

    void Update()
    {
        //PLACE OBJECT, BASE FUNCTIONALITY
        if (DrawingOn)
        {
            Vector3 origin = ObjectToFollow.transform.position;
            Vector3 direction = ObjectToFollow.transform.TransformDirection(Vector3.forward);
            endPoint = origin + direction * PlaceDistanceForward;

            //PLACE OBJECT, BASE FUNCTIONALITY
            if (Vector3.Distance(endPoint, previousPosition) > DistanceTolerance)
            {
                ObjectInstantiated = Instantiate(PrefabObjectToPlace, endPoint, ObjectToFollow.transform.rotation);
                ObjectInstantiated.transform.RotateAround(ObjectInstantiated.transform.position, ObjectInstantiated.transform.right, 180);
                ObjectInstantiated.transform.localScale = new Vector3(.01f, .01f, .01f);
                previousPosition = endPoint;

                //DRAW LINE, EXTRA FUNCTIONALITY
                newDrawVerticies = true;
                currVerticies.Add(endPoint);
                ObjectInstantiated.transform.parent = myDoodleLineRend.transform;
            }
            //DRAW LINE, EXTRA FUNCTIONALITY
            if (newDrawVerticies)
            {
                if (currVerticies.Count > 0)
                {
                    doodleLine.positionCount = currVerticies.Count;

                    for (int i = 0; i < currVerticies.Count; i++)
                    {
                        doodleLine.SetPosition(i, currVerticies[i]);
                    }
                    newDrawVerticies = false;
                }
            }
        }
    }

    public void LineStart()
    {
        //PLACE OBJECT, BASE FUNCTIONALITY
        DrawingOn = true;

        //DRAW LINE, EXTRA FUNCTIONALITY
        myDoodleLineRend = Instantiate(PrefabLineRenderer, Vector3.zero, Quaternion.identity);
        doodleLine = myDoodleLineRend.GetComponent<LineRenderer>();
        doodleLine.material.color = lineColor;
        doodleLine.startWidth = LineRendererWidth;
        doodleLine.endWidth = LineRendererWidth;
    }

    public void LineStop()
    {
        //PLACE OBJECT, BASE FUNCTIONALITY
        DrawingOn = false;

        //DRAW LINE, EXTRA FUNCTIONALITY
        currVerticies = new List<Vector3>();
    }
}
