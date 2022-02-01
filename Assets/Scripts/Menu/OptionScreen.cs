using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreen : MonoBehaviour
{
    [SerializeField] private GameObject _optionScreen;
    [SerializeField] private GameObject _optionTriggerZone;
    [SerializeField] private CustomSlider[] vols;
    CustomSlider selectedSlider;
    int selection;


    // Start is called before the first frame update
    void Start()
    {
        // _optionScreen.SetActive(false);
        selectedSlider = vols[0];
    }

    // Update is called once per frame
    void Update()
    {
        selection += -(int)Input.GetAxisRaw("Vertical");
        selection = Mathf.Clamp(selection, 0, 1);
        selectedSlider = vols[selection];
        if (selectedSlider.f >= 0 || selectedSlider.f <= 1) selectedSlider.f += Input.GetAxis("Right Stick X") / 4;
        selectedSlider.f = Mathf.Clamp(selectedSlider.f, 0, 1);
        
        foreach (CustomSlider c in vols)
        {
            if (c == selectedSlider) c.I.sprite = c.i[1];
            else c.I.sprite = c.i[0];
        }
    }

    public void Close()
    {
        StateManager.Pause();
        _optionTriggerZone.SetActive(true);
        _optionScreen.SetActive(false);
    }
}
