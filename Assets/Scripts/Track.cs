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

    public Track[] ConnectionsBegin{
        get{
            var ret = new List<Track>();
            foreach(Track track in worldData.tracks){
                if (track.firstPoint == this.firstPoint){
                    if (Util.sameDirection(track.firstAngle,Util.oppositeAngle(this.firstAngle),5)){
                        ret.Add(track);
                    }
                }
                if (track.lastPoint == this.firstPoint){
                    if (Util.sameDirection(track.lastAngle,this.firstAngle,5)){
                        ret.Add(track);
                    }
                }
            }
            _ConnectionsBegin = ret.ToArray();
            return ret.ToArray();
        }
    }

    public Track[] ConnectionsEnd{
        get{
            var ret = new List<Track>();
            foreach(Track track in worldData.tracks){
                if (track.firstPoint == this.lastPoint){
                    if (Util.sameDirection(track.firstAngle,this.lastAngle,5)){
                        ret.Add(track);
                    }
                }
                if (track.lastPoint == this.lastPoint){
                    if (Util.sameDirection(track.lastAngle,Util.oppositeAngle(this.lastAngle),5)){
                        ret.Add(track);
                    }
                }
            }
            _ConnectionsEnd = ret.ToArray();
            return ret.ToArray();
        }
    }

    public static Track newTrack (PointCreator pointCreator){

		return newTrack (pointCreator.points, pointCreator.angels);
    }
    public static Track newTrack (Vector2[] points, float[] angles){
        GameObject newGameObject = new GameObject();
		Track ret = newGameObject.AddComponent<Track>(); 
        ret.points = points;
        ret.angles = angles;
        

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
