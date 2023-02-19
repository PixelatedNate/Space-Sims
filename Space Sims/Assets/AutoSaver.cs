using UnityEngine;

public class AutoSaver : MonoBehaviour
 {
    bool reset = false;

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
        if (pause && !reset)
        {
            SaveSystem.SaveAll();
            Application.Quit();
        }
    }


    public void HardReset()
    {
        SaveSystem.ClearSave();
        Application.Quit();
    }

}
