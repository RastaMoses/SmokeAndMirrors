using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIOverlayHoldButton : MonoBehaviour
{
    [SerializeField] private bool IgnoresGamePause;

    [SerializeField] private string _buttonKey;

    [SerializeField] private float _pressTime;
    private float _currentTime = 0;

    [SerializeField] private UnityEvent _onButtonInput;

    [SerializeField] private Image _progressImage;

    private bool _takesInput = true;

    public bool Interactable = true;

    private void Awake()
    {
        _takesInput = true;
        _progressImage.fillAmount = 0;
        _currentTime = 0;
    }

    void Update()
    {
        if (!Interactable)
            return;

        if (CheckInput(_buttonKey))
        {
            if (_onButtonInput != null)
                _onButtonInput.Invoke();
        }
    }

    private bool CheckInput(string buttonName)
    {
        if (Input.GetButton(buttonName) && _takesInput)
        {
            _currentTime += IgnoresGamePause? Time.unscaledDeltaTime : Time.deltaTime;

            //update UI
            float progress = _currentTime / _pressTime;
            _progressImage.fillAmount = progress;

            if (_currentTime >= _pressTime)
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
