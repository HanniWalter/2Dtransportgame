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

    void setUpDraw(){
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.transform.localScale = new Vector3(Lenght*0.9f, 3,0); 
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Square");
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = train.gameObject.transform;
        setUpDraw();
    }

    public void draw(int vehicleOffset){
        var list = new List<Vector2>(train.locationPoints);
        list.Reverse();
        var pointa=list[vehicleOffset];
        var pointb=list[vehicleOffset+(int)(Lenght*0.9f*PointCreator.pointsPerUnit)];
        transform.position = Util.toV3((pointa+pointb)/2f);
        transform.eulerAngles= new Vector3(0,0,Mathf.Rad2Deg*Mathf.Atan2((pointb-pointa).y,(pointb-pointa).x));
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
