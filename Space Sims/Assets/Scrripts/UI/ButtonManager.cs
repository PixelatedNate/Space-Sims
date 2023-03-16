using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public enum ButtonName
    {
        Navigation,
        Quests,
        AvaliableQuest,
        InprogressQuest,
        CompletedQuest,
        Milestones,
        BuildRoom,
        peoplelist,
        Alets,
        CloseAll,
        PersonListFillterLeft,
        PersonListFillterRight,
        PersonListFillterInverse,
        PersonMagnifigyGlassIcon,
        TravelToBtn 
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

    private void Start()
    {
        QuestManager.OnQuestAdded += UpdateQuestImageToNewQuest;   
    }

    public void UpdateQuestImageToNewQuest()
    {
        Button questButton = getButton(ButtonName.Quests);
        questButton.transform.GetChild(0).GetComponent<Image>().sprite = Icons.GetMiscUIIcon(UIIcons.QuestAlert);
    }
    public void UpdateQuestImageToNormalQuest()
    {
        Button questButton = getButton(ButtonName.Quests);
        questButton.transform.GetChild(0).GetComponent<Image>().sprite = Icons.GetMiscUIIcon(UIIcons.QuestIcon);
    }



    public void SetAllButtons(bool enabled)
    {
        foreach (ButtonsReffrence buttonReffrence in buttons)
        {
            Color buttonColour = buttonReffrence.button.GetComponent<Button>().image.color;
            buttonReffrence.button.GetComponent<Button>().interactable = enabled;
        }
    }


    public void SetButtonEnabled(ButtonName name, bool enabled)
    {
        Button b = getButton(name);
        Color buttonColour = b.GetComponent<Button>().image.color;
        b.GetComponent<Button>().interactable = enabled;                  
    }


    private Button getButton(ButtonName name)
    {
        foreach (ButtonsReffrence buttonReffrence in buttons)
        {
            if (buttonReffrence.name == name)
            {
                return buttonReffrence.button;
                //Color buttonColour = buttonReffrence.button.GetComponent<Button>().image.color;
               // buttonReffrence.button.GetComponent<Button>().interactable = enabled;
            }
        }
        return null;
    }

}
