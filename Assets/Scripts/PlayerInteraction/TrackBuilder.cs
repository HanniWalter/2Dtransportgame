using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrackBuilder : MonoBehaviour
{

    public MainInputController mainInputController;

    public Vector2? lastpoint; 
    //public float? lastangle; 
    public WayPoint lastWayPoint;
    public PlayerInputController playerInputController;
    private TabGroup mainTabGroup;
    [SerializeField] private TabButton buttonBuildNormal; 
    LineRenderer previewTrack;

    private void Awake()
	{
	}
    // Start is called before the first frame update
    void Start()
    {
        previewTrack = GetComponent<LineRenderer>();
        mainTabGroup = GetComponent<TabGroup>();
        
        mainInputController= FindObjectOfType<MainInputController>();
		playerInputController = mainInputController.playerInputController;
        playerInputController.Mouse.RightButton.performed += ctx => RightMouseClick(ctx);
        playerInputController.Mouse.LeftButton.performed += _ => LeftMouseClick();
    }

    // Update is called once per frame
    void Update()
    {
        showPreviewTrack();
    }

    private void RightMouseClick(InputAction.CallbackContext ctx)
    {
        Reset();
    }



    private void LeftMouseClick()
    {
        if(mainTabGroup.selectedTab == buttonBuildNormal){
            TrackPlaceing();
        }
        else
        {
            Reset();
        }
    }

    private void Reset(){
        lastpoint = null; 
        lastWayPoint = null;
    }

    private void showPreviewTrack(){
        
       /* if(mainInputController.objectundercourser != null){
            var wayPoint = (WayPoint) mainInputController.objectundercourser;
            //if(wayPoint.type == WayPoint.Type.Switch||wayPoint.type == WayPoint.Type.Signal){
            //    return;
            //}
        }
        if (lastWayPoint == null && lastpoint == null)
        {
            previewTrack.positionCount = 0;
            previewTrack.SetPositions(new Vector3[0]);
            return;
        }
        Vector2 endLocation;
        float? startangle = null;
        float? endangle = null;
        //Debug.Log("Test");

        if(mainInputController.objectundercourser != null)
        {
            
            WayPoint wayPoint = null;
            
            wayPoint = (WayPoint) mainInputController.objectundercourser;
            if(wayPoint == lastWayPoint){
                return;
            }
            //Debug.Log(mainInputController.objectundercourser.GetType());
            //Debug.Log((new List<float>(wayPoint.TracksDirection.Keys)[0]));
            //Debug.Log("Test.5");
            //if (wayPoint.type == WayPoint.Type.DeadEnd){
            //    endangle = (new List<float>(wayPoint.TracksDirection.Keys))[0];
            //}
            //if (wayPoint.type == WayPoint.Type.Default){
                    //here better angle
                endangle = (new List<float>(wayPoint.TracksDirection.Keys))[0];
            //}
            endLocation = wayPoint.location;
        }else
        {
            //Debug.Log("Test2");
            endLocation = mainInputController.courserFixed;
            //Debug.Log("Test3");
        }
        //Debug.Log("Test3.5");


        if(lastWayPoint != null){ 
            //here lastangle
            //Debug.Log("Test4");

            //if (lastWayPoint.type == WayPoint.Type.DeadEnd){
                startangle = Util.oppositeAngle((new List<float>(lastWayPoint.TracksDirection.Keys))[0]);
            //}
            //if (lastWayPoint.type == WayPoint.Type.Default){
                //here better angle
                startangle = Util.oppositeAngle((new List<float>(lastWayPoint.TracksDirection.Keys))[0]);
            //}

            //Debug.Log("Test5");
            var points = PointCreator.create(lastWayPoint.location,endLocation,startangle,endangle).points;
            previewTrack.positionCount = points.Length;
            previewTrack.SetPositions(Util.toV3A(points));
            //Debug.Log("Test6");

        }else
        {
            var points = PointCreator.create(lastpoint.Value,endLocation,startangle,endangle).points;
            previewTrack.positionCount = points.Length;
            previewTrack.SetPositions(Util.toV3A(points));
        }
            //Debug.Log("Test7");*/
    }

    private void TrackPlaceing(){/*
        if(mainInputController.objectundercourser != null){
            var wayPoint = (WayPoint) mainInputController.objectundercourser;
            //if(wayPoint.type == WayPoint.Type.Switch||wayPoint.type == WayPoint.Type.Signal){
            //    lastpoint = null;
            //    lastWayPoint = null;
            //    return;
            //}
        }
        if (lastWayPoint == null && lastpoint == null)
        {
            if(mainInputController.objectundercourser != null){
                lastWayPoint = (WayPoint) mainInputController.objectundercourser;
            }else{
                lastpoint = mainInputController.courserFixed;
            }
        }else
        {
            WayPoint wayPoint = null; 
            float? startangle = null;
            float? endangle = null;
            //Debug.Log("Test");

            if(mainInputController.objectundercourser != null)
            {
                wayPoint = (WayPoint) mainInputController.objectundercourser;
                if(wayPoint == lastWayPoint){
                    return;
                }
                //if (wayPoint.type == WayPoint.Type.DeadEnd){
                //    endangle = (new List<float>(wayPoint.TracksDirection.Keys))[0];
                //}
                //if (wayPoint.type == WayPoint.Type.Default){
                    //here better angle
                    endangle = (new List<float>(wayPoint.TracksDirection.Keys))[0];
                //}

            }else
            {
                //Debug.Log("Test2");

                wayPoint = WayPoint.newWayPoint(mainInputController.courserFixed);
                //Debug.Log("Test3");

            }

            if(lastWayPoint != null){ 
                //here lastangle
                //Debug.Log("Test4");
                //if (lastWayPoint.type == WayPoint.Type.DeadEnd){
                //    startangle = Util.oppositeAngle((new List<float>(lastWayPoint.TracksDirection.Keys))[0]);
                //}
                //if (lastWayPoint.type == WayPoint.Type.Default){
                    //here better angle
                    startangle = Util.oppositeAngle((new List<float>(lastWayPoint.TracksDirection.Keys))[0]);
                //}

                //Debug.Log("Test5");

                Track track = Track.newTrack(PointCreator.create(lastWayPoint.location,wayPoint.location,startangle,endangle),lastWayPoint,wayPoint);
                //Debug.Log("Test6");

            }else
            {
                lastWayPoint = WayPoint.newWayPoint(lastpoint.Value);
                Track track = Track.newTrack(PointCreator.create(lastpoint.Value,wayPoint.location,null,endangle),lastWayPoint,wayPoint);
            }
            lastpoint = wayPoint.location;
            lastWayPoint = wayPoint;
        }*/
    }
}
