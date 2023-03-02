using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipsAndToutorial : MonoBehaviour
{

    public static TipsAndToutorial Instance { get; set; }
    void Awake()
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


    [Serializable]
    public enum ToutorialMenus
    {
        TopBar,
        Alerts,
        QuestList,
        Quest,
        PeopleList,
        PersonView,
        DragingPersonToRoom,

    }

    [SerializeField]
    private ToutorialSegment[] toutorialSegments;

    private bool ToutorialEnabled { get; set; }

    [SerializeField]
    private GameObject ToutorialPanle;
    [SerializeField]
    private GameObject firstToutorialPanle;

    [SerializeField]
    private TextMeshProUGUI toutorialTitle;
    [SerializeField]
    private TextMeshProUGUI toutorialText;

    [SerializeField]
    private Material higlightMaterial;

    private int page;
    private ToutorialSegment activeToutorial;

    [SerializeField]
    private GameObject nextbutton;
    [SerializeField]
    private GameObject backbutton;



    private void Start()
    {
        foreach (ToutorialSegment tm in toutorialSegments)
        {
            tm.completed = false;
        }
    }





    public void SetTipsEnabled(bool enabled)
    {
        ToutorialEnabled = enabled;
        firstToutorialPanle.SetActive(false);
        if (enabled)
        {
            OpenTipMenu(ToutorialMenus.TopBar);
        }
    }



    public void OpenTipMenu(ToutorialMenus tipMenu)
    {
        return;
        if (!ToutorialEnabled)
        {
            return;
        }
        ToutorialSegment tip = GetToutortialByEnum(tipMenu);
        if (tip == null)
        {
            return;
        }
        if (tip.completed == true)
        {
            return;
        }
        TouchControls.EnableCameramovemntAndSelection(false);
        SetAllButtons(false);
        SetOutlineImage(tip.HighlightedUIElements);
        activeToutorial = tip;
        page = 0;
        backbutton.SetActive(false);
        if (page == activeToutorial.pages.Length - 1)
        {
            nextbutton.SetActive(false);
        }
        else
        {
            nextbutton.SetActive(true);
        }
        ToutorialPanle.SetActive(true);
        toutorialTitle.text = tip.pages[0].pagetitle;
        toutorialText.text = tip.pages[0].pageText;
    }


    public void CloseTipMenu()
    {
        ClearOutlineImage(activeToutorial.HighlightedUIElements);
        ClearOutlineImage(activeToutorial.pages[page].HighlightedUIElements);
        ToutorialPanle.SetActive(false);
        activeToutorial.completed = true;
        activeToutorial = null;
        SetAllButtons(true);
        TouchControls.EnableCameramovemntAndSelection(true);
    }

    public void NextTipMenuPage()
    {
        if (activeToutorial != null)
        {
            ClearOutlineImage(activeToutorial.pages[page].HighlightedUIElements);
            page++;
            backbutton.SetActive(true);
            if (page == activeToutorial.pages.Length - 1)
            {
                nextbutton.SetActive(false);
            }
            toutorialTitle.text = activeToutorial.pages[page].pagetitle;
            toutorialText.text = activeToutorial.pages[page].pageText;
            SetOutlineImage(activeToutorial.pages[page].HighlightedUIElements);
        }
    }
    public void PreviousTipMenuPage()
    {
        if (activeToutorial != null)
        {
            ClearOutlineImage(activeToutorial.pages[page].HighlightedUIElements);
            page--;
            nextbutton.SetActive(true);
            if (page == 0)
            {
                backbutton.SetActive(false);
            }
            toutorialTitle.text = activeToutorial.pages[page].pagetitle;
            toutorialText.text = activeToutorial.pages[page].pageText;
            SetOutlineImage(activeToutorial.pages[page].HighlightedUIElements);
        }
    }



    public void ClearOutlineImage(string[] elements)
    {
        foreach (string element in elements)
        {
            GameObject gm = GameObject.Find(element);
            gm.GetComponent<Image>().material = null;
        }
    }

    public void SetOutlineImage(string[] elements)
    {
        foreach (string element in elements)
        {
            GameObject gm = GameObject.Find(element);
            gm.GetComponent<Image>().material = higlightMaterial;
        }
    }


    private ToutorialSegment GetToutortialByEnum(ToutorialMenus tipMenu)
    {
        foreach (ToutorialSegment t in toutorialSegments)
        {
            if (t.tipMenu == tipMenu)
            {
                return t;
            }
        }
        return null;
    }


    public void SetAllButtons(bool enabled)
    {
        Button[] allbuttons = GameObject.FindObjectsOfType<Button>();
        foreach (Button btn in allbuttons)
        {
            btn.enabled = enabled;
        }
    }


}
