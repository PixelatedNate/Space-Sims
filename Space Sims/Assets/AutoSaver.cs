using UnityEngine;

public class AutoSaver : MonoBehaviour
{

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        SaveSystem.SaveAll();
# endif
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveSystem.SaveAll();
        }
    }

}
