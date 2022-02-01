using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Condition
{
    private PlayerController _player;
    private Game _game;

    [SerializeField] private UIAnimatedHoldButton _button;
    [SerializeField] private GameObject _spotLight;
    [SerializeField] private float activationTime = 0.3f;

    private bool _playerInside = false;
    bool playerStays;
    float playerStaysTime;

    // Start is called before the first frame update
    void Start()
    {
        playerStaysTime = 0f;
        _player = FindObjectOfType<PlayerController>();
        _game = FindObjectOfType<Game>();
        ActivateUI(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInside && !playerStays)
        {
            playerStaysTime += Time.deltaTime;
            if(playerStaysTime >= activationTime)
            {
                playerStays = true;
                if (fullfilled)
                    ActivateUI(true);
            }
            
        }
        else if (!_playerInside)
        {
            playerStaysTime = 0;
            playerStays = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        _playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        _playerInside = false;
        ActivateUI(false);
    }

    public void CompleteLevel()
    {
        ActivateUI(false);
        _player.movementEnabled = false;
        _game.LevelComplete();
    }

    protected override void OnFullfilled()
    {
        if(_playerInside)
            ActivateUI(true);
    }

    protected override void OnUnfullfilled()
    {
        ActivateUI(false);
    }

    private void ActivateUI(bool activate)
    {
        _button.gameObject.SetActive(activate);
        _spotLight.SetActive(activate);
    }

    protected override void InitializeValues(int target = 1, int start = 0)
    {
        target = 0;
        base.InitializeValues(target, start);
    }

    
}
