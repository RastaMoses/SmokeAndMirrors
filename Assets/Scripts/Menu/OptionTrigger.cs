using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionTrigger : MonoBehaviour
{
    public GameObject Spotlight;
    public HoldButton OptionButton;

    private bool _interactable;
    private string _gamePadbuttonKey = "A";

    // Start is called before the first frame update
    void Start()
    {
        Spotlight.SetActive(false);
        OptionButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_interactable)
        {
            if (OptionButton.CheckInput(_gamePadbuttonKey))
            {
                //trigger option
                //make lambda function here?
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Spotlight.SetActive(true);
        OptionButton.gameObject.SetActive(true);
        _interactable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Spotlight.SetActive(false);
        OptionButton.gameObject.SetActive(false);
        _interactable = false;
    }
}
