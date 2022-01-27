using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Config Params
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] public float jumpDelay = 0.1f;
    [SerializeField] public float ladderSpeed;
    [SerializeField] public float ladderDownSpeed;


    //State
    public bool jumpEnabled = true;
    public bool movementEnabled = true;

    private GameObject _interactingGameobject = null;

    public bool canInteractWith(GameObject obj)
    {
        if(_interactingGameobject == null)
        {
            return true;
        }
        else if(_interactingGameobject == obj)
        {
            return true;
        }
        return false;
    }

    public bool SetInteractionWith(GameObject obj)
    {
        //try to set obj as the object interaction happens with, return true/false depending on if this worked
        if(canInteractWith(obj))
        {
            _interactingGameobject = obj;
            return true;
        }
        return false;
    }

    public bool StopInteractionWith(GameObject obj)
    {
        //stop interaction with obj, return true/false depending on if this worked
        if (_interactingGameobject != null && _interactingGameobject == obj)
        {
            _interactingGameobject = null;
            return true;
        }
        return false;
    }

    //Cached Comp Reference
    Game game;

    //TODO influence movement speed when pushing/pulling

    //TODO don't allow interaction with an object if already interacting with one


    private void Awake()
    {
        game = FindObjectOfType<Game>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Goal()
    {
        movementEnabled = false;
        game.LevelComplete();
    }



    public void Respawn()
    {
        transform.position = game.respawnPoint.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Goal();
        }
    }
}
