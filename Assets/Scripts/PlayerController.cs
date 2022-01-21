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
