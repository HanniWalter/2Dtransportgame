﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : WorldObject
{
    private static int _num = 0;
    public Vector2 location{
        get{
            return Util.toV2(transform.position);
        }
        set{
            transform.position = Util.toV3(value);
        }
    }
    
    public Vector2 direction;
    public Track[] a;


    public static Stop newStop(Vector2 location){
        WorldData worldData = FindObjectOfType<WorldData>();
        GameObject newGameObject = new GameObject();
		Stop ret = newGameObject.AddComponent<Stop>(); 
        ret.location = location;
        
		return ret;
    }

    public Track[] GetTracks(){
        List<Track> ret = new List<Track>();
        foreach(Track t in worldData.tracks){
            if(t.firstPoint == this.location){
                ret.Add(t);
            }
            if(t.lastPoint == this.location){
                
                ret.Add(t);
            }
        }
        a = ret.ToArray();
        return ret.ToArray();
    }

    new void Awake(){
        ((WorldObject) this).Awake();
        gameObject.name = "Stop "+ _num.ToString();
        _num++;
        gameObject.AddComponent<SpriteRenderer>();
        worldData.stops.Add(this);

    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (this.GetTracks()[0].firstPoint == location){
            direction = this.GetTracks()[0].firstTangent;
        }else{
            direction = this.GetTracks()[0].lastTangent;
        }
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
        //SpriteRenderer.sprite
        
        //lineRenderer.positionCount = points.Length;
        //lineRenderer.SetPositions(Util.toV3A(points));
    }
}