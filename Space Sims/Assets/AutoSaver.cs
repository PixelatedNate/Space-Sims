using UnityEngine;

public class AutoSaver : MonoBehaviour
{

    private void OnApplicationQuit()
    {
        SaveSystem.SaveAll();
    }

}
