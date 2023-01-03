using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Instance;

    List<Alert> Alerts = new List<Alert>();

    [SerializeField]
    private AlertsUI AlertsUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SendAlert(Alert alert)
    {
        if (Alerts.Count == 0)
        {
            AlertsUI.gameObject.SetActive(true);
        }
        Alerts.Add(alert);
        AlertsUI.AddAlert(alert);
    }

    public void RemoveAlert(Alert alert)
    {
        Alerts.Remove(alert);
        AlertsUI.RemoveAlert(alert);
    }
}
