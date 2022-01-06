using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPull : MonoBehaviour
{
    public bool MovingPossible = true;

    public bool PullingOrPusing => _pulling || _pushing; //use to enable/disable jumping
    private bool _pushing; //for animation purpose;
    private bool _pulling; //for animation purpose;

    private string _pushPullButton = "A";

    [SerializeField] private Moveable _moveable;
    [SerializeField] private bool _movingObj;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("mov3? " + _moveable == null);
        if (_moveable!= null)
        {
            if (Input.GetButton(_pushPullButton))
            {
                //IF pull button pressed: ...




                float xMovement = Input.GetAxisRaw("Horizontal");
                if (xMovement != 0)
                {
                    //Vector3 direction = transform.position - _moveable.transform.position; 
                    //float dist = direction.magnitude;
                    //Vector3 pullDir = direction.normalized; // short blue arrow from crate to player

                    Vector2 direction = transform.position - _moveable.transform.position;
                    float distance = direction.magnitude;
                    Vector2 simpleDirection = (transform.position - _moveable.transform.position).normalized;

                    float playerRelativePosToMoveable = simpleDirection.x;


                    //go left
                    if (xMovement < 0)
                    {
                        //pulling from left (to left)
                        if (playerRelativePosToMoveable < 0)
                        {
                            _pulling = true;
                            _pulling = false;
                        }
                        //pushing from right (to left)
                        else if (playerRelativePosToMoveable > 0)
                        {
                            _pushing = true;
                            _pulling = false;
                        }

                        if (_moveable.moveLeft)
                        {
                            MovingPossible = true;
                            StartDraggingObj();

                            //float pullF = 10;
                            //// for fun, pull a little bit more if further away:
                            //// (so, random, optional junk):
                            //float pullForDist = (distance - 3) / 2.0f;
                            //if (pullForDist > 20) pullForDist = 20;
                            //pullF += pullForDist;
                            //// Now apply to pull force, using standard meters/sec converted
                            ////    into meters/frame:
                            ////_moveable.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(simpleDirection.x, 0, 0) * (pullF * Time.deltaTime);
                            //_moveable.gameObject.GetComponent<Rigidbody2D>().velocity += simpleDirection * (pullF * Time.deltaTime);
                        }
                        else
                        {
                            //_moveable.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                            MovingPossible = false;
                            EndDraggingObj();
                        }
                    }
                    //go right
                    else if (xMovement > 0)
                    {
                        //pushing from left (to right)
                        if (playerRelativePosToMoveable < 0)
                        {
                            _pushing = true;
                            _pulling = false;
                        }
                        //pulling from right (to right)
                        else if (playerRelativePosToMoveable > 0)
                        {
                            _pulling = true;
                            _pushing = false;
                        }

                        if (_moveable.moveRight)
                        {
                            MovingPossible = true;
                            StartDraggingObj();
                        }
                        else
                        {
                            MovingPossible = false;
                            EndDraggingObj();
                        }
                    }
                    if (MovingPossible)
                    {
                        //_moveable.GetComponent<Rigidbody2D>().isKinematic= false;
                        ////move
                        //float pullF = 10;
                        //// for fun, pull a little bit more if further away:
                        //// (so, random, optional junk):
                        //float pullForDist = (distance - 3) / 2.0f;
                        //if (pullForDist > 20) pullForDist = 20;
                        //pullF += pullForDist;
                        //// Now apply to pull force, using standard meters/sec converted
                        ////    into meters/frame:
                        ////_moveable.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(simpleDirection.x, 0, 0) * (pullF * Time.deltaTime);
                        //_moveable.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(simpleDirection.x, 0) * (pullF * Time.deltaTime);
                    }
                    else
                    {
                        EndDraggingObj();
                    }
                }
                else
                {
                    _pushing = false;
                    _pulling = false;
                }
                if (!MovingPossible)
                {
                    playerController.movementEnabled = false;
                }
                else
                {
                    playerController.movementEnabled = true;
                }
            }
            else if (Input.GetButtonUp(_pushPullButton))
            {
                MovingPossible = true;
                playerController.movementEnabled = true;
                EndDraggingObj();
                _pushing = false;
                _pulling = false;
            }
            //if (!MovingPossible || !_movingObj)
            //{
            //    _moveable.GetComponent<Rigidbody2D>().isKinematic = true;
            //}




        }
        //else
        //{
        //    MovingPossible = true;
        //}

        //playerController.movementEnabled = MovingPossible;
    }


    private void StartDraggingObj()
    {
        if (_movingObj)
            return;

        Debug.Log("start p");


        playerController.jumpEnabled = false;

        Debug.Log("mov1? " + _moveable == null);
        _moveable.transform.parent = transform;

        _movingObj = true;
    }

    private void EndDraggingObj()
    {
        Debug.Log("try end p");

        if (!_movingObj)
            return;

        Debug.Log("end p");

        playerController.jumpEnabled = true;

        _moveable.transform.parent = null;
        _movingObj = false;
    }

    public void SetMoveable(Moveable moveable, bool putAsMoveable)
    {
        if (putAsMoveable && _moveable == null)
            _moveable = moveable;

        if (!putAsMoveable && _moveable == moveable)
            _moveable = null;
    }
}
