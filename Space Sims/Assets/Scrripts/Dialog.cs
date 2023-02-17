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
    }

    // Start is called before the first frame update

    void Awake()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        TouchControls.EnableCameramovemntAndSelection(false);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetButton == null && Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == linesobj[index].lines)
            {
                NextLine();
            }
            else
            {
              //  StopAllCoroutines();
              //  textComponent.text = lines[index];
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
        TargetButton = linesobj[index].button;
        
        if(linesobj[index].AltbuttonName != "")
        {
            TargetButton = GameObject.Find(linesobj[index].AltbuttonName).GetComponent<Button>();
        }

        if(TargetButton != null)
        {
            TargetButton.onClick.AddListener(NextLine);
            TargetButton.GetComponent<Animator>().SetBool("Blink",true);
        }
    }
    void NextLine()
    {
        if (TargetButton != null)
        {
            TargetButton.onClick.RemoveListener(NextLine);
            TargetButton.GetComponent<Animator>().SetBool("Blink", false);
            TargetButton = null;
        }
        if (index < linesobj.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            TouchControls.EnableCameramovemntAndSelection(true);
            gameObject.SetActive(false);
        }
    }
}
