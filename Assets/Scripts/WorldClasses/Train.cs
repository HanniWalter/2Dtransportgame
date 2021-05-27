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

    //List<(Track,bool)> path = new List<(Track, bool)>();

    public List<Vehicle> vehicles = new List<Vehicle>();

    public float Lenght 
    {
        get{
            var ret = 0f;
            foreach (var item in vehicles)
            {
                ret += item.Lenght;
            }
            return ret;
        }
    }
    //[SerializeField] List<Track> _path = new List<Track>();
    /*int indexStops{
        get{
            return _indexStops;
        }
        set{
            _indexStops = (value % stops.Count);
        }
    } */ 
    
    public Queue<Vector2> locationPoints;
    /*public Queue<Vector2> locationPoints{
        set{
            _locationPoints = value;
            debug_locationPoints = value.ToArray();
        }
        get{
            return _locationPoints;
        }
    }*/
    //[SerializeField] Vector2[] debug_locationPoints;
    //public List<WayPoint> stops = new List<WayPoint>();
    
    public static Train newTrain(Track track, bool TrackDirection){
        GameObject newGameObject = new GameObject();
		Train ret = newGameObject.AddComponent<Train>(); 
        if (!TrackDirection){
            ret.indexTrack = 1;
        }else{
            ret.indexTrack = track.points.Length-2;
        }
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
        int index1 = indexTrack;
        bool direction1 = TrackDirection;
        Track track1 = track;
        //Debug.Log((int) (Lenght*PointCreator.pointsPerUnit));
        locationPoints = new Queue<Vector2>((int) (Lenght*PointCreator.pointsPerUnit));
        for (int i = 0; i < (int) (1.1 *Lenght*PointCreator.pointsPerUnit); i++)
        {
            locationPoints.Enqueue((track1.points[index1]));
            if(!TrackDirection){
                index1++;
            }else{
                index1--;
            }
        }
        var list = new List<Vector2>( locationPoints.ToArray());
        list.Reverse();
        locationPoints = new Queue<Vector2>(list);

    }

    // Update is called once per frame
    void Update()
    {
        location = track.points[indexTrack];
        direction = track.angles[indexTrack];
        /*if (location == stops[indexStops].location && Util.sameDirection(direction, stops[indexStops].direction ,5)){
            Debug.Log("arrived at: "+stops[indexStops].wayPoint.name);

            indexStops = indexStops +1;
        
        }*/
        int vehicleOffset = 0;
        drive(Time.deltaTime* 40);
        foreach (var item in vehicles)
        {
            item.draw(vehicleOffset);
            vehicleOffset += (int)(item.Lenght * PointCreator.pointsPerUnit);
        }
    }

    void locationPointsUpdate(int start,int end){
        if(end>start){
            for (int i = start; i <= end; i++)
            {
                locationPoints.Enqueue(track.points[i]);
            }
        }else{
            for (int i = start - 1; i >= end ; i--)
            {
                locationPoints.Enqueue(track.points[i]);
            }      
        }
        int maxlenght = (int)(1.1 * Lenght*PointCreator.pointsPerUnit); 
        for (int i = maxlenght; i < locationPoints.Count; i++)
        {
            locationPoints.Dequeue();
        }
    }

    void drive(float distance){
        if (indexTrack==track.points.Length-1 && TrackDirection){
            WayPoint wp = track.WayPointEnd;
            track = wp.nextTrack(track);
            if(wp == track.WayPointBegin){
                indexTrack = 0;
                TrackDirection = true;
            }else{
                indexTrack = track.points.Length-1;
                TrackDirection = false;
            }
            drive(distance);
            return;
        }

        if (indexTrack==0 && !TrackDirection){
            WayPoint wp = track.WayPointBegin;
            track = wp.nextTrack(track);
            if(wp == track.WayPointBegin){
                indexTrack = 0;
                TrackDirection = true;
            }else{
                indexTrack = track.points.Length-1;
                TrackDirection = false;
            }
            drive(distance);
            return;
        }
        if (distance<0.001){
            return;
        }
        if (TrackDirection){
            int PointsToGo = (int) (PointCreator.pointsPerUnit * distance);
            int pointsLeftInTrack = track.points.Length - indexTrack;
            if (PointsToGo>=pointsLeftInTrack){
                locationPointsUpdate(indexTrack,track.points.Length-1);
                indexTrack=track.points.Length-1;
                drive((PointsToGo-pointsLeftInTrack)/PointCreator.pointsPerUnit);
                return;
            }else{
                pointsLeftInTrack -= PointsToGo;
                locationPointsUpdate(indexTrack, track.points.Length -pointsLeftInTrack);
                indexTrack = track.points.Length -pointsLeftInTrack;
            }
            return;
        }else{
            int PointsToGo = (int) (PointCreator.pointsPerUnit * distance);
            int pointsLeftInTrack = indexTrack;
            if (PointsToGo>=pointsLeftInTrack){
                locationPointsUpdate(indexTrack,0);
                indexTrack=0;
                drive((PointsToGo+pointsLeftInTrack-track.points.Length)/PointCreator.pointsPerUnit);
                return;
            }else{
                pointsLeftInTrack -= PointsToGo;
                locationPointsUpdate(indexTrack, pointsLeftInTrack);
                indexTrack = pointsLeftInTrack;
            }
            return;
        }   
    }

    /*bool findPath(){
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
                    if(nextTrack.firstPoint == current.Item1.lastPoint && Util.sameDirection(nextTrack.firstAngle , current.Item1.lastAngle,5)){
                        addToCalc.Add((nextTrack,true));
                    }
                    if(nextTrack.lastPoint == current.Item1.lastPoint && Util.sameDirection(nextTrack.lastAngle,Util.oppositeAngle(current.Item1.lastAngle),5)){
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
        Signal destination = stops[indexStops];
        //Debug.Log(toCalc.Count);
        foreach (var item in toCalc)
        {
            //Debug.Log(item.Item1);
            //Debug.Log(item.Item2);
        }

        if (!(toCalc.Contains((destination.location,destination.direction)))){
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
            if (lowest.Item1 == destination.location && Util.sameDirection(lowest.Item2, destination.direction,5)){
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
        
    }*/
}
