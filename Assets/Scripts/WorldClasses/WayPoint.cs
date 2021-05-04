using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : WorldObject
{
    public Signal signalA;
    public Signal signalB;

    //for nameing
    private static int _num = 0;

    public Vector2 location{
        get{
            return Util.toV2(transform.position);
        }
        set{
            transform.position = Util.toV3(value);
        }
    }
    
    public float direction;

    public Dictionary<float,List<Track>> TracksDirection =new Dictionary<float, List<Track>>();

    public List<Track> DEB_Tracks = new List<Track>();
    public List<float> DEB_Direction = new List<float>();

    public void AddTrack(Track track){
        if(track.firstPoint == location){
            if (TracksDirection.ContainsKey(track.firstAngle)){
            }else{
                TracksDirection[track.firstAngle] = new List<Track>();
            }
            TracksDirection[track.firstAngle].Add(track);
            DEB_Tracks.Add(track);
            DEB_Direction.Add(track.firstAngle);
        }
        if(track.lastPoint == location){
            if (TracksDirection.ContainsKey(Util.oppositeAngle(track.lastAngle))){
            }else{
                TracksDirection[Util.oppositeAngle(track.lastAngle)] = new List<Track>();
            }
            TracksDirection[Util.oppositeAngle(track.lastAngle)].Add(track);
            DEB_Tracks.Add(track);
            DEB_Direction.Add(Util.oppositeAngle(track.lastAngle));
        }
    }

    public static WayPoint newWayPoint(Vector2 location){
        WorldData worldData = FindObjectOfType<WorldData>();
        GameObject newGameObject = new GameObject();
		WayPoint ret = newGameObject.AddComponent<WayPoint>(); 
        ret.location = location;
        
		return ret;
    }

    /*public Track[] GetTracks(){
        List<Track> ret = new List<Track>();
        foreach(Track t in worldData.tracks){
            if(t.firstPoint == this.location){
                ret.Add(t);
            }
            if(t.lastPoint == this.location){
                
                ret.Add(t);
            }
        }
        return ret.ToArray();
    }*/

    new void Awake(){
        ((WorldObject) this).Awake();
        gameObject.name = "WayPoint "+ _num.ToString();
        _num++;
        gameObject.AddComponent<SpriteRenderer>();
        worldData.wayPoints.Add(this);

    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale*=4;
        draw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void draw(){
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Circle");
    }

    public Type type{
        get{
            if(signalA != null || signalB != null){
                return Type.Signal;
            }
            if(TracksDirection.Count==1){
                return Type.DeadEnd;
            }
            foreach (var item in TracksDirection.Values)
            {
                if(item.Count==2){
                    return Type.Switch;
                }
            }
            return Type.Default;
        }
    }

    public enum Type
    {
        Default,
        DeadEnd,
        Signal,
        Switch
    }
}
