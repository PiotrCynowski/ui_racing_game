using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    [SerializeField] Image _image01 = null;
    [SerializeField] Image _image02 = null;
    [SerializeField] Image _image03 = null;

    [SerializeField] Button _raceButton = null;
    [SerializeField] RaceView _raceView = null;

    void Start()
    {
        _raceButton.onClick.AddListener(RaceClick);
    }

    private void OnDestroy()
    {
        _raceButton.onClick.RemoveAllListeners();
    }

    private void RaceClick()
    {
        _raceView.Open(_image01.sprite, _image02.sprite, _image03.sprite);
    }
}
