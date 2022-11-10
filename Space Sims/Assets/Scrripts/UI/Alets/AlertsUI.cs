using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertsUI : MonoBehaviour
{
    [SerializeField]
    UIButton uIButton;

    [SerializeField]
    private Transform AlertsScrollPanal;
    private Dictionary<Alert,GameObject> AlertsInView = new Dictionary<Alert, GameObject>();
    [SerializeField]
    private GameObject AlertUIPrefab;
    [SerializeField]
    private GameObject PermentAlertUIPrefab;  
    public void OpenAlerts()
    {
        uIButton.AlertTabSlideOut();
    }

    public void closeAlerts()
    {
        uIButton.AlertTabSlideIn();
    }

    public void AddAlert(Alert alert)
    {
        if (alert.prority == Alert.AlertPrority.Permanet)
        {
            GameObject alertPermentGO = GameObject.Instantiate(PermentAlertUIPrefab, AlertsScrollPanal);
            PermentAlertComponet alertPermentComponet = alertPermentGO.GetComponent<PermentAlertComponet>();
            alertPermentComponet.SetAlert(alert);
            AlertsInView.Add(alert, alertPermentGO);
        }
        else
        {
            GameObject alertGO = GameObject.Instantiate(AlertUIPrefab, AlertsScrollPanal);
            AlertComponet alertComponet = alertGO.GetComponent<AlertComponet>();
            alertComponet.SetAlert(alert);
            AlertsInView.Add(alert, alertGO);
        }
    }
    public void RemoveAlert(Alert alert)
    {
        GameObject.Destroy(AlertsInView[alert]);
        AlertsInView.Remove(alert);
    }

}
