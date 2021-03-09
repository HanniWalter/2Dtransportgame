using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public Sprite sprite_HP_0;
    public Sprite sprite_HP_1;
    WayPoint stop;
    bool trackDirection;
    public Vector2 location{
        get{
            return stop.location;
        }
    }

    public float direction{
        get{
            if(trackDirection){
                return stop.direction;
            }
            return Util.oppositeAngle(stop.direction);
        }
    }
    public static Signal newSignal(WayPoint stop,bool direction){
        GameObject This_Prefab = Resources.Load("Prefabs/SignalPrefab") as GameObject;
        GameObject newGameObject = Instantiate(This_Prefab, new Vector3(0.5f, 2f, 0), Quaternion.identity);
		newGameObject.transform.parent = stop.transform;
        newGameObject.transform.localPosition = new Vector3(0.5f, 2f, 0);
        newGameObject.transform.eulerAngles = Vector3.forward*90; 
        Signal ret = newGameObject.GetComponent<Signal>(); 
        ret.stop = stop;
        ret.trackDirection = direction;
        if(ret.trackDirection){
            ret.stop.signalA=ret;
        }else{
            ret.stop.signalB=ret;
        }
        
		return ret;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
