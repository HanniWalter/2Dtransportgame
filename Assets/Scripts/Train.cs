using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : WorldObject
{
    private static int _num = 0;
    Track track;
    int indexTrack;
    int _indexStops = 0;
    bool TrackDirection;

    Vector2[] path = null;
    int indexStops{
        get{
            return _indexStops;
        }
        set{
            _indexStops = value % stops.Count;
        }
    }  
    
    public Vector2 location{
        get{
            return Util.toV2(transform.position);
        }
        set{
            transform.position = Util.toV3(value);
        }
    }
    
    public float direction{
        get{
            return transform.eulerAngles.z;
        }
        set{
            transform.eulerAngles = new Vector3(0,0,value);
        }
    }
    public List<Stop> stops = new List<Stop>();
    
    public static Train newTrain(Track track,int pointnum,bool TrackDirection){
        GameObject newGameObject = new GameObject();
		Train ret = newGameObject.AddComponent<Train>(); 
        ret.indexTrack = pointnum;
        ret.track = track;
        ret.TrackDirection = TrackDirection;
        
		return ret;
    }
    
    new void Awake(){
        ((WorldObject) this).Awake();
        gameObject.name = "Train "+ _num.ToString();
        _num++;
        worldData.trains.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        draw();
        findPath();
    }

    // Update is called once per frame
    void Update()
    {
        location = track.points[indexTrack];
        direction = Mathf.Atan2(track.tangents[indexTrack].y,track.tangents[indexTrack].x)*Mathf.Rad2Deg;
    }

    void draw(){
        gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Square");
    }

    bool findPath(){
        HashSet<(Vector2,Vector2)> toCalc = new HashSet<(Vector2,Vector2)>();
        HashSet<(Track,bool)> addToCalc = new HashSet<(Track,bool)>();
        addToCalc.Add((this.track,this.TrackDirection));
        while (addToCalc.Count!=0)
        {
            (Track,bool) current = (null,true); //= null true so the compiler shuts up
            (Vector2,Vector2) locationToCheck = (Vector2.zero,Vector2.zero);
            foreach ((Track,bool) item in addToCalc)
            {
                current = item;
                break;
            }
            addToCalc.Remove(current);

            if (current.Item2){
                locationToCheck = (current.Item1.lastPoint,current.Item1.lastTangent);
            }else{
                locationToCheck = (current.Item1.firstPoint,-current.Item1.firstTangent);
            }

            if (!toCalc.Add(locationToCheck)){
                continue;
            }

            if (current.Item2){
                foreach (Track nextTrack in current.Item1.ConnectionsEnd)
                {
                    if(nextTrack.firstPoint == current.Item1.lastPoint && nextTrack.firstTangent == current.Item1.lastTangent){
                        addToCalc.Add((nextTrack,true));
                    }
                    if(nextTrack.lastPoint == current.Item1.lastPoint && nextTrack.lastTangent == -current.Item1.lastTangent){
                        addToCalc.Add((nextTrack,false));
                    }
                }
            }else{
                foreach (Track nextTrack in current.Item1.ConnectionsBegin)
                {
                    if(nextTrack.firstPoint == current.Item1.firstPoint && nextTrack.firstTangent == -current.Item1.firstTangent){
                        addToCalc.Add((nextTrack,true));
                    }
                    if(nextTrack.lastPoint == current.Item1.firstPoint && nextTrack.lastTangent == current.Item1.firstTangent){
                        addToCalc.Add((nextTrack,false));
                    }
                }
            }
            
        }
        Stop destination = stops[indexStops]; 
        if (!(toCalc.Contains((destination.location,destination.direction))||toCalc.Contains((destination.location,-destination.direction)))){
            return false;
        }
        //setup done








        return true;
        
    }
}
