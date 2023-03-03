using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class planetView : MonoBehaviour
{
    PlanetContainer SelectedPlanet;

    [SerializeField]
    Image PlanetImg;

    [SerializeField]
    Button TravelButton;

    [SerializeField]
    TextMeshProUGUI Name, TravalTime, NumberOfQuest, QuestRestTime;

    public void SetPlanet(PlanetContainer planet)
    {
        SelectedPlanet = planet;
        UpdateText();
        UpdateImage();

        if (NavigationManager.CurrentPlanet == planet)
        {
            TravelButton.enabled = false;
        }
        else
        {
            TravelButton.enabled = true;
        }

    }

    public void TravelToPlanet()
    {
        if(NavigationManager.CurrentPlanet == SelectedPlanet)
        {
            return;
        }
        else if (QuestManager.GetQuestsByStaus(QuestStatus.InProgress).Count != 0)
        {
            UIManager.Instance.Conformation(CancalQuestAndTravelToPlanet, "This will cancal all you're inprogress quest");
        }
        else
        {
            NavigationManager.NavigateToTargetPlanet(SelectedPlanet);
        }
    }

    public void CancalQuestAndTravelToPlanet()
    {
        AbstractQuest[] questsInProgress = QuestManager.GetQuestsByStaus(QuestStatus.InProgress).ToArray();
        foreach(AbstractQuest quest in questsInProgress)
        {
            quest.CancalQuest();
        }
        NavigationManager.NavigateToTargetPlanet(SelectedPlanet);
    }


    private void UpdateImage()
    {
        PlanetImg.sprite = SelectedPlanet.planetData.PlanetSprite;
    }

    private void UpdateText()
    {
        Name.text = SelectedPlanet.planetData.PlanetName;
        NumberOfQuest.text = SelectedPlanet.HasPlayerVisted ? SelectedPlanet.getAvalibleQuests().Length.ToString() : "?";
        QuestRestTime.text = SelectedPlanet.NewQuestIn.RemainingDuration.ToString("h'h 'm'm'");
        //NumberOfQuest.text = SelectedPlanet. Quests.Length.ToString();
        if (NavigationManager.InNavigation)
        {
            TravalTime.text = "Unable To calcualte whilst in transit";
        }
        else
        {
            TravalTime.text = NavigationManager.CalcualteTravleTime(SelectedPlanet).ToString("h'h 'm'm 's's'");
        }
    }

}
