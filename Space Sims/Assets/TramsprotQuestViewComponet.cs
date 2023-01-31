using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TramsprotQuestViewComponet : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI targetLocation, transprotNumber;
    [SerializeField]
    Image TransportIconImage;





    public void SelectQuest(TransportQuest transportQuest)
    {

        targetLocation.text = transportQuest.TargetPlanetName;
        if (transportQuest.TransaportPeople.Length != 0)
        {
            TransportIconImage.sprite = Icons.GetMiscUIIcon(UIIcons.Person);
        }
        transprotNumber.text = "X" + transportQuest.TransaportPeople.Length.ToString();

    }



}
