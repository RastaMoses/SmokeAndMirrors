using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyText : MonoBehaviour
{
    [SerializeField] private ShinyParent _shiny;
    [SerializeField] private GameObject _shinyText;
    private bool _activated = false;


    // Start is called before the first frame update
    void Start()
    {
        _shinyText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_activated != _shiny.activated)
        {
            _shinyText.SetActive(_shiny.activated);

            _activated = _shiny.activated;
        }

    }
}
