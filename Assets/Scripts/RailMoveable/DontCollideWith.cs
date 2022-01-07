using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontCollideWith : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ignore");
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
