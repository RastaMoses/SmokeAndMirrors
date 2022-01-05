using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions
{
    public static List<EdgeCollider2D> FilterEdgeColliders(List<Collider2D> colliderList)
    {
        List<EdgeCollider2D> outputList = new List<EdgeCollider2D>();
        foreach (Collider2D col in colliderList)
        {
            if (col.GetType() == typeof(EdgeCollider2D))
            {
                outputList.Add((EdgeCollider2D)col);
            }
        }
        return outputList;
    }

    public static Vector2 FindEdgeOverlap(EdgeCollider2D edgeOne, EdgeCollider2D edgeTwo)
    {
        Vector2 a = PointToVector(edgeOne.points[edgeOne.pointCount-2], edgeOne.gameObject);
        Vector2 b = PointToVector(edgeOne.points[edgeOne.pointCount-1], edgeOne.gameObject);

        Vector2 c = PointToVector(edgeTwo.points[edgeTwo.pointCount-2], edgeTwo.gameObject);
        Vector2 d = PointToVector(edgeTwo.points[edgeTwo.pointCount-1], edgeTwo.gameObject);

        float a1 = b.y - a.y;
        float b1 = a.x - b.x;
        float c1 = a1 * a.x + b1 * a.y;

        float a2 = d.y - c.y;
        float b2 = c.x - d.x;
        float c2 = a2 * c.x + b2 * c.y;

        float det = a1 * b2 - a2 * b1;

        if (det == 0)
        {
            return Vector2.zero;
        }
        else
        {
            float x = (b2 * c1 - b1 * c2) / det;
            float y = (a1 * c2 - a2 * c1) / det;
            return new Vector2(x, y);
        }
    }

    public static Vector2 FindBisection(Vector2 originOne, Vector2 originTwo, Vector2 overlap)
    {
        Vector2 midPoint = (originOne + originTwo)/2;
        return (overlap - midPoint).normalized;
    }

    public static Vector2 PointToVector(Vector2 point, GameObject obj)
    {
        return obj.transform.position + point.x * obj.transform.right + point.y * obj.transform.up;
    }

    public static List<Vector2> PointsToVectors(EdgeCollider2D ec)
    {
        List<Vector2> outputList = new List<Vector2>();
        List<Vector2> inputList = new List<Vector2>();
        ec.GetPoints(inputList);
        foreach (Vector2 point in inputList)
        {
            outputList.Add(PointToVector(point, ec.gameObject));
        }

        return outputList;
    }

    public static Vector2 VectorToPoint(Vector2 vector, GameObject obj)
    {
        return obj.transform.position - obj.transform.right * vector.x - obj.transform.up * vector.y;
        // return new Vector2(vector.x * obj.transform.right.magnitude - obj.transform.position.x, vector.y * obj.transform.up.magnitude - obj.transform.position.y);
    }

    public static List<Vector2> VectorsToPoints(List<Vector2> vectors, GameObject gameObject)
    {
        List<Vector2> outputList = new List<Vector2>();
        foreach (Vector2 vector in vectors)
        {
            outputList.Add(VectorToPoint(vector, gameObject));
        }

        return outputList;
    }
}
