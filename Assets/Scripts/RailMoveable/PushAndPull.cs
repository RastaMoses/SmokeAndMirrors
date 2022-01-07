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

    public Transform PullPushLeft;
    public Transform PullPushRight;

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

                    //dont start moving obj until next to it
                    if (_movingObj || !((PullPushLeft.transform.position.x > _moveable.RightEdge.x && PullPushRight.transform.position.x > _moveable.RightEdge.x) ||
                        (PullPushLeft.transform.position.x < _moveable.LeftEdge.x && PullPushRight.transform.position.x < _moveable.LeftEdge.x)))
                        return;

                    Vector2 direction = transform.position - _moveable.transform.position;
                    float distance = direction.magnitude;
                    Vector2 simpleDirection = (transform.position - _moveable.transform.position).normalized;

                    float playerRelativePosToMoveable = simpleDirection.x;

                    //go left
                    if (xMovement < 0)
                    {
                        //pulling from left(to left)
                        if (playerRelativePosToMoveable < 0)
                        {
                            _pulling = true;
                            _pushing = false;
                        }
                        //pushing from right (to left)
                        else if (playerRelativePosToMoveable > 0)
                        {
                            _pushing = true;
                            _pulling = false;
                        }

                        //if (PullPushRight.position.x <= _moveable.LeftEdge.x)
                        //{
                        //    _pulling = true;
                        //    _pushing = false;
                        //}
                        ////pushing from right (to left)
                        //else if (PullPushLeft.position.x >= _moveable.RightEdge.x)
                        //{
                        //    _pushing = true;
                        //    _pulling = false;
                        //}

                        if (_moveable.moveLeft)
                        {
                            MovingPossible = true;
                            StartDraggingObj(false);
                        }
                        else
                        {
                            MovingPossible = false;
                            //EndDraggingObj();
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

                        ////pushing from left (to right)
                        //if (PullPushRight.position.x <= _moveable.LeftEdge.x)
                        //{
                        //    _pushing = true;
                        //    _pulling = false;
                        //}
                        ////pulling from right (to right)
                        //else if (PullPushRight.position.x >= _moveable.RightEdge.x)
                        //{
                        //    _pulling = true;
                        //    _pushing = false;
                        //}

                        if (_moveable.moveRight)
                        {
                            MovingPossible = true;
                            StartDraggingObj(true);
                        }
                        else
                        {
                            MovingPossible = false;
                           // EndDraggingObj();
                        }
                    }
                    if (MovingPossible)
                    {
                        if (playerController.movementEnabled)
                            return;
                        playerController.movementEnabled = true;
                        Debug.Log("X set playr movement true");
                    }
                    else
                    {
                        if (!playerController.movementEnabled)
                            return;
                        playerController.movementEnabled = false;
                        Debug.Log("X set playr movement false");

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
                    if (!playerController.movementEnabled)
                        return;
                    playerController.movementEnabled = false;
                    Debug.Log("set playr movement false");
                }
                else
                {
                    if (playerController.movementEnabled)
                        return;
                    playerController.movementEnabled = true;
                    Debug.Log("set playr movement true");
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


    private void StartDraggingObj(bool right)
    {
        if (_movingObj)
            return;

        //Debug.Log("start p");


        playerController.jumpEnabled = false;

        _movingObj = true;


        if (!((PullPushLeft.transform.position.x > _moveable.RightEdge.x && PullPushRight.transform.position.x > _moveable.RightEdge.x) ||
    (PullPushLeft.transform.position.x < _moveable.LeftEdge.x && PullPushRight.transform.position.x < _moveable.LeftEdge.x)))
            return;

        Vector3 targetPos = Vector3.zero;
        if (right)
        {
            targetPos = _pushing ? PullPushRight.position + _moveable.EdgeWidhtOffset : PullPushLeft.position - _moveable.EdgeWidhtOffset;
        }
        else
        {
            targetPos = _pushing ? PullPushLeft.position - _moveable.EdgeWidhtOffset : PullPushRight.position + _moveable.EdgeWidhtOffset;
        }

        _moveable.transform.position = targetPos;
        _moveable.transform.parent = transform;

        _moveable.Moving = true;
    }

    private void EndDraggingObj()
    {
       // Debug.Log("try end p");

        if (!_movingObj)
            return;

        //Debug.Log("end p");

        playerController.jumpEnabled = true;

        _moveable.transform.parent = null;
        _movingObj = false;

        _moveable.Moving = false;
    }

    public void SetMoveable(Moveable moveable, bool putAsMoveable)
    {
        if (putAsMoveable && _moveable == null)
            _moveable = moveable;

        if (!putAsMoveable && _moveable == moveable)
            _moveable = null;
    }
}
