using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransprotQuestViewComponet : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI targetLocation, transprotNumber;
    [SerializeField]
    Image TransportIconImage, PlanetSpriteImage;





    public void SelectQuest(TransportQuest transportQuest)
    {

        targetLocation.text = transportQuest.transportQuestData.TargetPlanetData.PlanetName;
        if (transportQuest.transportQuestData.TransaportPeople.Length != 0)
        {
            TransportIconImage.gameObject.SetActive(true);
            TransportIconImage.sprite = Icons.GetMiscUIIcon(UIIcons.Person);
        }
        else
        {
            TransportIconImage.gameObject.SetActive(false);
        }
        PlanetSpriteImage.sprite = transportQuest.transportQuestData.TargetPlanetData.PlanetSprite;
        if (transportQuest.transportQuestData.TransaportPeople.Length != 0)
        {
            transprotNumber.text = "X" + transportQuest.transportQuestData.TransaportPeople.Length.ToString();
        }
        else
        {
            transprotNumber.text = "";
        }

    }



}
