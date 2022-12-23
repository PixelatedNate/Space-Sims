using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public enum ButtonName
    {
        Navigation,
    }
 

    [Serializable]
    public class ButtonsReffrence
    {
        [SerializeField]
        public ButtonName name;
        [SerializeField]
        public Button button;
    }

    [SerializeField]
    ButtonsReffrence[] buttons;

    public static ButtonManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void SetButtonEnabled(ButtonName name, bool enabled)
    {
        foreach(ButtonsReffrence buttonReffrence in buttons)
        {
            if(buttonReffrence.name == name)
            {
                Color buttonColour = buttonReffrence.button.GetComponent<Button>().image.color;
                buttonReffrence.button.GetComponent<Button>().interactable = enabled;
            }
        }
    }

}
