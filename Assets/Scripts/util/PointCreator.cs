using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCreator
{
    //for returning
    public Vector2[] points;
    public Vector2[] tangents;
    public bool valid;

    public static float pointsPerUnit = 10f;
    public static float minLength = 2f;
    public static float maxLength = 10000f;

    public static PointCreator create(Vector2 begin,Vector2 end,Vector2? beginTangent = null,Vector2? endTangent = null){
        if (beginTangent.HasValue){
            //too straight for curve
            if (Util.sameDirection((begin-end),beginTangent.Value,5)){
                return StraightTwoPoints(begin ,end);
            }
            return CircleTwoPointsBeginTangent(begin, end, beginTangent.Value);

        }else{
            return StraightTwoPoints(begin ,end);
        }
    }

    public static PointCreator StraightTwoPoints(Vector2 begin, Vector2 end)
    {
        PointCreator pointCreator = new PointCreator();

        if ((begin - end).magnitude > maxLength)
        {
            pointCreator.tangents = new Vector2[0];
            pointCreator.points = new Vector2[0];
            pointCreator.valid = false;
            return pointCreator;
        }

        if ((begin - end).magnitude < minLength)
        {
            pointCreator.tangents = new Vector2[0];
            pointCreator.points = new Vector2[0];
            pointCreator.valid = false;
            return pointCreator;
        }

        int numberOfPoints = (int)Mathf.Ceil((begin - end).magnitude * pointsPerUnit);
        Vector2[] PointsLoc = new Vector2[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = 1.0f - ((float)i / (float)(numberOfPoints - 1));
            PointsLoc[i] = Vector2.Lerp(begin, end, (float)i / (numberOfPoints - 1));
        }
        pointCreator.valid = true;
        pointCreator.points = PointsLoc;
        pointCreator.tangents = new Vector2[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            pointCreator.tangents[i] = new Vector2(begin.x - end.x, begin.y - end.y);
        }
        return pointCreator;

    }
    public static PointCreator CircleTwoPointsBeginTangent(Vector2 begin, Vector2 end, Vector2 beginTangent)
    {
        PointCreator pointCreator = new PointCreator();

        Vector2 middlePoint = lineLineIntersection(begin, Vector2.Perpendicular(beginTangent), 0.5f * begin + 0.5f * end, Vector2.Perpendicular(end - begin));

        float radius = (middlePoint - begin).magnitude;
        float startAngle = Mathf.Atan2(begin.y - middlePoint.y, begin.x - middlePoint.x);
        float endAngle = Mathf.Atan2(end.y - middlePoint.y, end.x - middlePoint.x);

        bool clockwise = (Vector2.SignedAngle((begin - middlePoint), (beginTangent))>0);
        if (!clockwise)
        {
            if (startAngle > endAngle)
            {
                endAngle += 2 * Mathf.PI;
            }
        }
        else
        {
            if (startAngle < endAngle)
            {
                startAngle += 2 * Mathf.PI;
            }
        }
        
        float arclength = Mathf.Abs((endAngle - startAngle) * radius);

        if (arclength > maxLength)
        {
            Debug.Log("too Long");
            pointCreator.tangents = new Vector2[0];
            pointCreator.points = new Vector2[0];
            pointCreator.valid = false;
            return pointCreator;
        }

        if (arclength < minLength)
        {
            Debug.Log("too short");
            pointCreator.tangents = new Vector2[0];
            pointCreator.points = new Vector2[0];
            pointCreator.valid = false;
            return pointCreator;
        }

        int numberofPoints = ((int)(arclength * pointsPerUnit)) + 1;
        if (numberofPoints < 2)
        {
            Debug.Log("need more points");
            pointCreator.tangents = new Vector2[0];
            pointCreator.points = new Vector2[0];
            pointCreator.valid = false;
            return pointCreator;
        }
        pointCreator.valid = true;
        pointCreator.points = new Vector2[numberofPoints];
        pointCreator.tangents = new Vector2[numberofPoints];

        for (int i = 0; i < numberofPoints; i++)
        {
            float t = (float)i / (float)(numberofPoints - 1);
            pointCreator.points[i] = LerpCircle(begin, end, middlePoint, radius, startAngle, endAngle, arclength, t);
            pointCreator.tangents[i] = LerpCircleTangent(startAngle, endAngle, clockwise, t);
        }
        pointCreator.points[0] = begin;
        pointCreator.points[pointCreator.points.Length - 1] = end;
        pointCreator.tangents[0] = beginTangent;
        return pointCreator;
    }

    //Lerp around The circlearc with given parameters
    private static Vector2 LerpCircle(Vector2 begin, Vector2 end, Vector2 middlePoint, float radius, float startAngle, float endAngle, float arclength, float t)
    {
        float currentangle = Mathf.Lerp(startAngle, endAngle, t);
        float sin = Mathf.Sin(currentangle);
        float cos = Mathf.Cos(currentangle);
        return middlePoint + (new Vector2(cos, sin) * radius);
    }

    //Lerp around The circlearc with given parameters but returns the tangent of the circle
    private static Vector2 LerpCircleTangent(float startAngle, float endAngle, bool clockwise, float t)
    {
        float currentangle = Mathf.Lerp(startAngle, endAngle, t);
        if (clockwise)
        {
            currentangle -= Mathf.PI / 2;
        }
        else
        {
            currentangle += Mathf.PI / 2;
        }
        float sin = Mathf.Sin(currentangle);
        float cos = Mathf.Cos(currentangle);
        return -new Vector2(cos, sin);
    }

    //doesnt check if there is an intersection first
    //https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection#Given_two_points_on_each_line
    private static Vector2 lineLineIntersection(Vector2 a, Vector2 at, Vector2 c, Vector2 ct)
    {
        at.Normalize();
        ct.Normalize();
        Vector2 b = a + at;
        Vector2 d = c + ct;

        float t = ((a.x - c.x) * (c.y - d.y) - (a.y - c.y) * (c.x - d.x)) / ((a.x - b.x) * (c.y - d.y) - (a.y - b.y) * (c.x - d.x));
        return new Vector2(a.x + t * at.x, a.y + t * at.y);
    }
}
