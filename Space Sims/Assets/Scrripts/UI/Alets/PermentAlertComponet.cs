using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PermentAlertComponet : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Name;
    public Alert ActiveAlert;
    public Image alertIcon;

    public void SetAlert(Alert alert)
    {
        ActiveAlert = alert;
        UpdateValues();
    }

    private void UpdateValues()
    {
        alertIcon.sprite = ActiveAlert.AlertIcon;
        Name.text = ActiveAlert.name;
    }




}
