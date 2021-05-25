using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : WorldObject
{
    //for nameing
    private static int _num = 0;
    
    public WayPointType type{
        get{
            if(tracks[0] == null) return WayPointType.EmptyWayPoint;
            if(tracks[1] == null) return WayPointType.DeadEnd;
            if(tracks[2] == null){
                if(signals[0] != null || signals[1] != null) return WayPointType.SignalWayPoint;
                else return WayPointType.DefaultWayPoint;
            }
            
            if(tracks[3] != null) return WayPointType.TrackCrossing;

            if(tracks[2].WayPointBegin == this){
                if(tracks[0].WayPointBegin == this){
                    if(Util.sameDirection(Util.oppositeAngle(tracks[2].firstAngle),tracks[0].firstAngle,5)){
                        return WayPointType.TrackSwitch;
                    }else{
                        return WayPointType.UnfinishedTrackCrossing;
                    }                    
                }else if(tracks[0].WayPointEnd == this){
                    if(Util.sameDirection(tracks[2].firstAngle,tracks[0].lastAngle,5)){
                        return WayPointType.TrackSwitch;
                    }else{
                        return WayPointType.UnfinishedTrackCrossing;
                    } 
                }
            }else if(tracks[2].WayPointEnd == this){
                if(tracks[0].WayPointBegin == this){
                    if(Util.sameDirection(tracks[2].lastAngle,tracks[0].firstAngle,5)){
                        return WayPointType.TrackSwitch;
                    }else{
                        return WayPointType.UnfinishedTrackCrossing;
                    }
                }else if(tracks[0].WayPointEnd == this){
                    if(Util.sameDirection(Util.oppositeAngle(tracks[2].lastAngle),tracks[0].lastAngle,5)){
                        return WayPointType.TrackSwitch;
                    }else{
                        return WayPointType.UnfinishedTrackCrossing;
                    }        
                }
            }
            throw new GameLogicException("something doesn't work");
        }
    }
    
    public Track[] tracks = {null,null,null,null};
    public Signal[] signals = {null,null};
    public bool branchoff = false;

    public static WayPoint newWayPoint(Vector2 location){
        WorldData worldData = FindObjectOfType<WorldData>();
        GameObject newGameObject = new GameObject();
		WayPoint ret = newGameObject.AddComponent<WayPoint>(); 
        ret.location = location;
        
		return ret;
    }

    new void Awake(){
        ((WorldObject) this).Awake();
        gameObject.name = "WayPoint "+ _num.ToString();
        _num++;
        gameObject.AddComponent<SpriteRenderer>();
        worldData.wayPoints.Add(this);
    }

    // Start is called before the first frame update
    void Start(){
        transform.localScale*=4;
        draw();
    }

    void OnDestroy(){
        worldData.wayPoints.Remove(this);
        foreach (var track in tracks){
            if(track != null) Destroy(track.gameObject);
        }       
    }

    void draw(){
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Circle");
    }

    public void AddTrack(Track track){
        if (track.firstPoint != location && track.lastPoint != location){
            throw new GameLogicException($"Track \"{ track.name}\" does not touch the WayPoint \"{this.name}\"."); 
        }
        switch (type)
        {
            case WayPointType.EmptyWayPoint:
                tracks[0] = track;
                break;
            case WayPointType.DeadEnd:
                if(tracks[0].WayPointBegin == this){
                    if(track.WayPointBegin == this){
                        if(!Util.sameDirection(tracks[0].firstAngle, Util.oppositeAngle(track.firstAngle),5))
                            throw new GameLogicException($"Track \"{ track.name}\" at \"{this.name}\" don't line up.");  
                    }else if(track.WayPointEnd == this){
                        if(!Util.sameDirection(tracks[0].firstAngle, track.lastAngle,5))
                            throw new GameLogicException($"Track \"{ track.name}\" at \"{this.name}\" don't line up.");  
                    }else{
                        throw new GameLogicException($"Track \"{ track.name}\" does not touch the WayPoint \"{this.name}\"."); 
                    }

                }else if(tracks[0].WayPointEnd == this){
                    if(track.WayPointBegin == this){
                        if(!Util.sameDirection(tracks[0].lastAngle, track.firstAngle,5))
                            throw new GameLogicException($"Track \"{ track.name}\" at \"{this.name}\" don't line up.");  
                    }else if(track.WayPointEnd == this){
                        if(!Util.sameDirection(tracks[0].lastAngle, Util.oppositeAngle(track.lastAngle), 5))
                            throw new GameLogicException($"Track \"{ track.name}\" at \"{this.name}\" don't line up.");  
                    }else{
                        throw new GameLogicException($"Track \"{ track.name}\" does not touch the WayPoint \"{this.name}\"."); 
                    }
                }
                tracks[1] = track;
                break;
            case WayPointType.DefaultWayPoint:
                tracks[2] = track;

                //check if track0 and track1 should be changed
                if(tracks[1].firstPoint == location){
                    if(tracks[2].firstPoint == location){
                        if(Util.sameDirection(tracks[1].firstAngle,Util.oppositeAngle(tracks[2].firstAngle),5)){
                            //swap
                            Track t = tracks[0];
                            tracks[0] = tracks[1];
                            tracks[1] = t; 
                        }
                    }else if(tracks[2].lastPoint == location){
                        if(Util.sameDirection(tracks[1].firstAngle,tracks[2].lastAngle,5)){
                            //swap
                            Track t = tracks[0];
                            tracks[0] = tracks[1];
                            tracks[1] = t; 
                        }
                    }
                }else if(tracks[1].lastPoint == location){
                    if(tracks[2].firstPoint == location){
                        if(Util.sameDirection(tracks[1].lastAngle,tracks[2].firstAngle,5)){
                            //swap
                            Track t = tracks[0];
                            tracks[0] = tracks[1];
                            tracks[1] = t; 
                        }
                    }else if(tracks[2].lastPoint == location){
                        if(Util.sameDirection(tracks[1].lastAngle,Util.oppositeAngle(tracks[2].lastAngle),5)){
                            //swap
                            Track t = tracks[0];
                            tracks[0] = tracks[1];
                            tracks[1] = t; 
                        }
                    }
                }
                break;
            case WayPointType.TrackSwitch:
                throw new GameLogicException($"TrackSwitch \"{this.name}\" can't add track \"{ track.name}\".");
            case WayPointType.SignalWayPoint:
                throw new GameLogicException($"SignalWayPoint \"{this.name}\" can't add track \"{ track.name}\"."); 
            case WayPointType.UnfinishedTrackCrossing:
                if(tracks[2].WayPointBegin == this){
                    if(!Util.sameDirection(tracks[2].firstAngle, Util.oppositeAngle(track.firstAngle),5) && (!Util.sameDirection(tracks[2].firstAngle, track.lastAngle,5))){
                        throw new GameLogicException($"Track \"{ track.name}\" at \"{this.name}\" don't line up."); 
                    }
                }else{
                    if(!Util.sameDirection(tracks[2].lastAngle, Util.oppositeAngle(track.lastAngle),5) && (!Util.sameDirection(tracks[2].lastAngle, track.firstAngle, 5))){
                        throw new GameLogicException($"Track \"{ track.name}\" at \"{this.name}\" don't line up."); 
                    }
                }
                tracks[3] = track;
                break;
            case WayPointType.TrackCrossing:
                throw new GameLogicException($"TrackCrossing \"{this.name}\" can't add track \"{ track.name}\"."); 
        }
        foreach (var t in tracks){
            if (t!=null)t.color();
        }
    }

public void DeleteTrack(Track track){
        if (track.firstPoint != location || track.lastPoint != location){
            throw new GameLogicException($"Track \"{this.name}\" does not touch the WayPoint \""+this.name+"\"."); 
        }
        switch (type)
        {
            case WayPointType.EmptyWayPoint:
                throw new GameLogicException($"Track \"{track.name}\" is not contained in \"{this.name}\".");   
            case WayPointType.DeadEnd:
                tracks[0] = null;
                break;
            case WayPointType.DefaultWayPoint:
                if (track == tracks[0]){
                    tracks[0] = tracks[1];
                    tracks[1] = null;
                    break;
                } else if(track == tracks[1]){
                    tracks[1] = null;
                    break;
                }
                throw new GameLogicException($"Track \"{track.name}\" is not contained in \"{this.name}\".");
            case WayPointType.TrackSwitch:
                if (track == tracks[0]){
                    throw new GameLogicException($"Can't \"{this.name}\" destroy track \"{ track.name}\"");
                } else if(track == tracks[1]){
                    tracks[1] = tracks[2];
                    tracks[2] = null; 
                    break;
                } else if(track == tracks[2]){
                    tracks[2] = null;
                    break;
                }
                throw new GameLogicException($"Track \"{track.name}\" is not contained in \"{this.name}\".");
            case WayPointType.SignalWayPoint:
                throw new GameLogicException($"SignalWayPoint \"{this.name}\" can't delete track \"{ track.name}\"."); 
            case WayPointType.UnfinishedTrackCrossing:
                if (track == tracks[0]){
                    throw new GameLogicException($"UnfinishedTrackCrossing \"{this.name}\" can't delete track \"{ track.name}\"");
                } else if(track == tracks[1]){
                    throw new GameLogicException($"UnfinishedTrackCrossing \"{this.name}\" can't delete track \"{ track.name}\"");
                } else if(track == tracks[2]){
                    tracks[2] = null;
                    break;
                }
                throw new GameLogicException($"Track \"{track.name}\" is not contained in \"{this.name}\".");
            case WayPointType.TrackCrossing:
                if (track == tracks[0]){
                    tracks[0] = tracks[2];
                    tracks[2] = tracks[1];
                    tracks[1] = tracks[3];
                    tracks[3] = null;
                    break;
                } else if(track == tracks[1]){
                    tracks[1] = tracks[2];
                    tracks[2] = tracks[0];
                    tracks[0] = tracks[3];
                    tracks[3] = null;
                    break;
                } else if(track == tracks[2]){
                    tracks[2] = tracks[3];
                    tracks[3] = null;
                    break;
                } else if(track == tracks[3]){
                    tracks[3] = null;
                    break;
                } 
                throw new GameLogicException($"TrackCrossing \"{this.name}\" can't add tracks."); 
        }
    }

    public bool stops(Track lastTrack){
        switch (type)
        {
            case WayPointType.EmptyWayPoint:
                throw new GameLogicException($"EmptyWayPoint \"{this.name}\" has no track.");
            case WayPointType.DeadEnd:
                return true;
            case WayPointType.DefaultWayPoint:
                return false;
            case WayPointType.TrackSwitch:
                return false;
            case WayPointType.SignalWayPoint:
                //TodO
                return false;
            case WayPointType.UnfinishedTrackCrossing:
                if(lastTrack == tracks[0]) return false;
                if(lastTrack == tracks[1]) return false;
                if(lastTrack == tracks[2]) throw new GameLogicException($"TrackCrossing \"{this.name}\" is not finished at track \"{lastTrack.name}\".");
                throw new GameLogicException($"Track \"{lastTrack.name}\" is not contained in \"{this.name}\".");
            case WayPointType.TrackCrossing:
                if(lastTrack == tracks[0]) return tracks[1];
                if(lastTrack == tracks[1]) return tracks[0];
                if(lastTrack == tracks[2]) return tracks[3];
                if(lastTrack == tracks[3]) return tracks[2];
                throw new GameLogicException($"Track \"{lastTrack.name}\" is not contained in \"{this.name}\".");
            default:
                throw new GameLogicException(type+" is not supportet.");
        }
    }

    public Track nextTrack(Track lastTrack){
        switch (type)
        {
            case WayPointType.EmptyWayPoint:
                throw new GameLogicException($"EmptyWayPoint \"{this.name}\" has no track.");
            case WayPointType.DeadEnd:
                throw new GameLogicException($"DeadEnd \"{this.name}\" has no second track.");
            case WayPointType.DefaultWayPoint:
                if(lastTrack == tracks[0]) return tracks[1];
                if(lastTrack == tracks[1]) return tracks[0];
                throw new GameLogicException($"Track \"{lastTrack.name}\" is not contained in \"{this.name}\".");
            case WayPointType.TrackSwitch:
                if(lastTrack == tracks[0]){
                    if (branchoff) return tracks[2];
                    else return tracks[1];
                }
                if(lastTrack == tracks[1]) return tracks[0];
                if(lastTrack == tracks[2]) return tracks[0];               
                throw new GameLogicException($"Track \"{lastTrack.name}\" is not contained in \"{this.name}\".");
            case WayPointType.SignalWayPoint:
                if(lastTrack == tracks[0]) return tracks[1];
                if(lastTrack == tracks[1]) return tracks[0];
                throw new GameLogicException($"Track \"{lastTrack.name}\" is not contained in \"{this.name}\".");
            case WayPointType.UnfinishedTrackCrossing:
                if(lastTrack == tracks[0]) return tracks[1];
                if(lastTrack == tracks[1]) return tracks[0];
                if(lastTrack == tracks[2]) throw new GameLogicException($"TrackCrossing \"{this.name}\" is not finished at track \"{lastTrack.name}\".");
                throw new GameLogicException($"Track \"{lastTrack.name}\" is not contained in \"{this.name}\".");
            case WayPointType.TrackCrossing:
                if(lastTrack == tracks[0]) return tracks[1];
                if(lastTrack == tracks[1]) return tracks[0];
                if(lastTrack == tracks[2]) return tracks[3];
                if(lastTrack == tracks[3]) return tracks[2];
                throw new GameLogicException($"Track \"{lastTrack.name}\" is not contained in \"{this.name}\".");
            default:
                throw new GameLogicException(type+" is not supportet.");
        }
    }

    public override string getInterface(){
        return type.ToString();
    }
}

public enum WayPointType
{
    EmptyWayPoint,
    DeadEnd,
    DefaultWayPoint,
    TrackSwitch,
    SignalWayPoint,
    UnfinishedTrackCrossing,
    TrackCrossing
}