using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F
{
    public static Vector2 FindBisection(Vector2 originOne, Vector2 originTwo, Vector2 overlap)
    {
        Vector2 a = overlap - originOne;
        Vector2 b = overlap - originTwo;

        return (b.magnitude * a + a.magnitude * b).normalized;
    }

    public static Vector2 GetOverlap(LightScript m, LightScript n)
    {
        Vector2 a = m.transform.position;
        Vector2 b = m.transform.position + m.direction * m.lightRange;
        Vector2 c = n.transform.position;
        Vector2 d = n.transform.position + n.direction * n.lightRange;
        if (!IsIntersecting(m, n))
        {
            return new Vector2(float.MaxValue, float.MaxValue);
        }

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

    public static int FindClosest(Vector2 origin, params Vector2[] list)
    {
        int output = 0;
        Vector2 start = list[0];
        for (int i = 0; i < list.Length; i++)
        {
            if (Vector2.Distance(list[i], origin) <= Vector2.Distance(start, origin))
            {
                start = list[i];
                output = i;
            }
        }

        return output;
    }

    public static bool IsIntersecting(LightScript m, LightScript n, bool shouldIncludeEndPoints = false)
    {
        Vector2 a = m.transform.position;
        Vector2 b = m.transform.position + m.direction * m.lightRange;
        Vector2 c = n.transform.position;
        Vector2 d = n.transform.position + n.direction * n.lightRange;
        //To avoid floating point precision issues we can add a small value
        float epsilon = 0.00001f;

        bool isIntersecting = false;

        float denominator = (d.y - c.y) * (b.x - a.x) - (d.x - c.x) * (b.y - a.y);

        //Make sure the denominator is > 0, if not the lines are parallel
        if (denominator != 0f)
        {
            float u_a = ((d.x - c.x) * (a.y - c.y) - (d.y - c.y) * (a.x - c.x)) / denominator;
            float u_b = ((b.x - a.x) * (a.y - c.y) - (b.y - a.y) * (a.x - c.x)) / denominator;

            //Are the line segments intersecting if the end points are the same
            if (shouldIncludeEndPoints)
            {
                //Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
                if (u_a >= 0f + epsilon && u_a <= 1f - epsilon && u_b >= 0f + epsilon && u_b <= 1f - epsilon)
                {
                    isIntersecting = true;
                }
            }
            else
            {
                //Is intersecting if u_a and u_b are between 0 and 1
                if (u_a > 0f + epsilon && u_a < 1f - epsilon && u_b > 0f + epsilon && u_b < 1f - epsilon)
                {
                    isIntersecting = true;
                }
            }
        }

        return isIntersecting;
    }
}
