using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField] private GameObject _buttonUI;
    [SerializeField] private Transform _railCheckLeft;
    [SerializeField] private Transform _railCheckRight;

    //public bool moveLeft { get; private set; }
    //public bool moveRight { get; private set; }

    public bool moveLeft;
    public bool moveRight;

    private bool _interactable = false;

    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _buttonUI.SetActive(false);
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //find out if player left or right and which direction input to find out if pull or push
        //ground check on left and right for rails - if one fails and move direction that direction stop moving

        //if (!_interactable)
        //    return;

        transform.position = new Vector3(transform.position.x, _startPos.y, 0);

        if (Physics2D.Linecast(new Vector2(_railCheckLeft.position.x, transform.position.y), _railCheckLeft.position, 1 << LayerMask.NameToLayer("Rails")))
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
        if (Physics2D.Linecast(new Vector2(_railCheckRight.position.x, transform.position.y),_railCheckRight.position, 1 << LayerMask.NameToLayer("Rails")))
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        //show button to use
        _buttonUI.SetActive(true);

        _interactable = true;

        collision.gameObject.GetComponent<PushAndPull>().SetMoveable(this, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (transform.parent != null && transform.parent.CompareTag("Player"))
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        Debug.Log("player leave");

        //stop show button to use
        _buttonUI.SetActive(false);

        _interactable = false;

        collision.gameObject.GetComponent<PushAndPull>().SetMoveable(this, false);
    }
}
