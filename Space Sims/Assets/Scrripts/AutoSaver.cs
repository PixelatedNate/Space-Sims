using UnityEngine;

public class AutoSaver : MonoBehaviour
{
    
    public static bool reset = false;

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        if (!reset)
        {
            SaveSystem.SaveAll();
        }
# endif
    }

    private void OnApplicationPause(bool pause)
    {
#if !UNITY_EDITOR
        if (pause && !reset)
        {
            SaveSystem.SaveAll();
            Application.Quit();
        }
# endif
    }

    public void HardReset()
    {
        SaveSystem.ClearSave();
        Application.Quit();
    }

}
