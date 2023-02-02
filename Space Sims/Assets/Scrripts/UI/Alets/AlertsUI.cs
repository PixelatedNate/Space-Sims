using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertsUI : MonoBehaviour
{
    [SerializeField]
    UIButton uIButton;

    [SerializeField]
    private Transform AlertsScrollPanal;
    private Dictionary<Alert, GameObject> AlertsInView = new Dictionary<Alert, GameObject>();
    [SerializeField]
    private GameObject AlertUIPrefab;
    [SerializeField]
    private GameObject PermentAlertUIPrefab;

    [SerializeField]
    private GameObject AlertImageUI;


    private bool IsOpen { get; set; }

    private int GoodAlertCount = 0, PermentAlertCount = 0;

    [SerializeField]
    private TextMeshProUGUI goodAlert, permentAlert;

    public void OpenAlerts()
    {
        if (!IsOpen)
        {
            UIManager.Instance.ClearLeftPanal();
            uIButton.AlertTabSlideOut();
            IsOpen = true;
        }
    }

    public void CloseAlerts()
    {
        if (IsOpen)
        {
            uIButton.AlertTabSlideIn();
            IsOpen = false;
        }
    }

    public void AddAlert(Alert alert)
    {
        if (AlertsInView.Count == 0)
        {
            ShowAlertPanal();
        }

        if (alert.prority == Alert.AlertPrority.Permanet)
        {
            PermentAlertCount++;
        }
        else
        {
            GoodAlertCount++;
        }
        GameObject alertGO = GameObject.Instantiate(AlertUIPrefab, AlertsScrollPanal);
        AlertComponet alertComponet = alertGO.GetComponent<AlertComponet>();
        alertComponet.SetAlert(alert);
        AlertsInView.Add(alert, alertGO);
        UpdateText();
    }

    public void UpdateAlert(Alert alert)
    {
        GameObject alertGO = AlertsInView[alert];
        alertGO.GetComponent<AlertComponet>().SetAlert(alert);
        UpdateText();

    }




    public void RemoveAlert(Alert alert)
    {
        if (alert.prority == Alert.AlertPrority.Permanet)
        {
            PermentAlertCount--;
        }
        else
        {
            GoodAlertCount--;
        }
        GameObject.Destroy(AlertsInView[alert]);
        AlertsInView.Remove(alert);
        if (AlertsInView.Count == 0)
        {
            HideAlertPanal();
        }
        UpdateText();
    }


    private void UpdateText()
    {
        goodAlert.text = GoodAlertCount.ToString();
        permentAlert.text = PermentAlertCount.ToString();
    }

    private void HideAlertPanal()
    {
        GetComponent<Image>().enabled = false;
        AlertImageUI.SetActive(false);
    }

    private void ShowAlertPanal()
    {
        GetComponent<Image>().enabled = true;
        AlertImageUI.SetActive(true);
    }

}
