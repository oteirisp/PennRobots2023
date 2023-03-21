

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;


public class Robots_VR_ControllerButtons : MonoBehaviour
{
    public float laserLength = 3.0f;

    public UnityEvent VRController_ButtonEvent_ThumbLeft = new UnityEvent();
    public UnityEvent VRController_ButtonEvent_ThumbRight = new UnityEvent();
    public UnityEvent VRController_ButtonEvent_ThumbUp = new UnityEvent();
    public UnityEvent VRController_ButtonEvent_ThumbDown = new UnityEvent();

    public UnityEvent VRController_ButtonEvent_TriggerClicked = new UnityEvent();
    public UnityEvent VRController_ButtonEvent_TriggerUnClicked = new UnityEvent();
    public UnityEvent VRController_ButtonEvent_Grip = new UnityEvent();
    public UnityEvent VRController_ButtonEvent_Menu = new UnityEvent();

    private bool isClicked_Trigger = false;

    void Start()
    {
        SteamVR_TrackedController trackedController = GetComponent<SteamVR_TrackedController>();
        trackedController.TriggerClicked += new ClickedEventHandler(DoClick);
        trackedController.TriggerUnclicked += new ClickedEventHandler(UnClick);
        trackedController.PadClicked += new ClickedEventHandler(PadClicked);
        trackedController.MenuButtonClicked += new ClickedEventHandler(MenuClick);
        trackedController.Gripped += new ClickedEventHandler(Gripped);
    }


    private void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit hit;
        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out hit))
        {
            string myTag = hit.transform.tag;
            GameObject touchObject = hit.transform.gameObject;

            if (Vector3.Distance(origin, hit.point) < laserLength)
            {
                MyRayHit();
            }
            else
            {
                MyRayMissed();
            }
        }
        else
        {//this means the raycast missed
            MyRayMissed();
        }
    }
    void MyRayHit()
    {

    }
    void MyRayMissed()
    {

    }
    void DoClick(object sender, ClickedEventArgs e)
    {
        if (!isClicked_Trigger)
        {
            Debug.Log("hey i clicked the trigger button");
            //sceneManager.GetComponent<InClassScript>().MySpecialButton();
            //sceneManager.GetComponent<Robots_SendInstructionToMaster>().SendToMaster();
            //this.gameObject.GetComponent<Robots_DrawLine>().LineStart();
            VRController_ButtonEvent_TriggerClicked.Invoke();
            isClicked_Trigger = true;
        }
    }
    void UnClick(object sender, ClickedEventArgs e)
    {
        if (isClicked_Trigger)
        {
            Debug.Log("hey i UN clicked the trigger button");
            //this.gameObject.GetComponent<Robots_DrawLine>().LineStop();
            VRController_ButtonEvent_TriggerUnClicked.Invoke();
            isClicked_Trigger = false;
        }
    }
    void PadClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("hey i clicked the pad button");

        float PadLimitHigh = 0.7f;
        float PadLimitLow = 0.3f;

        if (e.padX < -(PadLimitHigh) && e.padY < PadLimitLow) //Left
        {
            VRController_ButtonEvent_ThumbLeft.Invoke();
        }
        else if (e.padX > PadLimitHigh && e.padY < PadLimitLow) //Right
        {
            VRController_ButtonEvent_ThumbRight.Invoke();
        }
        else if (e.padX < PadLimitLow && e.padY > PadLimitHigh) //Up
        {
            VRController_ButtonEvent_ThumbUp.Invoke();
        }
        else if (e.padX < PadLimitLow && e.padY < -(PadLimitHigh)) //Down
        {
            VRController_ButtonEvent_ThumbDown.Invoke();
        }
    }

    void Gripped(object sender, ClickedEventArgs e)
    {
        //sceneManager.GetComponent<Robots_BringTargetToMe>().SendAllPointsToMachina();
        Debug.Log("hey i clicked the grip button");
        VRController_ButtonEvent_Grip.Invoke();
    }

    void MenuClick(object sender, ClickedEventArgs e)
    {
        Debug.Log("hey i clicked the menu button");
        VRController_ButtonEvent_Menu.Invoke();
    }
}