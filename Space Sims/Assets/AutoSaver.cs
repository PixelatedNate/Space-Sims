using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaver : MonoBehaviour
{

    private void OnApplicationQuit()
    {
        SaveSystem.SaveAll();
    }

}
