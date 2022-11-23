using System.Collections;
using System.Collections.Generic;
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
        if(ActiveAlert.prority != Alert.AlertPrority.Permanet)
        {
            button.onClick.AddListener(ClearAlert);
        }
        button.onClick.AddListener(ActiveAlert.OnClickEffect);
    }



    private void ClearAlert()
    {
        AlertManager.Instance.RemoveAlert(ActiveAlert);
    }




}
