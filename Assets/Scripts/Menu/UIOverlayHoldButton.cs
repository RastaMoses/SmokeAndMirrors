using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOverlayHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string _buttonKey;

    [SerializeField] private float _pressTime;
    private float _currentTime = 0;

    [SerializeField] private bool IgnoresGamePause;
    public bool Interactable = true;

    [SerializeField] private UnityEvent _onButtonInput;

    [SerializeField] private Image _progressImage;

    private bool _takesInput = true;
    private bool _mousePress = false;

    private bool _processingInputFeedback = false;

    private void Awake()
    {
        _takesInput = true;
        _progressImage.fillAmount = 0;
        _currentTime = 0;
    }

    private void OnEnable()
    {
        Reset();
    }

    void Update()
    {
        if (!Interactable)
            return;

        if (CheckInput(_buttonKey))
        {
            if (_onButtonInput != null)
                StartCoroutine(ReceivedCompleteInput());
        }
    }

    IEnumerator ReceivedCompleteInput()
    {
        //prolong the visual feedback of input completion before actually invoking the function the button triggers
        _processingInputFeedback = true;
        yield return new WaitForSecondsRealtime(0.1f);
        _progressImage.fillAmount = 0;
        yield return new WaitForSecondsRealtime(0.1f);
        _processingInputFeedback = false;
        _onButtonInput.Invoke();
    }

    private bool CheckInput(string buttonName)
    {
        if ((_mousePress || Input.GetButton(buttonName)) && _takesInput)
        {
            return ProgressInput();
        }
        if (Input.GetButtonUp(buttonName))
        {
            Reset();
        }
        return false;
    }

    private bool ProgressInput()
    {
        _currentTime += IgnoresGamePause ? Time.unscaledDeltaTime : Time.deltaTime;

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
        return false;
    }

    private void Reset()
    {
        if (_processingInputFeedback)
            return;
        //input canceled
        _currentTime = 0;
        _progressImage.fillAmount = 0;
        _takesInput = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            _mousePress = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _mousePress = false;
            Reset();
        }
    }
}
