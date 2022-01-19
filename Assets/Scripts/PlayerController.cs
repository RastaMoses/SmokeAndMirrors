using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Config Params
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;

    //State
    public bool jumpEnabled = true;
    public bool movementEnabled = true;

    public Rigidbody2D _rigidbody;

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

    public void Freeze(bool freeze)
    {
        _rigidbody.isKinematic = freeze;
        movementEnabled = !freeze;
    }


    public void Respawn()
    {
        transform.position = game.respawnPoint.position;
    }
}
