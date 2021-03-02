using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCreator
{
    //for returning
    public Vector2[] points;
    public Vector2[] tangents;
    public float[] angels;
    public bool valid;

    public static float pointsPerUnit = 10f;
    public static float minLength = 2f;
    public static float maxLength = 10000f;

    public static PointCreator create(Vector2 begin,Vector2 end,float? beginAngle = null,float? endAngle = null){
        Vector2? endTangent = null;
        Vector2? beginTangent = null;
        if (beginAngle.HasValue){
            beginTangent=Util.angleToV2(beginAngle.Value);
        }
        if (endAngle.HasValue){
            endTangent=Util.angleToV2(endAngle.Value);
        }
        if (beginTangent.HasValue && endTangent.HasValue){
            var ret = bezierCurvePC(begin,end,beginTangent.Value,endTangent.Value);
            ret.angels = new float[ret.tangents.Length];
            for (int i = 0; i < ret.tangents.Length; i++)
            {
                ret.angels[i] = Mathf.Atan2(ret.tangents[i].y, ret.tangents[i].x)*Mathf.Rad2Deg;
                if (ret.angels[i]<0){
                    ret.angels[i]+=360;
                }
            }
            return ret;
        }
        if (endTangent.HasValue){
            if (Util.sameDirection((end-begin),-endTangent.Value,5)){
                var ret = StraightTwoPoints(begin ,end);
                ret.angels = new float[ret.tangents.Length];
            for (int i = 0; i < ret.tangents.Length; i++)
            {
                ret.angels[i] = Mathf.Atan2(ret.tangents[i].y, ret.tangents[i].x)*Mathf.Rad2Deg;
                if (ret.angels[i]<0){
                    ret.angels[i]+=360;
                }
            }
            return ret;
            }

            PointCreator pc = CircleTwoPointsBeginTangent(end,begin, endTangent.Value);
            System.Array.Reverse(pc.points);
            System.Array.Reverse(pc.tangents);
            for (int i = 0; i < pc.tangents.Length; i++)
            {
                pc.tangents[i] = - pc.tangents[i];
            }
            pc.angels = new float[pc.tangents.Length];
            for (int i = 0; i < pc.tangents.Length; i++)
            {
                pc.angels[i] = Mathf.Atan2(pc.tangents[i].y, pc.tangents[i].x)*Mathf.Rad2Deg;
                if (pc.angels[i]<0){
                    pc.angels[i]+=360;
                }
            }
            return pc;
        }

        if (beginTangent.HasValue){
            //too straight for curve
            if (Util.sameDirection((end-begin),beginTangent.Value,5)){
                var retu= StraightTwoPoints(begin ,end);
                retu.angels = new float[retu.tangents.Length];

                for (int i = 0; i < retu.tangents.Length; i++)
                {
                    retu.angels[i] = Mathf.Atan2(retu.tangents[i].y, retu.tangents[i].x)*Mathf.Rad2Deg;
                    if (retu.angels[i]<0){
                    retu.angels[i]+=360;
                }
                }
                return retu;
            }
            var ret= CircleTwoPointsBeginTangent(begin, end, beginTangent.Value);
            ret.angels = new float[ret.tangents.Length];
            for (int i = 0; i < ret.tangents.Length; i++)
            {
                ret.angels[i] = Mathf.Atan2(ret.tangents[i].y, ret.tangents[i].x)*Mathf.Rad2Deg;
                if (ret.angels[i]<0){
                    ret.angels[i]+=360;
                }
            }
            return ret;

        }else{
            var ret = StraightTwoPoints(begin ,end);
            ret.angels = new float[ret.tangents.Length];
            for (int i = 0; i < ret.tangents.Length; i++)
            {
                ret.angels[i] = Mathf.Atan2(ret.tangents[i].y, ret.tangents[i].x)*Mathf.Rad2Deg;
                if (ret.angels[i]<0){
                    ret.angels[i]+=360;
                }
            }
            return ret;
        }
    }

    private static PointCreator bezierCurvePC(Vector2 begin,Vector2 end,Vector2 beginTangent,Vector2 endTangent){
        PointCreator pointCreator = new PointCreator();
        int numberOfPoints = (int)Mathf.Ceil((begin - end).magnitude * 10);
        pointCreator.points = new Vector2[numberOfPoints+1];
        pointCreator.tangents = new Vector2[numberOfPoints+1];
        for (int i = 0; i <= numberOfPoints; i++)
        {
            float t =(float)i/(float)numberOfPoints;
            pointCreator.points[i]=BezierCurve(begin,end,beginTangent,endTangent,t);
        }
        for (int i = 1; i < numberOfPoints; i++)
        {
            pointCreator.tangents[i]=(pointCreator.points[i+1]-pointCreator.points[i-1]).normalized;
        }
        pointCreator.tangents[0] = beginTangent.normalized;
        pointCreator.tangents[numberOfPoints] = endTangent.normalized;
        return pointCreator;
    }
    private static PointCreator StraightTwoPoints(Vector2 begin, Vector2 end)
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
            pointCreator.tangents[i] = (end-begin).normalized;
        }
        return pointCreator;

    }
    private static PointCreator CircleTwoPointsBeginTangent(Vector2 begin, Vector2 end, Vector2 beginTangent)
    {
        PointCreator pointCreator = new PointCreator();
        //beginTangent = - beginTangent;
        Vector2 middlePoint = lineLineIntersection(begin, Vector2.Perpendicular(beginTangent), 0.5f * begin + 0.5f * end, Vector2.Perpendicular(end - begin));

        float radius = (middlePoint - begin).magnitude;
        float startAngle = Mathf.Atan2(begin.y - middlePoint.y, begin.x - middlePoint.x);
        float endAngle = Mathf.Atan2(end.y - middlePoint.y, end.x - middlePoint.x);

        bool clockwise = (Vector2.SignedAngle((begin - middlePoint), (beginTangent))<0);
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

    private static Vector2 BezierCurve(Vector2 begin,Vector2 end,Vector2 beginTangent,Vector2 endTangent, float t){
        Vector2 a = begin;
        Vector2 b = begin+beginTangent.normalized*(end-begin).magnitude/2;
        Vector2 c = end -endTangent.normalized*(end-begin).magnitude/2;
        Vector2 d = end;
        DebugUtil debugUtil = GameObject.FindObjectOfType<DebugUtil>();
        /*if(t==0){
        debugUtil.Markpoint(a,Color.red);
        debugUtil.Markpoint(b,Color.blue);
        debugUtil.Markpoint(c,Color.green);
        debugUtil.Markpoint(d,Color.yellow);
        }*/

        return (1-t)*(1-t)*(1-t)*a+3*(1-t)*(1-t)*t*b+3*(1-t)*t*t*c+t*t*t*d;
    }    
}
