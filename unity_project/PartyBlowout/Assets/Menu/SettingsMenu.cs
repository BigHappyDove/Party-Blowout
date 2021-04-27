using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        if (_slider == null)
            throw new ArgumentException();
    }

    private void Update() => AudioListener.volume = _slider.value;
}
