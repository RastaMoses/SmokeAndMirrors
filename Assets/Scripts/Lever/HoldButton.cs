using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    [SerializeField]
    float _pressTime;
    float _currentTime = 0;
    private KeyCode _key;

    [SerializeField]
    private Image _progressImage;

    private bool _takesInput = true;

    private void Awake()
    {
        _takesInput = true;
        _progressImage.fillAmount = 0;
        _currentTime = 0;
    }

    public bool CheckInput(string buttonName)
    {
        if (Input.GetButton(buttonName) && _takesInput)
        {
            _currentTime += Time.deltaTime;

            //update UI
            float prct = _currentTime / _pressTime;
            _progressImage.fillAmount = prct;

            if(_currentTime >= _pressTime)
            {
                _takesInput = false;
                _progressImage.fillAmount = 1;
                //input completed
                return true;
            }
        }
        if (Input.GetButtonUp(buttonName))
        {
            //input canceled
            _currentTime = 0;
            _progressImage.fillAmount = 0;
            _takesInput = true;
        }
        return false;
    }

}
