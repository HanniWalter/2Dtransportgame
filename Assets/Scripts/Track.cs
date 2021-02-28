using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : WorldObject
{
    public void debug(){
        Debug.Log(gameObject.name);
        Debug.Log( Lenght);
    }

    private float _lenght = 0;
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
    private List<Track> TracksConnectedStart;
    private List<Track> TracksConnectedEnd;
    [SerializeField] public Vector2[] points;
    [SerializeField] public Vector2[] tangents;

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
    public Vector2 firstTangent{
        get
        {
            return tangents[0];
        }
    }
    public Vector2 lastTangent{
        get
        {
            return tangents[points.Length-1];
        }
    }

    [SerializeField] private Track[] _ConnectionsBegin;
    [SerializeField] private Track[] _ConnectionsEnd;

    public Track[] ConnectionsBegin{
        get{
            var ret = new List<Track>();
            foreach(Track track in worldData.tracks){
                if (track.firstPoint == this.firstPoint){
                    if (Util.sameDirection(track.firstTangent,-this.firstTangent,5)){
                        ret.Add(track);
                    }
                }
                if (track.lastPoint == this.firstPoint){
                    if (Util.sameDirection(track.lastTangent,this.firstTangent,5)){
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
                    if (Util.sameDirection(track.firstTangent,this.lastTangent,5)){
                        ret.Add(track);
                    }
                }
                if (track.lastPoint == this.lastPoint){
                    if (Util.sameDirection(track.lastTangent,-this.lastTangent,5)){
                        ret.Add(track);
                    }
                }
            }
            _ConnectionsEnd = ret.ToArray();
            return ret.ToArray();
        }
    }

    public static Track newTrack (PointCreator pointCreator){

		return newTrack (pointCreator.points, pointCreator.tangents);
    }
    public static Track newTrack (Vector2[] points, Vector2[] tangents){
        GameObject newGameObject = new GameObject();
		Track ret = newGameObject.AddComponent<Track>(); 
        ret.points = points;
        ret.tangents = tangents;
        

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
        debug();
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
