using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedHoldButton : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    private Sprite _buttonImageNoInteraction;
    [SerializeField] private Sprite[] animationSprites;

    [SerializeField] float _pressTime;
    float _currentTime = 0;

    private bool _takesInput = true;
    private bool _processingInputFeedback = false;

    private void Awake()
    {
        _takesInput = true;
        _buttonImageNoInteraction = _buttonImage.sprite;
        _currentTime = 0;
    }

    public bool CheckInput(string buttonName)
    {
        if (Input.GetButton(buttonName) && _takesInput)
        {
            _currentTime += Time.deltaTime;

            //update UI
            if (_currentTime > 0)
            {
                float progress = _currentTime / _pressTime;
                float animationProgress = progress * animationSprites.Length;
                int animationSpriteIndex = Mathf.Max(0, Mathf.FloorToInt(animationProgress) - 1);
                _buttonImage.sprite = animationSprites[animationSpriteIndex];
            }

            if (_currentTime >= _pressTime)
            {
                _takesInput = false;
                _buttonImage.sprite = animationSprites[animationSprites.Length - 1];
                StartCoroutine(ReceivedCompleteInput());
                //input completed
                return true;
            }
        }
        if (Input.GetButtonUp(buttonName))
        {
            //input canceled
            Reset();
        }
        return false;
    }

    IEnumerator ReceivedCompleteInput()
    {
        //prolong the visual feedback of input completion
        _processingInputFeedback = true;
        yield return new WaitForSecondsRealtime(0.35f);
        _buttonImage.sprite = _buttonImageNoInteraction;
        yield return new WaitForSecondsRealtime(0.3f);
        _processingInputFeedback = false;
    }

    public void Activate(string buttonName)
    {
        this.gameObject.SetActive(true);
        //if the button was deactivated only shortly for visual feedback, it needs to check if it needs to reset because interaction stopped
        if (!Input.GetButton(buttonName))
            Reset();
    }

    public void Deactivate()
    {
        if(gameObject.activeSelf) StartCoroutine(DeactivateAfterInputFeedback());
    }

    private IEnumerator DeactivateAfterInputFeedback()
    {
        while (_processingInputFeedback)
        {
            yield return null;
        }
       // Reset();
        this.gameObject.SetActive(false);
    }

    private void Reset()
    {
        if (_processingInputFeedback)
            return;
        //input canceled
        _currentTime = 0;
        _buttonImage.sprite = _buttonImageNoInteraction;
        _takesInput = true;
    }
}
