using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : WorldObject
{
    private Vector2[] path = new Vector2[0];
    private static int _num = 0;
    Track track;
    int indexTrack;
    int _indexStops = 0;
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
    
    public static Train newTrain(Track track,int pointnum){
        GameObject newGameObject = new GameObject();
		Train ret = newGameObject.AddComponent<Train>(); 
        ret.indexTrack = pointnum;
        ret.track = track;
        
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
        direction = Mathf.Atan2(track.tangents[indexTrack].y,track.tangents[indexTrack].x)*Mathf.Rad2Deg;
    }

    void draw(){
        gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Square");
    }




}
