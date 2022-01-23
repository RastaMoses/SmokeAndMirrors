using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableRoundLamp : SelectableObj
{
    private L _light;
    private string _swapButton = "A";
    private string _cancelButton = "B";

    Vector2 _rightStick = Vector2.zero;
    Vector2 _vectorSensibility = new Vector2(0.02f, 0.02f);
    //public float rotationSpeed; // only needed if rotating and not setting rotation directly


    protected override void InitializeOnStart()
    {
        _light = GetComponent<L>();
    }

    public override void ProcessInput()
    {
        if (Input.GetButtonDown(_swapButton))
        {
            _light.AddToSwap();
        }

        float ry = Input.GetAxis("Right Stick Y");
        float rx = Input.GetAxis("Right Stick X");
        _rightStick = new Vector2(rx, -ry);
        //Rotate selected obj on input from right stick
        if (!SelectableObjController.VectorAproxEqual(Vector2.zero, _rightStick, _vectorSensibility))
        {
            Vector3 target = new Vector3(_rightStick.x, _rightStick.y, 0);
            //alt: rotation with movement towards direction
            //Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, target);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            //alt: direct rotation without rotation movement
            transform.rotation = Quaternion.FromToRotation(Vector3.up, target);

        }
    }

    void Update()
    {
        if (Input.GetButtonDown(_cancelButton) || Input.GetMouseButtonDown(1))
        {
            _light.CancelSwap();
        }
    }

    void OnMouseDown()
    {
        _light.AddToSwap();
    }

}
