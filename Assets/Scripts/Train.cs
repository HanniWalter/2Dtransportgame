using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : WorldObject
{
    private static int _num = 0;
    [SerializeField] Track track;
    [SerializeField] int indexTrack;
    [SerializeField] int _indexStops = 0;
    [SerializeField] bool TrackDirection;

    List<(Track,bool)> path = new List<(Track, bool)>();
    //[SerializeField] List<Track> _path = new List<Track>();
    int indexStops{
        get{
            return _indexStops;
        }
        set{
            _indexStops = (value % stops.Count);
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
    }

    // Update is called once per frame
    void Update()
    {
        location = track.points[indexTrack];
        direction = track.angles[indexTrack];
        //Debug.Log(path.Count);
        if (location == stops[indexStops].location && Util.sameDirection(direction , stops[indexStops].direction ,5)||location == stops[indexStops].location && Util.sameDirection(direction,Util.oppositeAngle(stops[indexStops].direction),5)){
            Debug.Log("arrived at: "+stops[indexStops].name);

            indexStops = indexStops +1;
        }
        drive(Time.deltaTime* 40);
    }

    void draw(){
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.transform.localScale *= 3; 
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Square");
    }


    void drive(float distance){
        if (indexTrack==track.points.Length-1 && TrackDirection || indexTrack==0 && !TrackDirection){
            if(path.Count == 0){
                bool a= findPath();
            }
            if (path.Count==0){
                return;
            }
            track = path[0].Item1;
            TrackDirection = path[0].Item2;
            indexTrack=0;
            path.RemoveAt(0);
            /*_path  = new List<Track>();
            foreach(var item in path)
            {
                _path.Add(item.Item1);
            }*/
        }
        if (distance<0.1){
            return;
        }
        if (TrackDirection){
            int PointsToGo = (int) (PointCreator.pointsPerUnit * distance);
            int pointsLeftInTrack = track.points.Length - indexTrack;
            if (PointsToGo>=pointsLeftInTrack){
                indexTrack=track.points.Length-1;
                drive((PointsToGo-pointsLeftInTrack)/PointCreator.pointsPerUnit);
            }else{
                pointsLeftInTrack -= PointsToGo;
                indexTrack = track.points.Length -pointsLeftInTrack;
            }
        }else{
            int PointsToGo = (int) (PointCreator.pointsPerUnit * distance);
            int pointsLeftInTrack = track.points.Length - indexTrack;
            if (PointsToGo>=pointsLeftInTrack){
                indexTrack=0;
                drive((PointsToGo+pointsLeftInTrack-track.points.Length)/PointCreator.pointsPerUnit);
            }else{
                pointsLeftInTrack -= PointsToGo;
                indexTrack = pointsLeftInTrack;
            }
        }   
    }

    bool findPath(){
        HashSet<(Vector2,float)> toCalc = new HashSet<(Vector2,float)>();
        HashSet<(Track,bool)> addToCalc = new HashSet<(Track,bool)>();
        addToCalc.Add((this.track,this.TrackDirection));
        while (addToCalc.Count!=0)

        {
            (Track,bool) current = (null,true); //= 0 true so the compiler shuts up
            (Vector2,float) locationToCheck = (Vector2.zero,0);
            foreach ((Track,bool) item in addToCalc)
            {
                current = item;
                break;
            }
            addToCalc.Remove(current);

            if (current.Item2){
                locationToCheck = (current.Item1.lastPoint,current.Item1.lastAngle);
            }else{
                locationToCheck = (current.Item1.firstPoint,Util.oppositeAngle(current.Item1.firstAngle));
            }

            if (!toCalc.Add(locationToCheck)){
                continue;
            }

            if (current.Item2){
                foreach (Track nextTrack in current.Item1.ConnectionsEnd)
                {
                    if(nextTrack.firstPoint == current.Item1.lastPoint && Util.sameDirection( nextTrack.firstAngle , current.Item1.lastAngle,5)){
                        addToCalc.Add((nextTrack,true));
                    }
                    if(nextTrack.lastPoint == current.Item1.lastPoint &&Util.sameDirection(nextTrack.lastAngle,Util.oppositeAngle(current.Item1.lastAngle),5)){
                        addToCalc.Add((nextTrack,false));
                    }
                }
            }else{
                foreach (Track nextTrack in current.Item1.ConnectionsBegin)
                {
                    if(nextTrack.firstPoint == current.Item1.firstPoint && Util.sameDirection(nextTrack.firstAngle ,Util.oppositeAngle(current.Item1.firstAngle),5)){
                        addToCalc.Add((nextTrack,true));
                    }
                    if(nextTrack.lastPoint == current.Item1.firstPoint && Util.sameDirection(nextTrack.lastAngle, current.Item1.firstAngle,5)){
                        addToCalc.Add((nextTrack,false));
                    }
                }
            }
            
        }
        Stop destination = stops[indexStops];
        //Debug.Log(toCalc.Count);
        foreach (var item in toCalc)
        {
            //Debug.Log(item.Item1);
            //Debug.Log(item.Item2);
        }

        if (!(toCalc.Contains((destination.location,destination.direction))||toCalc.Contains((destination.location,Util.oppositeAngle(destination.direction))))){
            return false;
        }
        //setup done
    
        //dijkstra declaration
        Dictionary<(Vector2,float),float> weights = new Dictionary<(Vector2,float),float>();
        Dictionary<(Vector2,float),List<(Track,bool)>> course = new Dictionary<(Vector2,float),List<(Track,bool)>>();
        //dijkstra init
        foreach ((Vector2,float) item in toCalc)
        {
            weights.Add(item,float.PositiveInfinity);
            course.Add(item,new List<(Track,bool)>());
        }
        if(TrackDirection){
            weights[(track.lastPoint,track.lastAngle)] = 0;
        }else{
            weights[(track.firstPoint,Util.oppositeAngle(track.firstAngle))] = 0;
        }

        //dijkstra iteration
        while(true){
            float min = Mathf.Infinity;
            foreach (var item in toCalc)    
            {
                min = Mathf.Min(weights[item],min);
            }
            (Vector2,float) lowest = (Vector2.zero,0);
            foreach (var item in weights)    
            {
                if(item.Value == min){
                    lowest = item.Key;
                    break;
                }
            }
            List<(Track,bool)> lowestCourse= course[lowest];            
            if (lowest.Item1 == destination.location && Util.sameDirection(lowest.Item2, destination.direction,5)||lowest.Item1 == destination.location && Util.sameDirection(lowest.Item2, Util.oppositeAngle(destination.direction),5)){
                foreach (var item in lowestCourse)
                {

                }
                path = lowestCourse;
                return true;
            }

            foreach (var track in worldData.tracks)
            {
                if(track.firstPoint == lowest.Item1 && Util.sameDirection(track.firstAngle , lowest.Item2,5)){
                    float weight = weights[(track.lastPoint,track.lastAngle)];
                    if (weight>min+track.Lenght){
                        weights[(track.lastPoint,track.lastAngle)] = min+track.Lenght;
                        List<(Track,bool)> newCourse= new List<(Track, bool)>(course[lowest]);
                        newCourse.Add((track,true));
                        course[(track.lastPoint,track.lastAngle)]=newCourse;
                    }
                }
                if(track.lastPoint == lowest.Item1 && Util.sameDirection(track.lastAngle, Util.oppositeAngle(lowest.Item2),5)){
                    float weight = weights[(track.firstPoint,Util.oppositeAngle(track.firstAngle))];
                    if (weight>min+track.Lenght){
                        weights[(track.firstPoint,Util.oppositeAngle(track.firstAngle))] = min+track.Lenght;
                        List<(Track,bool)> newCourse= new List<(Track, bool)>(course[lowest]);
                        newCourse.Add((track,false));
                        course[(track.firstPoint,Util.oppositeAngle(track.firstAngle))]=newCourse;
                    }
                }

            }
            toCalc.Remove(lowest);
        }
        
    }
}
