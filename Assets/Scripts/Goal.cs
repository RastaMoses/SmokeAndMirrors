using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Condition
{
    private PlayerController _player;
    private Game _game;

    [SerializeField] private UIAnimatedHoldButton _button;
    [SerializeField] private GameObject _spotLight;

    private bool _playerInside = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _game = FindObjectOfType<Game>();
        _button.gameObject.SetActive(false);
        _spotLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        _playerInside = true;

        if (!fullfilled)
            return;

        ActivateUI(true);
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
