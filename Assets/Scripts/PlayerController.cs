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

    //Cached Comp Reference
    Game game;

    //TODO influence movement speed when pushing/pulling

    //TODO don't allow interaction with an object if already interacting with one


    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Respawn()
    {
        transform.position = game.respawnPoint.position;
    }
}
