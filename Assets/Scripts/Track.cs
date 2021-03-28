using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : WorldObject
{


    [SerializeField] private float _lenght = 0;
    public float Lenght{
        get{
            if (_lenght != 0){
                return _lenght;
            }else{
                _lenght = 0;
                for (int i = 0; i < points.Length-1; i++)
                {
                    _lenght +=(points[i] -points[i+1]).magnitude;
                }
                return _lenght;
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



    [SerializeField] private Track[] _ConnectionsBegin;
    [SerializeField] private Track[] _ConnectionsEnd;

    public WayPoint WayPointBegin;
    public WayPoint WayPointEnd;

    public List<Track> ConnectionsBegin{
        get{
            foreach (var item in WayPointBegin.TracksDirection.Keys)
            {
                if(Util.sameDirection(item,Util.oppositeAngle(firstAngle),5f)){
                    var ret = WayPointBegin.TracksDirection[item];
                    _ConnectionsBegin = ret.ToArray();
                    return ret;
                }
            }
            Debug.Log(this);
            return new List<Track>();
        }
    }

    public List<Track> ConnectionsEnd{
        get{
            foreach (var item in WayPointEnd.TracksDirection.Keys)
            {
                if(Util.sameDirection(item,lastAngle,5f)){
                    var ret = WayPointEnd.TracksDirection[item];
                    _ConnectionsEnd = ret.ToArray();
                    return ret;
                }
            }
            Debug.Log(this);
            return new List<Track>();
        }
    }

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
        gameObject.AddComponent<LineRenderer>();
        worldData.tracks.Add(this);
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (WayPoint wp in worldData.wayPoints)
        {
            wp.AddTrack(this);
        } 
        draw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void draw(){
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(Util.toV3A(points));
    }
}
