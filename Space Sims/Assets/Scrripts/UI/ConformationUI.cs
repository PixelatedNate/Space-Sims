using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConformationUI : MonoBehaviour
{
    public Button _accept, _decline;
    public TextMeshProUGUI _text;

    public void setText(string text)
    {
        this._text.text = text;
    }

    public void SetListeners(UnityAction onAccept, UnityAction onDecline)
    {
        TouchControls.EnableCameramovemntAndSelection(false);
        _accept.onClick.RemoveAllListeners();
        _decline.onClick.RemoveAllListeners();
       
        _accept.onClick.AddListener(onAccept);
        _accept.onClick.AddListener(OnAccept);
        if (onDecline != null)
        {
            _decline.onClick.AddListener(onDecline);
        }
        _decline.onClick.AddListener(OnDecline);
    }

    private void OnDecline()
    {
        TouchControls.EnableCameramovemntAndSelection(true);
        this.gameObject.SetActive(false);
    }

    private void OnAccept()
    { 
        TouchControls.EnableCameramovemntAndSelection(true);
        this.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
