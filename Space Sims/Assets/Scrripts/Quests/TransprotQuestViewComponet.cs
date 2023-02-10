using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransprotQuestViewComponet : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI targetLocation, transprotNumber;
    [SerializeField]
    Image TransportIconImage;





    public void SelectQuest(TransportQuest transportQuest)
    {

        targetLocation.text = transportQuest.transportQuestData.TargetPlanetData.PlanetName;
        if (transportQuest.transportQuestData.TransaportPeople.Length != 0)
        {
            TransportIconImage.sprite = Icons.GetMiscUIIcon(UIIcons.Person);
        }
        transprotNumber.text = "X" + transportQuest.transportQuestData.TransaportPeople.Length.ToString();

    }



}
