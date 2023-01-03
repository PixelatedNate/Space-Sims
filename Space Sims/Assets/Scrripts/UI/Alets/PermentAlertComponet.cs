using TMPro;
using UnityEngine;

public class PermentAlertComponet : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Name;
    public Alert ActiveAlert;

    public void SetAlert(Alert alert)
    {
        ActiveAlert = alert;
        UpdateValues();
    }

    private void UpdateValues()
    {
        Name.text = ActiveAlert.name;
    }




}
