using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : WorldObject
{


    [SerializeField] private float _length = 0;
    public float Length{
        get{
            if (_length != 0){
                return _length;
            }else{
                _length = 0;
                for (int i = 0; i < points.Length-1; i++)
                {
                    _length +=(points[i] -points[i+1]).magnitude;
                }
                return _length;
            }
        }
    }

    private static int _num = 0;
    [SerializeField] public Vector2[] points;
    [SerializeField] public float[] angles;

    public Vector2 firstPoint{
        get
        {
            return points[0];
        }
    }
    public Vector2 lastPoint {
        get
        {
            return points[points.Length-1];
        }
    }
    public float firstAngle{
        get
        {
            return angles[0];
        }
    }
    public float lastAngle{
        get
        {
            return angles[points.Length-1];
        }
    }

    public WayPoint WayPointBegin;
    public WayPoint WayPointEnd;

    LineRenderer[] lineRenderer = {null,null,null};


    public static Track newTrack (PointCreator pointCreator, WayPoint begin, WayPoint end){

		return newTrack (pointCreator.points, pointCreator.angels,begin,end);
    }
    public static Track newTrack (Vector2[] points, float[] angles, WayPoint begin, WayPoint end){
        GameObject newGameObject = new GameObject();
		Track ret = newGameObject.AddComponent<Track>(); 
        ret.points = points;
        ret.angles = angles;
        ret.WayPointBegin = begin;
        ret.WayPointEnd = end;

		return ret;
    }

    new void Awake(){
        ((WorldObject) this).Awake();
        gameObject.name = "Track "+ _num.ToString();
        _num++;

        worldData.tracks.Add(this);
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        draw();
        WayPointBegin.AddTrack(this);
        WayPointEnd.AddTrack(this);
    }

    void OnDestroy(){
        WayPointBegin.DeleteTrack(this);
        WayPointEnd.DeleteTrack(this);
    }

    void Update(){
        color();
    }

    void draw(){
        //LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        
        int offset = 200; 
        Vector2[] pointsA = new Vector2[offset+1];
        Vector2[] pointsB = new Vector2[points.Length-2*offset+1];
        Vector2[] pointsC = new Vector2[offset];
        for (int i = 0; i < points.Length; i++)
        {
            if(i<=offset){
                pointsA[i] = points[i];
            }
            if((i>=offset) && (i<=points.Length-offset)){
                pointsB[i-offset] = points[i];
            }
            if(i>=points.Length-offset){
                pointsC[i+offset-points.Length] = points[i];
            }
        }
        var seg1 = new GameObject("Segment1");
        var seg2 = new GameObject("Segment2");
        var seg3 = new GameObject("Segment3");
        seg1.transform.parent = transform;
        seg2.transform.parent = transform;
        seg3.transform.parent = transform;
        lineRenderer[0] =seg1.AddComponent<LineRenderer>();
        lineRenderer[1] =seg2.AddComponent<LineRenderer>();
        lineRenderer[2] =seg3.AddComponent<LineRenderer>();
        lineRenderer[0].positionCount = pointsA.Length;
        lineRenderer[1].positionCount = pointsB.Length;
        lineRenderer[2].positionCount = pointsC.Length;
        lineRenderer[0].SetPositions(Util.toV3A(pointsA));
        lineRenderer[1].SetPositions(Util.toV3A(pointsB));
        lineRenderer[2].SetPositions(Util.toV3A(pointsC));

        color();
    }

    public void color(){ 
        colorFirst();
        colorMiddle();
        colorLast();        
    }

    private void colorFirst(){
        var matlight = Resources.Load<Material>("Materials/TrackLight");
        var matdark = Resources.Load<Material>("Materials/Track");
        if(WayPointBegin.type == WayPointType.TrackSwitch){
            if(WayPointBegin.branchoff){
                if(WayPointBegin.tracks[1]==this){
                    lineRenderer[0].material = matlight;
                    lineRenderer[0].sortingOrder = -1;
                    return;
                }
            }else{
                if(WayPointBegin.tracks[2]==this){
                    lineRenderer[0].material = matlight;
                    lineRenderer[0].sortingOrder = -1;
                    return;
                }
            }
        }
        lineRenderer[0].material = matdark;
        lineRenderer[0].sortingOrder = 0;
    }

    private void colorMiddle(){
        var matlight = Resources.Load<Material>("Materials/TrackLight");
        var matdark = Resources.Load<Material>("Materials/Track");
        lineRenderer[1].material = matdark;
        
    }

    private void colorLast(){
        var matlight = Resources.Load<Material>("Materials/TrackLight");
        var matdark = Resources.Load<Material>("Materials/Track");
        if(WayPointEnd.type == WayPointType.TrackSwitch){
            if(WayPointEnd.branchoff){
                if(WayPointEnd.tracks[1]==this){
                    lineRenderer[2].material = matlight;
                    lineRenderer[2].sortingOrder = -1;
                    return;
                }
            }else{
                if(WayPointEnd.tracks[2]==this){
                    lineRenderer[2].material = matlight;
                    lineRenderer[2].sortingOrder = -1;
                    return;
                }
            }
        }
        lineRenderer[2].material = matdark;
        lineRenderer[2].sortingOrder = 0;
    }
}
