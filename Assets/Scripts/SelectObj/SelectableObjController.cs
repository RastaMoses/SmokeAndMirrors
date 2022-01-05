using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectableObjController : MonoBehaviour
{
    public List<SelectableObj> OriginalS;
    public List<SelectableObj> SelectableObjs;
    public GameObject Player;
    public bool InSelectionMode = false;
    SelectableObj selectedObj;


    // Start is called before the first frame update
    void Start()
    {
        var x = GameObject.FindObjectsOfType(typeof(SelectableObj)) as SelectableObj[];
        OriginalS = x.ToList();
        SelectableObjs = OriginalS.ToList();
        OnStartSelectMode();
    }

    bool VectorAproxEqual(Vector2 vector1, Vector2 vector2, Vector2 sensibility)
    {
        if (vector1.x <= vector2.x + sensibility.x && vector1.y <= vector2.y + sensibility.y &&
            vector1.x >= vector2.x - sensibility.y && vector1.y >= vector2.y - sensibility.y)
            return true;
        return false;
    }

    float rightStickInputTime = 0f;
    float rightStickHoldTime = 1f;

    Vector2 rightStick = Vector2.zero;
    Vector2 vectorSensibility = new Vector2(0.02f, 0.02f);

    bool righStockHold => rightStickInputTime >= rightStickHoldTime;

    Vector2 lastRight = Vector2.zero;

    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (InSelectionMode)
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
            if (!InSelectionMode)
            {
                OnStartSelectMode();
            }
        }
        else
        {
            if (InSelectionMode)
            {
                OnEndSelectMode();
            }
        }

        float my = Input.GetAxis("Right Stick Y");
        float mx = Input.GetAxis("Right Stick X");
        rightStick = new Vector2(mx, -my);

        //Debug.Log("r s : " + rightStick.x + ", " + rightStick.y);
        float rightStickAngle = Mathf.Round(Mathf.Atan2(rightStick.y, rightStick.x) * Mathf.Rad2Deg);
        //Debug.Log("r angle: " + rightStickAngle);
        //Debug.Log("rSAprox 0? " + VectorAproxEqual(Vector2.zero, rightStick, vectorSensibility));


        if(Input.GetButtonDown("Right Bumper"))
        {
            Debug.Log("Right Bumper");
        }
        if (Input.GetButtonDown("Left Bumper"))
        {
            Debug.Log("Left Bumper");
        }
        if(Input.GetAxis("Left Trigger") != 0)
        {
            Debug.Log("Left Trigger: " + Input.GetAxis("Left Trigger"));
        }
        if (Input.GetAxis("Right Trigger") != 0)
        {
            Debug.Log("Right Trigger: " + Input.GetAxis("Right Trigger"));
        }

        bool wasRightStickPressed = rightStickInputTime > 0 && rightStickInputTime < rightStickHoldTime && VectorAproxEqual(Vector2.zero, rightStick, vectorSensibility);

        ////Debug.Log("WasRightStickpressed " + wasRightStickPressed);
        //if (wasRightStickPressed)
        //{
        //    Debug.Log("r s : " + rightStick.x + ", " + rightStick.y + " left: " + Vector2.left + " right: " + Vector2.right);
        //    Vector2 nR = rightStick.normalized;
        //    Debug.Log("N r s : " + rightStick.x + ", " + rightStick.y);
        //    float leftDot = Vector2.Dot(Vector2.left.normalized, rightStick.normalized);
        //    Debug.Log("leftDtot" + leftDot);
        //    Debug.Log("lastright " + lastRight);
        //    Debug.Break();
        //}
  

        //if (!VectorAproxEqual(Vector2.zero, rightStick, vectorSensibility))
        //{
        //    rightStickInputTime += Time.deltaTime;
        //}
        //else
        //{
        //    if(rightStickInputTime != 0)
        //        Debug.Log("rSHold " + righStockHold);

        //    rightStickInputTime = 0;
        //}

        //if (righStockHold)
        //{
        //    Debug.Log("rightSTInputTime" + rightStickInputTime);
        //    Debug.Log("rSHold " + righStockHold);
        //}


        lastRight = rightStick;
        if (InSelectionMode)
        {

            //float sensibility = 1;
            //foreach(SelectableObj selectableObj in SelectableObjs)
            //{

            //    if (selectableObj.Angle <= rightStickAngle + sensibility  && selectableObj.Angle >= rightStickAngle - sensibility ){
            //        selectedObj.DeSelect();
            //        selectedObj = selectableObj;
            //        selectedObj.Select();
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.X) || (wasRightStickPressed && VectorAproxEqual(Vector2.left, rightStick, vectorSensibility)))
            //{
            //    selectedObj.DeSelect();
            //    //back
            //    int nextIndex = SelectableObjs.IndexOf(selectedObj) - 1;
            //    if (nextIndex < 0)
            //        nextIndex = SelectableObjs.Count - 1;
            //    selectedObj = SelectableObjs[nextIndex];
            //    selectedObj.Select();
            //}
            //else if (Input.GetKeyDown(KeyCode.C) || (wasRightStickPressed && VectorAproxEqual(Vector2.right, rightStick, vectorSensibility)))
            //{
            //    //forth
            //    selectedObj.DeSelect();
            //    //back
            //    int nextIndex = SelectableObjs.IndexOf(selectedObj) + 1;
            //    if (nextIndex >= SelectableObjs.Count)
            //        nextIndex = 0;
            //    selectedObj = SelectableObjs[nextIndex];
            //    selectedObj.Select();
            //}
            if (!VectorAproxEqual(Vector2.zero, rightStick, vectorSensibility))
            {
                Vector3 target = new Vector3(rightStick.x, rightStick.y, 0);
                //alt: rotation with movement towards direction
                //Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, target);
                //selectedObj.transform.rotation = Quaternion.RotateTowards(selectedObj.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                //alt: direct rotation without rotation movement
                if (!VectorAproxEqual(Vector2.zero, rightStick, vectorSensibility))
                    selectedObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, target);

            }

            if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Left Bumper"))
            {
                selectedObj.DeSelect();
                //back
                int nextIndex = SelectableObjs.IndexOf(selectedObj) - 1;
                if (nextIndex < 0)
                    nextIndex = SelectableObjs.Count - 1;
                selectedObj = SelectableObjs[nextIndex];
                selectedObj.Select();
            }
            else if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Right Bumper"))
            {
                //forth
                selectedObj.DeSelect();
                //back
                int nextIndex = SelectableObjs.IndexOf(selectedObj) + 1;
                if (nextIndex >= SelectableObjs.Count)
                    nextIndex = 0;
                selectedObj = SelectableObjs[nextIndex];
                selectedObj.Select();
            }

        }
    }


    private void OnStartSelectMode()
    {
        InSelectionMode = true;

        ObjCenter = CalculateCentroid(SelectableObjs);
        transform.position = ObjCenter;
        SelectableObj closestObj = null;

        foreach(SelectableObj selectableObj in SelectableObjs)
        {
            //selectableObj.DotToPlayer = Vector2.Dot(Vector2.left.normalized, (selectableObj.transform.position - Player.transform.position).normalized);
            Vector2 dir = selectableObj.transform.position - Player.transform.position;
            selectableObj.DotToPlayer = Vector2.Dot(Vector2.left, (selectableObj.transform.position - Player.transform.position)); //The DotProduct inticates how similar the two vectors are (normalized -1 if saem direction, 1 if opposite)
                                                                                                                                   //selectedObj.Det = Vector2.left * dir - dir * Vector2.left;
                                                                                                                                   //selectableObj.Angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                                                                                                                                   //selectableObj.Angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            //selectableObj.Angle = Mathf.Round( Mathf.Atan2(selectableObj.transform.position.y, selectableObj.transform.position.x) * Mathf.Rad2Deg);

            //player relative
            //Vector3 relative = Player.transform.InverseTransformPoint(selectableObj.transform.position);
            //selectableObj.Angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

            Vector3 relative = transform.InverseTransformPoint(selectableObj.transform.position);
            selectableObj.Angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

            if(closestObj == null || (closestObj != null && Vector2.Distance(Player.transform.position, selectableObj.transform.position) < Vector2.Distance(Player.transform.position, closestObj.transform.position)))
            {
                closestObj = selectableObj;
            }

        }
        //SelectableObjs = SelectableObjs.OrderBy(s => s.DotToPlayer).ToList();
        SelectableObjs = SelectableObjs.OrderBy(s => s.Angle).ToList();
        selectedObj = closestObj;
        selectedObj.Select();
    }

    void OnEndSelectMode()
    {
        InSelectionMode = false;
        selectedObj.DeSelect();
        selectedObj = null;
    }

    Vector3 ObjCenter = Vector3.zero;

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
}
