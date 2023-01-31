using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertComponet : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Name, discripiton, Time;
    public Alert ActiveAlert;
    [SerializeField]
    private Button button;

    [SerializeField]
    Image AlertIconImg, iconBackGroundColourImage, textBackGroundColourImage;

    public void SetAlert(Alert alert)
    {
        ActiveAlert = alert;
        UpdateValues();
    }

    private void UpdateValues()
    {
        Name.text = ActiveAlert.name;
        discripiton.text = ActiveAlert.message;
        Time.text = ActiveAlert.time.ToString("HH:mm");
        AlertIconImg.sprite = ActiveAlert.AlertIcon;
        if (ActiveAlert.prority != Alert.AlertPrority.Permanet)
        {
            button.onClick.AddListener(ClearAlert);
        }
        if (ActiveAlert.prority == Alert.AlertPrority.High)
        {
            iconBackGroundColourImage.color = Color.red;
        }
        else if (ActiveAlert.prority == Alert.AlertPrority.low)
        {
            iconBackGroundColourImage.color = Color.green;
        }
        else if (ActiveAlert.prority == Alert.AlertPrority.Permanet)
        {
            textBackGroundColourImage.color = Color.yellow;
        }
        else
        {
            iconBackGroundColourImage.color = Color.green;
        }
        button.onClick.AddListener(ActiveAlert.OnClickEffect);
    }



    private void ClearAlert()
    {
        AlertManager.Instance.RemoveAlert(ActiveAlert);
    }




}
