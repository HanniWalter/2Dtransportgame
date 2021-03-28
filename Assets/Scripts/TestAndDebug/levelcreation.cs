using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelcreation : MonoBehaviour
{
    int updateCalled = 0;
    public WorldData worlddata;
    void Start()
    {        
        var point1 = WayPoint.newWayPoint(new Vector2(0,0));
		var point2 = WayPoint.newWayPoint(new Vector2(10,10));
		var point3 = WayPoint.newWayPoint(new Vector2(60,40));
		var point4 = WayPoint.newWayPoint(new Vector2(80,100));
		var point5 = WayPoint.newWayPoint(new Vector2(0,100));
		var point6 = WayPoint.newWayPoint(new Vector2(100,0));
		var point7 = WayPoint.newWayPoint(new Vector2(200,0));
		var point8 = WayPoint.newWayPoint(new Vector2(300,-100));

        Track track1 = Track.newTrack(PointCreator.create(point1.location, point2.location),point1,point2);
        Track track2 = Track.newTrack(PointCreator.create(point2.location, point3.location,track1.lastAngle),point2,point3);
        Track track3 = Track.newTrack(PointCreator.create(point3.location, point4.location,track2.lastAngle),point3,point4);
        Track track4 = Track.newTrack(PointCreator.create(point4.location, point5.location,track3.lastAngle),point4,point5);
        Track track5 = Track.newTrack(PointCreator.create(point5.location, point6.location,track4.lastAngle),point5,point6);
        Track track6 = Track.newTrack(PointCreator.create(point6.location, point7.location,track5.lastAngle),point6,point7);
        Track track7 = Track.newTrack(PointCreator.create(point7.location, point8.location,track6.lastAngle),point7,point8);
        Track track8 = Track.newTrack(PointCreator.create(point8.location, point1.location,track7.lastAngle,track1.firstAngle),point8,point1);

        var point11 = WayPoint.newWayPoint(new Vector2(1000,0));
		var point12 = WayPoint.newWayPoint(new Vector2(1100,0));
		var point13 = WayPoint.newWayPoint(new Vector2(1100,100));
		var point14 = WayPoint.newWayPoint(new Vector2(1000,100));

		var point15 = WayPoint.newWayPoint(new Vector2(1050,50));

        float c = 20;
        var point20 = WayPoint.newWayPoint(new Vector2(1000,-c));
		var point21 = WayPoint.newWayPoint(new Vector2(1200,-c));
		var point22 = WayPoint.newWayPoint(new Vector2(1200,100+c));
		var point23 = WayPoint.newWayPoint(new Vector2(1000,100+c));
        var point24 = WayPoint.newWayPoint(new Vector2(950-c,50));

        Track track11 = Track.newTrack(PointCreator.create(point11.location, point12.location),point11,point12);
        Track track12 = Track.newTrack(PointCreator.create(point12.location, point13.location,track11.lastAngle),point12,point13);
        Track track13 = Track.newTrack(PointCreator.create(point13.location, point14.location,track12.lastAngle),point13,point14);
        Track track14 = Track.newTrack(PointCreator.create(point14.location, point11.location,track13.lastAngle),point14,point11);
        Track track15 = Track.newTrack(PointCreator.create(point15.location,point11.location,endAngle: track11.firstAngle),point15,point11);
        Track track16 = Track.newTrack(PointCreator.create(point15.location,point13.location,beginAngle: Util.oppositeAngle(track15.firstAngle), endAngle: Util.oppositeAngle(track13.firstAngle)),point15,point13);

        Track track20 = Track.newTrack(PointCreator.create(point20.location,point21.location),point20,point21);
        Track track21 = Track.newTrack(PointCreator.create(point11.location,point21.location,track11.firstAngle,track20.lastAngle),point11,point21);
        Track track22 = Track.newTrack(PointCreator.create(point21.location,point22.location,track20.lastAngle),point21,point22);
        Track track23 = Track.newTrack(PointCreator.create(point22.location,point23.location,track22.lastAngle),point22,point23);
        Track track24 = Track.newTrack(PointCreator.create(point23.location,point24.location,track22.lastAngle),point23,point24);
        Track track25 = Track.newTrack(PointCreator.create(point24.location,point20.location,track24.lastAngle,track20.firstAngle),point24,point20);
        Track track26 = Track.newTrack(PointCreator.create(point22.location,point14.location,Util.oppositeAngle( track20.lastAngle),track13.lastAngle),point22,point14);

        Signal signal1 = Signal.newSignal(point12,true);
        Signal signal2 = Signal.newSignal(point23,false);

        
   /*   Debug.Log(item.name);
            var min = Mathf.Infinity;
            var max = 0f;

          for (int i = 1; i < item.points.Length; i++)
            {
                var dist = (item.points[i-1]-item.points[i]).magnitude;
                min = Mathf.Min(min,dist);
                max = Mathf.Max(max,dist);
            }
            Debug.Log(max);
            Debug.Log(min);*/
        

        Train train1 = Train.newTrain(track12,true);
        train1.stops.Add(signal1);
        train1.stops.Add(signal2);

       Vehicle vehicle1 = Vehicle.newVehicle(8,train1);
       Vehicle vehicle2 = Vehicle.newVehicle(6,train1);
       Vehicle vehicle3 = Vehicle.newVehicle(6,train1);
       Vehicle vehicle4 = Vehicle.newVehicle(6,train1);
       Vehicle vehicle5 = Vehicle.newVehicle(6,train1);
    }

    void Update()
    {
        if (updateCalled==0){
            foreach (var item in worlddata.tracks)
            {
                var a = item.ConnectionsBegin;
                var b = item.ConnectionsEnd;
            }
        }


        updateCalled++;
    }
}
