using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectableObjController : MonoBehaviour
{
    private Camera _camera;
    private GameObject _player;

    private List<SelectableObj> _allSelectableObjs;
    private List<SelectableObj> _selectableObjs = new List<SelectableObj>();

    private bool _inSelectionMode = false;
    private SelectableObj _selectedObj;
    private Vector3 _selectableObjsCenter = Vector3.zero;

    Vector2 _rightStick = Vector2.zero;
    Vector2 _vectorSensibility = new Vector2(0.02f, 0.02f);
    //public float rotationSpeed; // only needed if rotating and not setting rotation directly


    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _player = GameObject.FindGameObjectWithTag("Player");
        var selectables = GameObject.FindObjectsOfType(typeof(SelectableObj)) as SelectableObj[];
        _allSelectableObjs = selectables.ToList();
    }


    // Update is called once per frame
    void Update()
    {
        //do not check for selecting objects if there are no selectables
        if (_allSelectableObjs.Count == 0)
            return;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (_inSelectionMode)
            {
                OnEndSelectMode();
            }
            else
            {
                OnStartSelectMode();
            }
        }

        if(Input.GetAxis("Left Trigger") != 0)
        {
            if (!_inSelectionMode)
            {
                OnStartSelectMode();
            }
        }
        else
        {
            if (_inSelectionMode)
            {
                OnEndSelectMode();
            }
        }

        if (_inSelectionMode)
        {
            //TODO cast "spellray" from player to selected obj
            Debug.DrawLine(_player.transform.position, _selectedObj.transform.position, Color.blue);

            float ry = Input.GetAxis("Right Stick Y");
            float rx = Input.GetAxis("Right Stick X");
            _rightStick = new Vector2(rx, -ry);
            //Rotate selected obj on input from right stick
            if (!VectorAproxEqual(Vector2.zero, _rightStick, _vectorSensibility))
            {
                Vector3 target = new Vector3(_rightStick.x, _rightStick.y, 0);
                //alt: rotation with movement towards direction
                //Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, target);
                //selectedObj.transform.rotation = Quaternion.RotateTowards(selectedObj.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                //alt: direct rotation without rotation movement
                _selectedObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, target);

            }

            if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Left Bumper"))
            {
                _selectedObj.DeSelect();
                //select obj direction counter clockwise
                int nextIndex = _selectableObjs.IndexOf(_selectedObj) - 1;
                if (nextIndex < 0)
                    nextIndex = _selectableObjs.Count - 1;
                _selectedObj = _selectableObjs[nextIndex];
                _selectedObj.Select();
            }
            else if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Right Bumper"))
            {
                _selectedObj.DeSelect();
                //select obj direction clockwise
                int nextIndex = _selectableObjs.IndexOf(_selectedObj) + 1;
                if (nextIndex >= _selectableObjs.Count)
                    nextIndex = 0;
                _selectedObj = _selectableObjs[nextIndex];
                _selectedObj.Select();
            }

            

        }


        //Movement disable on magic mode
        /*
        if (InSelectionMode)
        {
            FindObjectOfType<PlayerController>().movementEnabled = false;
        }
        else
        {

            FindObjectOfType<PlayerController>().movementEnabled = true;
        }
        */
    }


    private void OnStartSelectMode()
    {
        //Only visible objects are selectable
        _selectableObjs = _allSelectableObjs.Where(s => IsWithinBounds(new Rect(0, 0, 1, 1), _camera.WorldToViewportPoint(s.transform.position))).ToList();

        //do not enter selection mode if there are no objs that can currently be selected
        if (_selectableObjs.Count == 0)
            return;

        _inSelectionMode = true;

        //Animation
        FindObjectOfType<PlayerController>().gameObject.GetComponent<Animator>().SetTrigger("magicMode");


        //order selectables in a circle
        _selectableObjsCenter = CalculateCentroid(_selectableObjs);
        transform.position = _selectableObjsCenter;
        //player relative -> give Player.transform
        _selectableObjs = _selectableObjs.OrderBy(s => s.GetAngle(transform)).ToList();

        //find closest obj to player
        SelectableObj closestObj = null;
        foreach(SelectableObj selectableObj in _selectableObjs)
        {
            if(closestObj == null || (closestObj != null && Vector2.Distance(_player.transform.position, selectableObj.transform.position) < Vector2.Distance(_player.transform.position, closestObj.transform.position)))
            {
                closestObj = selectableObj;
            }
        }
        _selectedObj = closestObj;
        _selectedObj.Select();
    }

    private void OnEndSelectMode()
    {
        _inSelectionMode = false;
        _selectedObj.DeSelect();
        _selectedObj = null;
    }

    Vector3 CalculateCentroid(List<SelectableObj> Objs)
    {
        Vector3 centroid = new Vector3(0, 0, 0);

        if (Objs.Count > 0)
        {
            foreach(SelectableObj obj in Objs)
            {
                centroid += obj.transform.position;
            }
            centroid /= Objs.Count;
        }

        return centroid;
    }

    bool VectorAproxEqual(Vector2 vector1, Vector2 vector2, Vector2 sensibility)
    {
        if (vector1.x <= vector2.x + sensibility.x && vector1.y <= vector2.y + sensibility.y &&
            vector1.x >= vector2.x - sensibility.y && vector1.y >= vector2.y - sensibility.y)
            return true;
        return false;
    }

    bool IsWithinBounds(Rect boundingBox, Vector2 point)
    {
        if (point.x >= boundingBox.x && point.x <= boundingBox.x + boundingBox.width
            && point.y >= boundingBox.y && point.y <= boundingBox.y + boundingBox.height)
            return true;
        return false;
    }
}
