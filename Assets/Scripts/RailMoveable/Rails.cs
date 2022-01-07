using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rails : MonoBehaviour
{
    [SerializeField] private bool _left;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("RailCollider"))
            return;

        Debug.Log("coll  " + collision.gameObject.name);

        Moveable moveable = collision.gameObject.transform.parent.GetComponent<Moveable>();
        if (moveable != null)
        {
            if (_left)
            {
                //Debug.Log("set l move false");
                moveable.moveLeft = false;
            }
            else
            {
                //Debug.Log("set r move false");
                moveable.moveRight = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("coll ex: " + collision.gameObject.name);
        if (!collision.gameObject.CompareTag("RailCollider"))
            return;

        Debug.Log("coll ex: " + collision.gameObject.name);

        Moveable moveable = collision.gameObject.transform.parent.GetComponent<Moveable>();
        if (moveable != null)
        {
            if (_left)
            {
                //Debug.Log("set l move true");
                moveable.moveLeft = true;
            }
            else
            {
                //Debug.Log("set r move true");
                moveable.moveRight = true;
            }
        }
    }


    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("coll: " + collision.gameObject.name);
    //    Moveable moveable = collision.gameObject.GetComponent<Moveable>();
    //    if (moveable != null)
    //    {
    //        Debug.Log("set move false");
    //        if (_left)
    //        {
    //            moveable.moveLeft = false;
    //        }
    //        else
    //        {
    //            moveable.moveRight = false;
    //        }
    //    }
    //}

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("coll ex: " + collision.gameObject.name);
    //    Moveable moveable = collision.gameObject.GetComponent<Moveable>();
    //    if (moveable != null)
    //    {
    //        Debug.Log("set move true");
    //        if (_left)
    //        {
    //            moveable.moveLeft = true;
    //        }
    //        else
    //        {
    //            moveable.moveRight = true;
    //        }
    //    }
    //}


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.GetComponent<Moveable>() == null)
    //    {
    //        Debug.Log("ignore");
    //        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    //    }
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Moveable>() == null)
    //    {
    //        Debug.Log("ignore2");
    //        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    //    }
    //}
}
