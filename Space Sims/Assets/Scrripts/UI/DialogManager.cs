using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public GameObject[] DialogChat;

    void Awake()
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
  
    public void StartDiaglogIndex(int i)
    {
        if (i == 0)
        {
            ButtonManager.Instance.SetAllButtons(false);
        }
        DialogChat[i].SetActive(true);
    }

}
