using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableRoundLamp : SelectableObj
{
    private NL _light;
    private string _swapButton = "A";
    private string _cancelButton = "B";

    Quaternion lastFrameRotation;
    bool playingSFX;
    [SerializeField] float sfxUpdateSpeed = 0.8f;

    Vector2 _rightStick = Vector2.zero;
    //Vector2 _vectorSensibility = new Vector2(0.02f, 0.02f);
    Vector2 _vectorSensibility = new Vector2(0.05f, 0.05f);
    [SerializeField] private float _rotationSpeed; // only needed if rotating and not setting rotation directly
    private float _currentRotationSpeed = 0f;
    [SerializeField] private float _acceleration;

    Vector3 target = Vector3.zero;

    protected override void InitializeOnStart()
    {
        _light = GetComponent<NL>();
        lastFrameRotation = transform.rotation;
        StartCoroutine(UpdateSFX());
        playingSFX = false;
    }

    public override void ProcessInput()
    {
        if (Input.GetButtonDown(_swapButton))
        {
            FindObjectOfType<NLC>().ATS(_light);
        }

        float ry = Input.GetAxis("Right Stick Y");
        float rx = Input.GetAxis("Right Stick X");
        _rightStick = new Vector2(rx, -ry);
        //Rotate selected obj on input from right stick
        if (!SelectableObjController.VectorAproxEqual(Vector2.zero, _rightStick, _vectorSensibility))
        {
            target = new Vector3(_rightStick.x, _rightStick.y, 0);
            //alt: rotation with movement towards direction
            //Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, target);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, target);
            if (isApproximate(transform.rotation, targetRotation, 0.005f))
            {
                //deacelarate when input rotation approx lamp rotation
                _currentRotationSpeed = Mathf.Max(_currentRotationSpeed - (_acceleration * Time.deltaTime), 0);
            }
            else if (isApproximate(transform.rotation, targetRotation, 0.1f) && _currentRotationSpeed > 90)
            {
                //when the lamp is turning and comes close to target rotation deaccalarete to max speed of 90
                _currentRotationSpeed = Mathf.Max(_currentRotationSpeed - (_acceleration * Time.deltaTime), 90);
            }
            else
            {
                _currentRotationSpeed = Mathf.Min(_rotationSpeed, _currentRotationSpeed + (_acceleration * Time.deltaTime));
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _currentRotationSpeed * Time.deltaTime);

            //alt: direct rotation without rotation movement
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, target);

        }
        else if(_currentRotationSpeed != 0 && target != Vector3.zero)
        {
            //Nachschwingen
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, target);
            _currentRotationSpeed = Mathf.Max(_currentRotationSpeed - (_acceleration* 1.5f * Time.deltaTime), 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _currentRotationSpeed * Time.deltaTime);
            if (isApproximate(transform.rotation, targetRotation, 0.005f))
            {
                target = Vector3.zero;
            }
        }
        else
        {
            _currentRotationSpeed = 0;
        }
    }
    

    public static bool isApproximate(Quaternion q1, Quaternion q2, float precision)
    {
        //ABS returns positive value, quaternion dot ranges from -1 to 1 where -1 and 1 arr both the same angle (0° and 360°)
        return Mathf.Abs(Quaternion.Dot(q1, q2)) >= 1 - precision;
    }


    void Update()
    {
        if (Input.GetButtonDown(_cancelButton) || Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<NLC>().CSW();
        }
        
    }

    IEnumerator UpdateSFX()
    {
        while (true)
        {
            yield return new WaitForSeconds(sfxUpdateSpeed);
            if (lastFrameRotation != transform.rotation && !playingSFX)
            {
                GetComponent<SFX>().PlayRotating();
                playingSFX = true;
            }
            if (lastFrameRotation == transform.rotation)
            {
                GetComponent<SFX>().PauseRotating();
                playingSFX = false;
            }

            lastFrameRotation = transform.rotation;
        }
        
    }

    void OnMouseDown()
    {
        FindObjectOfType<NLC>().ATS(_light);
    }

}
