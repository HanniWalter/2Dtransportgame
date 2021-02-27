using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelcreation : MonoBehaviour
{
    public WorldData worlddata;
    void Start()
    {        
        var point1 = new Vector2(0,0);
		var point2 = new Vector2(10,10);
		var point3 = new Vector2(60,40);
		var point4 = new Vector2(80,100);
		var point5 = new Vector2(0,100);
		var point6 = new Vector2(100,0);
		var point7 = new Vector2(200,0);
		var point8 = new Vector2(300,-100);

        Track track1 = Track.newTrack(PointCreator.create(point1, point2));
        Track track2 = Track.newTrack(PointCreator.create(point2, point3,track1.lastTangent));
        Track track3 = Track.newTrack(PointCreator.create(point3, point4,track2.lastTangent));
        Track track4 = Track.newTrack(PointCreator.create(point4, point5,track3.lastTangent));
        Track track5 = Track.newTrack(PointCreator.create(point5, point6,track4.lastTangent));
        Track track6 = Track.newTrack(PointCreator.create(point6, point7,track5.lastTangent));
        Track track7 = Track.newTrack(PointCreator.create(point7, point8,track6.lastTangent));
        Track track8 = Track.newTrack(PointCreator.create(point8, point1,track7.lastTangent,track1.firstTangent));

        var point11 = new Vector2(1000,0);
		var point12 = new Vector2(1100,0);
		var point13 = new Vector2(1100,100);
		var point14 = new Vector2(1000,100);

		var point15 = new Vector2(1050,50);

        float c = -50;
        var point20 = new Vector2(1000,c);
		var point21 = new Vector2(1300,c);

        Track track11 = Track.newTrack(PointCreator.create(point11, point12));
        Track track12 = Track.newTrack(PointCreator.create(point12, point13,track11.lastTangent));
        Track track13 = Track.newTrack(PointCreator.create(point13, point14,track12.lastTangent));
        Track track14 = Track.newTrack(PointCreator.create(point14, point11,track13.lastTangent));
        Track track15 = Track.newTrack(PointCreator.create(point15,point11,endTangent: track11.firstTangent));
        Track track16 = Track.newTrack(PointCreator.create(point15,point13,beginTangent: -track15.firstTangent, endTangent: -track12.firstTangent));

        Stop stop1 = Stop.newStop(point12);

        Track track20 = Track.newTrack(PointCreator.create(point20,point21));
        Track track21 = Track.newTrack(PointCreator.create(point11,point21,track11.firstTangent,track20.lastTangent));

        foreach (var item in worlddata.tracks)
        {
            var a = item.ConnectionsBegin;
            var b = item.ConnectionsEnd;
        }
        foreach (var item in worlddata.stops)
        {
            var a = item.GetTracks();
        }
    }

    void Update()
    {
        
    }
}
