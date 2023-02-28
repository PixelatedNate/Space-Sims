using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    public Lines[] linesobj;
    private Button TargetButton { get; set; }
    public UnityEvent EndOfDialogEvent;
  
    [Serializable]
    public class Lines
    {
        public UnityEvent StartEvent;
        public string lines;
        [Tooltip("Leave Blank to be on touch")]
        public Button button;
        [Tooltip("Ifbutton is not pressent yet in game")]
        public string AltbuttonName;
        public bool CloseMenuAfter = false;
        public bool ClearOnTouch = true;
        public UnityEvent EndOfTextEvent;

    }


    // Start is called before the first frame update

    void Awake()
    {
        DialogManager.Instance.activeDialog = this;
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
            if (textComponent.text == linesobj[index].lines && TargetButton == null && linesobj[index].ClearOnTouch)
            {
                NextLine();
            }
            else if (textComponent.text != linesobj[index].lines)
            {
                StopAllCoroutines();
                textComponent.text = linesobj[index].lines;
                setEndButtonOrEventTriggerEndOfLineEvent();
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
        linesobj[index].StartEvent?.Invoke();
        foreach (char c in linesobj[index].lines.ToCharArray())
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.CatChat);
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        setEndButtonOrEventTriggerEndOfLineEvent();
    }

    public void setEndButtonOrEventTriggerEndOfLineEvent()
    {
        linesobj[index].EndOfTextEvent?.Invoke();
        
        if(TargetButton == null && linesobj[index].AltbuttonName != "") // due to changes in obejcts getting destroyed and reordered in menus.
        {
            TargetButton = GameObject.Find(linesobj[index].AltbuttonName).GetComponent<Button>();
        }

       
        if (TargetButton != null)
        {
            TargetButton.interactable = true;

            TargetButton.onClick.AddListener(NextLine);
            if (TargetButton.GetComponent<Animator>() == null)
            {
                TargetButton.GetComponentInParent<Animator>()?.SetBool("Blink", true);
            }
            else
            {
                TargetButton.GetComponent<Animator>()?.SetBool("Blink", true);
            }
        }
    }

    public bool HasAnotherLine()
    {
        return (index < linesobj.Length - 1);
    }




    public void NextLine()
    {
        if (linesobj[index].CloseMenuAfter)
        {
            UIManager.Instance.DeselectAll();
        }

        if (TargetButton != null)
        {
            TargetButton.onClick.RemoveListener(NextLine);
            TargetButton.interactable = false;
            if (TargetButton.GetComponent<Animator>() == null)
            {
                TargetButton.GetComponentInParent<Animator>()?.SetBool("Blink", false);
            }
            else
            {
                TargetButton.GetComponent<Animator>()?.SetBool("Blink", false);
            }
            TargetButton = null;
        }
        if (index < linesobj.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            TargetButton = linesobj[index].button;

            if (linesobj[index].AltbuttonName != "")
            {
                TargetButton = GameObject.Find(linesobj[index].AltbuttonName).GetComponent<Button>();
            }

            if (TargetButton != null)
            {
                TargetButton.interactable = false;
            }

            StartCoroutine(TypeLine());

        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {

        DialogManager.Instance.activeDialog = null;
        ButtonManager.Instance.SetAllButtons(true);
        ButtonManager.Instance.SetButtonEnabled(ButtonManager.ButtonName.Navigation, false);
        TouchControls.EnableCameramovemntAndSelection(true);
        EndOfDialogEvent?.Invoke();
        gameObject.SetActive(false);

    }

}
