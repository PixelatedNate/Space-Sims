using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    public Lines[] linesobj;
    private Button TargetButton;

    [Serializable]
    public class Lines
    {
        public string lines;
        [Tooltip("Leave Blank to be on touch")]
        public Button button;
        [Tooltip("Ifbutton is not pressent yet in game")]
        public string AltbuttonName;
        public bool CloseMenuAfter = false;
        public CustomEventTriggers.EventName eventEndTrigger = CustomEventTriggers.EventName.None;

    }

    // Start is called before the first frame update

    void Awake()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        TouchControls.EnableCameramovemntAndSelection(false);
        UIManager.Instance.DeselectAll();
        TouchControls.RecenterCamera();
        RoomGridManager.Instance.SetBuildMode(false);
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == linesobj[index].lines && TargetButton == null && linesobj[index].eventEndTrigger == CustomEventTriggers.EventName.None)
            {
                NextLine();
            }
            else if(textComponent.text != linesobj[index].lines)
            {
                StopAllCoroutines();
                textComponent.text = linesobj[index].lines;
                setEndButtonOrEventTrigger();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
          foreach (char c in linesobj[index].lines.ToCharArray())
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.CatChat);
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        setEndButtonOrEventTrigger();
       
    }


    public void setEndButtonOrEventTrigger()
    {
        if(TargetButton != null)
        {
            TargetButton.interactable = true;

            TargetButton.onClick.AddListener(NextLine);
            if (TargetButton.GetComponent<Animator>() == null)
            {
                TargetButton.GetComponentInParent<Animator>().SetBool("Blink", true);
            }
            else
            {
                TargetButton.GetComponent<Animator>().SetBool("Blink", true);
            }
        }
        else if(linesobj[index].eventEndTrigger != CustomEventTriggers.EventName.None)
        {
            TouchControls.EnableCameramovemntAndSelection(true);
            CustomEventTriggers.GetEvent(linesobj[index].eventEndTrigger).onEventDelaget += NextLine;

        }

    }


    void NextLine(object soruce)
    {
        TouchControls.EnableCameramovemntAndSelection(false);
        NextLine();
    }

    void NextLine()
    {
        if(linesobj[index].CloseMenuAfter)
        {
            UIManager.Instance.DeselectAll();
        }

        if (TargetButton != null)
        {
            TargetButton.onClick.RemoveListener(NextLine);
            TargetButton.interactable = false;
            if (TargetButton.GetComponent<Animator>() == null)
            {
                TargetButton.GetComponentInParent<Animator>().SetBool("Blink", false);
            }
            else
            {
                TargetButton.GetComponent<Animator>().SetBool("Blink", false);
            }
            TargetButton = null;
        }
        if (index < linesobj.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            TargetButton = linesobj[index].button;

            if(linesobj[index].AltbuttonName != "")
           {
               TargetButton = GameObject.Find(linesobj[index].AltbuttonName).GetComponent<Button>();
           }
    
           if(TargetButton != null)
           {
               TargetButton.interactable = false;
           }

            StartCoroutine(TypeLine());

        }
        else
        {
            ButtonManager.Instance.SetAllButtons(true);
            ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.Navigation,false);
            TouchControls.EnableCameramovemntAndSelection(true);
            gameObject.SetActive(false);
        }
    }
}
