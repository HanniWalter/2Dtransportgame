using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Util{
    static public Vector3 toV3(Vector2 V2){
        return new Vector3(V2.x,V2.y,0);
    }

    static public Vector2 toV2(Vector3 V3){
        return new Vector2(V3.x,V3.y);
    }

    static public Vector3[] toV3A(Vector2[] V2s){
        Vector3[] ret = new Vector3[V2s.Length];
        for(int i = 0;i<V2s.Length;i++){
            ret[i] = toV3(V2s[i]);
        }
        return ret;
    }

    static public Vector2[] toV2A(Vector3[] V3s){

        Vector2[] ret = new Vector2[V3s.Length];
        for(int i = 0;i<V3s.Length;i++){
            ret[i] = toV2(V3s[i]);
        }
        return ret;
    }

    static public bool sameDirection(Vector2 v1,Vector2 v2, float degree)
    {
        return Mathf.Abs( Vector2.SignedAngle(v1,v2))<degree;
    }  

    static public bool sameDirection(float a1,float a2, float degree)
    {
        //Debug.Log(Mathf.Abs(Mathf.DeltaAngle(a1,a2)));

        return Mathf.Abs(Mathf.DeltaAngle(a1,a2))<degree;
    }  

    static public Vector2 angleToV2(float angle){
        return new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad),Mathf.Sin(angle*Mathf.Deg2Rad)).normalized;
    }

    static public float oppositeAngle(float angle){
        return (angle+180)%360;
    }
}