using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertOverLastTouch : MonoBehaviour
{


    public static AlertOverLastTouch Instance;

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

    private Vector3 LastTouchPosition = Vector3.zero;


    [SerializeField]
    GameObject PopUpTextPrefab;

    private Transform MainCanvers { get; set; }

    private void Start()
    {
        MainCanvers = GameObject.FindGameObjectWithTag("MainCanvas").transform;
    }

    public void PlayAlertOverLastTouch(string message, Color colour)
    {
        GameObject alert = GameObject.Instantiate(PopUpTextPrefab, LastTouchPosition, Quaternion.identity, MainCanvers);
        alert.GetComponent<TextMeshProUGUI>().text = message;
        alert.GetComponent<TextMeshProUGUI>().color = colour; 
    }





    private void Update()         
    {
        if(Input.GetMouseButton(0))
        {
            LastTouchPosition = Input.mousePosition;
        }
    }

}
