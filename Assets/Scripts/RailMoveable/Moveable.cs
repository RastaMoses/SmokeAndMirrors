using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField] private GameObject _buttonUI;
    //[SerializeField] private Transform _railCheckLeft;
    //[SerializeField] private Transform _railCheckRight;

    //public bool moveLeft { get; private set; }
    //public bool moveRight { get; private set; }

    //public bool moveLeft = true;
    //public bool moveRight = true;

    [SerializeField] private bool _moveLeft;
    [SerializeField] private bool _moveRight;

    public bool moveLeft { get { return _moveLeft; } set { _moveLeft = value; if (value == false) _lockedXPos = transform.position.x; } }
    public bool moveRight { get { return _moveRight; } set { _moveRight = value; if (value == false) _lockedXPos = transform.position.x; } }

    private float _lockedXPos = 0;

    private bool _interactable = false;

    private Vector3 _startPos;

    public Vector3 LeftEdge => transform.position - EdgeWidhtOffset;
    public Vector3 RightEdge => transform.position + EdgeWidhtOffset;

    public Vector3 EdgeWidhtOffset { get; private set; }

    public GameObject _colliderObj;

    private bool _moving;
    //public bool Moving { get { return _moving; } set { _moving = value;  } }
    public bool Moving { get { return _moving;  } set { _moving = value; _colliderObj.SetActive(_moving); }  }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        moveRight = true;

        _colliderObj.SetActive(false);

        _buttonUI.SetActive(false);
        _startPos = transform.position;

        //Vector3 offset = transform.up * (transform.localScale.y / 2f) * -1f;
        //Vector3 pos = transform.position + offset; //This is bottom Y edge

        //Debug.DrawLine(pos, transform.position);

        Vector3 offset1 = -(transform.right * (transform.localScale.x / 2f) * -1f);
        Vector3 pos1 = transform.position - offset1; //This is left X edge

        EdgeWidhtOffset = offset1;
       // LeftEdge = pos1;
        //RightEdge = transform.position - offset1;

        //Debug.DrawLine(pos1, transform.position, Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        //find out if player left or right and which direction input to find out if pull or push
        //ground check on left and right for rails - if one fails and move direction that direction stop moving

        //if (!_interactable)
        //    return;


        float xPos = transform.position.x;
        if(moveLeft == false && xPos < _lockedXPos)
        {
            xPos = _lockedXPos;
        }
        else if(moveRight == false && xPos > _lockedXPos)
        {
            xPos = _lockedXPos;
        }

        transform.position = new Vector3(xPos, _startPos.y, 0);

        //if (Physics2D.Linecast(new Vector2(_railCheckLeft.position.x, transform.position.y), _railCheckLeft.position, 1 << LayerMask.NameToLayer("Rails")))
        //{
        //    moveLeft = true;
        //}
        //else
        //{
        //    moveLeft = false;
        //}
        //if (Physics2D.Linecast(new Vector2(_railCheckRight.position.x, transform.position.y),_railCheckRight.position, 1 << LayerMask.NameToLayer("Rails")))
        //{
        //    moveRight = true;
        //}
        //else
        //{
        //    moveRight = false;
        //}

        //if (Physics2D.Linecast(transform.position, _railCheckLeft.position, 1 << LayerMask.NameToLayer("Rails")))
        //{
        //    moveLeft = true;
        //}
        //else
        //{
        //    moveLeft = false;
        //}
        //if (Physics2D.Linecast(transform.position, _railCheckRight.position, 1 << LayerMask.NameToLayer("Rails")))
        //{
        //    moveRight = true;
        //}
        //else
        //{
        //    moveRight = false;
        //}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        //show button to use
        _buttonUI.SetActive(true);

        _interactable = true;

        collision.gameObject.GetComponent<PushAndPull>().SetMoveable(this, true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (transform.parent != null && transform.parent.CompareTag("Player"))
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        //Debug.Log("player leave");

        //stop show button to use
        _buttonUI.SetActive(false);

        _interactable = false;

        collision.gameObject.GetComponent<PushAndPull>().SetMoveable(this, false);
    }
}
