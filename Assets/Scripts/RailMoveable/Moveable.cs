using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField] private GameObject _buttonUI;
    public GameObject _colliderObj;

    public Vector3 edgeWidhtOffset { get; private set; }
    public Vector3 leftEdge => transform.position - edgeWidhtOffset;
    public Vector3 rightEdge => transform.position + edgeWidhtOffset;
    private Vector3 _startPos;

    private bool _moving;
    public bool Moving { get { return _moving;  } set { _moving = value; _colliderObj.SetActive(_moving); }  }

    private bool _interactable;

    // Start is called before the first frame update
    void Start()
    {
        _colliderObj.SetActive(false);

        _buttonUI.SetActive(false);
        _startPos = transform.position;

        edgeWidhtOffset = -(transform.right * (transform.localScale.x / 2f) * -1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_interactable)
            return;

        transform.position = new Vector3(transform.position.x, _startPos.y, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        _interactable = true;

        //show button to use
        _buttonUI.SetActive(true);
        //activate collider that will collide with rail ends
        collision.gameObject.GetComponent<PushAndPull>().SetMoveable(this, true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (transform.parent != null && transform.parent.CompareTag("Player"))
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        _interactable = false;

        //stop show button to use
        _buttonUI.SetActive(false);
        //deactivate collider that will collide with rail ends
        collision.gameObject.GetComponent<PushAndPull>().SetMoveable(this, false);
    }
}
