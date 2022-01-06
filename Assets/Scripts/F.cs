using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F
{
    public static bool onSegment(Vector2 p, Vector2 q, Vector2 r)
    {
        if (q.x <= Mathf.Max(p.x, r.x) && q.x >= Mathf.Min(p.x, r.x) && q.y <= Mathf.Max(p.y, r.y) && q.y >= Mathf.Min(p.y, r.y))
        {
            return true;
        }

        return false;
    }

    public static float orientation(Vector2 p, Vector2 q, Vector2 r)
    {
        float val = (q.y - p.y) * (r.x - q.x) -
                (q.x - p.x) * (r.y - q.y);

        if (val == 0) return 0; // collinear

        return (val > 0) ? 1 : 2; // clock or counterclock wise
    }
    public static bool Intersect(LightScript m, LightScript n)
    {
        Vector2 p1 = m.points[m.points.Count - 2];
        Vector2 q1 = m.points[m.points.Count - 1];
        Vector2 p2 = n.points[n.points.Count - 2];
        Vector2 q2 = n.points[n.points.Count - 1];

        float o1 = orientation(p1, q1, p2);
        float o2 = orientation(p1, q1, q2);
        float o3 = orientation(p2, q2, p1);
        float o4 = orientation(p2, q2, q1);

        // General case
        if (o1 != o2 && o3 != o4)
            return true;

        // Special Cases
        // p1, q1 and p2 are collinear and p2 lies on segment p1q1
        if (o1 == 0 && onSegment(p1, p2, q1)) return true;

        // p1, q1 and q2 are collinear and q2 lies on segment p1q1
        if (o2 == 0 && onSegment(p1, q2, q1)) return true;

        // p2, q2 and p1 are collinear and p1 lies on segment p2q2
        if (o3 == 0 && onSegment(p2, p1, q2)) return true;

        // p2, q2 and q1 are collinear and q1 lies on segment p2q2
        if (o4 == 0 && onSegment(p2, q1, q2)) return true;

        return false; // Doesn't fall in any of the above cases
    }
    public static Vector2 GetOverlap(LightScript m, LightScript n)
    {
        if (!Intersect(m, n)) return new Vector2(float.MaxValue, float.MaxValue);

        Vector2 a = m.points[m.points.Count - 2];
        Vector2 b = m.points[m.points.Count - 1];
        Vector2 c = n.points[n.points.Count - 2];
        Vector2 d = n.points[n.points.Count - 1];

        // Line AB represented as a1x + b1y = c1 
        float a1 = b.y - a.y;
        float b1 = a.x - b.x;
        float c1 = a1 * (a.x) + b1 * (a.y);

        // Line CD represented as a2x + b2y = c2 
        float a2 = d.y - c.y;
        float b2 = c.x - d.x;
        float c2 = a2 * (c.x) + b2 * (c.y);

        float determinant = a1 * b2 - a2 * b1;

        if (determinant == 0)
        {
            // The lines are parallel. This is simplified 
            // by returning a pair of FLT_MAX 
            return new Vector2(float.MaxValue, float.MaxValue);
        }
        else
        {
            float x = (b2 * c1 - b1 * c2) / determinant;
            float y = (a1 * c2 - a2 * c1) / determinant;
            return new Vector2(x, y);
        }

    }
}
