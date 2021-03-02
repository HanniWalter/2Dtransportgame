using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : WorldObject
{
    static int _num =0;
    
    public Train train;
    public float Lenght;

    new void Awake(){
        ((WorldObject) this).Awake();
        gameObject.name = "Vehicle "+ _num.ToString();
        _num++;

    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = train.gameObject.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vehicle newVehicle(float Lenght, Train train){
        GameObject newGameObject = new GameObject();
		Vehicle ret = newGameObject.AddComponent<Vehicle>(); 
        ret.Lenght = Lenght;
        ret.train = train;
        train.vehicles.Add(ret);
        
		return ret;
    }
}
