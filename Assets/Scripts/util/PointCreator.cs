using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCreator : MonoBehaviour
{
    //for returning
    public Vector2[] points;
    public Vector2[] tangents;
    public bool valid;


    public static float pointsPerUnit = 10f;
    public static float minLength = 2f;
    public static float maxLength = 100f;

    public static PointCreator RegularTwoPonits(Vector2 begin, Vector2 end)
    {
        return StraightTwoPoints(begin, end);
    }

    public static PointCreator RegularTwoPointsStartTangent(Vector2 begin, Vector2 end, Vector2 beginTangent)
    {
        return CircleTwoPointsBeginTangent(begin, end, beginTangent);
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

        Vector2 start2d = begin;
        Vector2 end2d = end;

        Vector2 middlePoint = lineLineIntersection(begin, Vector2.Perpendicular(beginTangent), 0.5f * begin + 0.5f * end, Vector2.Perpendicular(end - begin));

        float radius = (middlePoint - begin).magnitude;
        /*
        float startAngle = Mathf.Atan2(start2d.y - middlePoint2d.y, start2d.x - middlePoint2d.x);
        float endAngle = Mathf.Atan2(end2d.y - middlePoint2d.y, end2d.x - middlePoint2d.x);

        bool clockwise = (Vector2.SignedAngle((start2d - middlePoint2d), (beginTangent)) < 0);

        if (clockwise)
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
            pointCreator.tangents = new Vector2[0];
            pointCreator.points = new Vector3[0];
            pointCreator.valid = false;
            return pointCreator;
        }

        if (arclength < minLength)
        {
            pointCreator.tangents = new Vector2[0];
            pointCreator.points = new Vector3[0];
            pointCreator.valid = false;
            return pointCreator;
        }

        int numberofPoints = ((int)(arclength * pointsPerUnit)) + 1;
        if (numberofPoints < 2)
        {
            pointCreator.tangents = new Vector2[0];
            pointCreator.points = new Vector3[0];
            pointCreator.valid = false;
            return pointCreator;
        }
        pointCreator.valid = true;
        pointCreator.points = new Vector3[numberofPoints];
        pointCreator.tangents = new Vector2[numberofPoints];

        for (int i = 0; i < numberofPoints; i++)
        {
            float t = (float)i / (float)(numberofPoints - 1);
            pointCreator.points[i] = LerpCircle(begin, end, middlePoint2d, radius, startAngle, endAngle, arclength, t);
            pointCreator.tangents[i] = LerpCircleTangent(startAngle, endAngle, clockwise, t);
        }
        pointCreator.points[0] = begin;
        pointCreator.points[pointCreator.points.Length - 1] = end;
        pointCreator.tangents[0] = beginTangent;*/
        return pointCreator;
    }

    //Lerp around The circlearc with given parameters
    private static Vector3 LerpCircle(Vector3 start, Vector3 end, Vector2 middlePoint2d, float radius, float startAngle, float endAngle, float arclength, float t)
    {

        float currentangle = Mathf.Lerp(startAngle, endAngle, t);
        float height = Mathf.Lerp(start.y, end.y, t);
        float sin = Mathf.Sin(currentangle);
        float cos = Mathf.Cos(currentangle);
        Vector2 point2d = middlePoint2d + (new Vector2(cos, sin) * radius);
        return new Vector3(point2d.x, height, point2d.y);
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
        return new Vector2(cos, sin);
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








    enum PointCreatorType
    {

        //with case
        regularTwoPonits,
        regularTwoPointsStartTangent,
        regularTwoPointsEndTangent,
        regularTwoPointsTwoTangents,


        //
        straightTwoPoints,
        straightPointTangentLength,
        smallcircleTwoPointsEndTangent,
        smallcircleTwoPoints,
        CircleTwoPointsBeginTangent,
        CircleTwoPointsEndTangent
    }
}
