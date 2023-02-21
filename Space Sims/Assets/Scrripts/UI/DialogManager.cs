using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public GameObject[] DialogChat;

    public Dialog activeDialog = null;

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

    public void WaitForPersonInFuelRoom()
    {
        TouchControls.EnableCameramovemntAndSelection(true);
        AbstractRoom.OnPersonAssgined += PeronInRoom;
    }

    public void PeronInRoom(AbstractRoom abstractRoom, Person person)
    {
         if(abstractRoom.RoomType == RoomType.Fuel)
         {
            activeDialog.EndDialog();
            AbstractRoom.OnPersonAssgined -= PeronInRoom;
         }
         else if(activeDialog.HasAnotherLine())
         {
             activeDialog.NextLine();
         }
    }

}
