using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Instance;
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

    public void SendAlert(Alerts.Alert alert)
    {
        //Debug.Log(alert.message);
    }

}
