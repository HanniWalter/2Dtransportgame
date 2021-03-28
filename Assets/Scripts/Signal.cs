using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : WorldObject
{
    public Sprite sprite_HP_0;
    public Sprite sprite_HP_1;
    public WayPoint wayPoint;
    public bool trackDirection;
    public Vector2 location{
        get{
            return wayPoint.location;
        }
    }

    public float direction{
        get{
            if(trackDirection){
                return wayPoint.direction;
            }
            return Util.oppositeAngle(wayPoint.direction);
        }
    }
    public static Signal newSignal(WayPoint stop,bool direction){
        //GameObject This_Prefab = Resources.Load("Prefabs/SignalPrefab") as GameObject;
        //GameObject newGameObject = Instantiate(This_Prefab, new Vector3(0.5f, 2f, 0), Quaternion.identity);
        GameObject newGameObject = new GameObject();

        //newGameObject.transform.localPosition = new Vector3(0.5f, 2f, 0);
        //newGameObject.transform.eulerAngles = Vector3.forward*90; 
        Signal ret = newGameObject.AddComponent<Signal>(); 
        ret.wayPoint = stop;
        ret.trackDirection = direction;

        return ret;
    }
    void Start()
    {
        gameObject.name = "Signal";
        gameObject.AddComponent<LineRenderer>();
        gameObject.transform.parent = wayPoint.transform;
        if(this.trackDirection){
            this.wayPoint.signalA=this;
        }else{
            this.wayPoint.signalB=this;
        }
    }

    new void Awake(){
        ((WorldObject) this).Awake();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
