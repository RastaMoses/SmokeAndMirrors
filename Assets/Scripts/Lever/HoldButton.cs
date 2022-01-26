using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    [SerializeField]  float _pressTime;
    float _currentTime = 0;
    [SerializeField]  private Image _progressImage;

    private bool _takesInput = true;
    private PlayerController _playerController;

    private void Awake()
    {
        _takesInput = true;
        _progressImage.fillAmount = 0;
        _currentTime = 0;
        _playerController = FindObjectOfType<PlayerController>();
    }

    public bool CheckInput(string buttonName)
    {
        if (Input.GetButton(buttonName) && _takesInput)
        {
            //check if player can interact with this at the moment
            if (!_playerController.SetInteractionWith(this.gameObject))
                return false;

            _currentTime += Time.deltaTime;

            //update UI
            float progress = _currentTime / _pressTime;
            _progressImage.fillAmount = progress;

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
            _playerController.StopInteractionWith(this.gameObject);

            //input canceled
            _currentTime = 0;
            _progressImage.fillAmount = 0;
            _takesInput = true;
        }
        return false;
    }

}
