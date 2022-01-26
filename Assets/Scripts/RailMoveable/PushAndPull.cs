using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPull : MonoBehaviour
{
    public Transform PullPushLeft;
    public Transform PullPushRight;

    private PlayerController _playerController;

    public bool _pushing; //for logic and animation purpose
    public bool _pulling; //for animation purpose
    //[SerializeField] bool flippedSprite; //Serialized for Debug Purposes


    private string _pushPullButton = "A";

    private Moveable _moveable;
    private bool _isMovingObj;

    //State
    float xMovement;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveable!= null)
        {
            if (Input.GetButton(_pushPullButton))
            {
                if (!_playerController.SetInteractionWith(_moveable.gameObject))
                    return;

                xMovement = Input.GetAxisRaw("Horizontal");
                if (xMovement != 0)
                {
                    if (_isMovingObj)
                    {
                        StartAnimation();
                    }

                    //start moving obj but dont start moving obj until next to it
                    if (_isMovingObj || !((PullPushLeft.transform.position.x > _moveable.rightEdge.x && PullPushRight.transform.position.x > _moveable.rightEdge.x) ||
                        (PullPushLeft.transform.position.x < _moveable.leftEdge.x && PullPushRight.transform.position.x < _moveable.leftEdge.x)))
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
                        StartDraggingObj(false);
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
                        StartDraggingObj(true);
                    }
                }
                else
                {
                    _pushing = false;
                    _pulling = false;

                    StopAnimation();

                }
            }
            else if (Input.GetButtonUp(_pushPullButton))
            {
                _playerController.StopInteractionWith(_moveable.gameObject);

                EndDraggingObj();
                _pushing = false;
                _pulling = false;
            }
        }

        
        
    }


    private void StartDraggingObj(bool right)
    {
        if (_isMovingObj)
            return;

        _playerController.jumpEnabled = false;

        _isMovingObj = true;

        //try start moving obj
        //TODO put a little leeway in where the player is already in front of the obj but still starts pushing
        if (!((PullPushLeft.transform.position.x > _moveable.rightEdge.x && PullPushRight.transform.position.x > _moveable.rightEdge.x) ||
        (PullPushLeft.transform.position.x < _moveable.leftEdge.x && PullPushRight.transform.position.x < _moveable.leftEdge.x)))
            return;

        Vector3 targetPos = Vector3.zero;
        if (right)
        {
            targetPos = _pushing ? PullPushRight.position + _moveable.edgeWidhtOffset : PullPushLeft.position - _moveable.edgeWidhtOffset;
        }
        else
        {
            targetPos = _pushing ? PullPushLeft.position - _moveable.edgeWidhtOffset : PullPushRight.position + _moveable.edgeWidhtOffset;
        }

        _moveable.transform.position = new Vector2(targetPos.x, _moveable.transform.position.y); //move the moveable to the X position where the player character's sprite and animation touch it
        _moveable.transform.SetParent(transform, true);

        //TODO adjust player moving speed to simulate weight of moveable

        _moveable.Moving = true;

        if (xMovement != 0)
        {
            StartAnimation();
        }
        
    }

    private void EndDraggingObj()
    {
        if (!_isMovingObj)
            return;

        _playerController.jumpEnabled = true;

        _moveable.transform.parent = null;
        _isMovingObj = false;

        _moveable.Moving = false;

        StopAnimation();

    }

    public void SetMoveable(Moveable moveable, bool putAsMoveable)
    {
        if (putAsMoveable && _moveable == null)
            _moveable = moveable;

        if (!putAsMoveable && _moveable == moveable)
            _moveable = null;
    }


    void StartAnimation()
    {
        //SFX Start
        _moveable.gameObject.GetComponent<SFX>().PlayRails();
        //Animation
        GetComponent<Animator>().SetBool("isPushing", true);
        GetComponent<PlayerMovement>().flipSprite = false;
        if (_moveable.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void StopAnimation()
    {
        //SFX Stop
        _moveable.gameObject.GetComponent<SFX>().PauseRails();
        //Animation Stop
        GetComponent<Animator>().SetBool("isPushing", false);
        GetComponent<PlayerMovement>().flipSprite = true;
    }

}
