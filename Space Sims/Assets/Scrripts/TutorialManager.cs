using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField]
    public static bool completed = false;

    public enum TutorialEvent
    {
        ScreenPaned,
        ScreenZoomed,
    }


    public void EventTrigered(TutorialEvent tutorialEvent)
    {

    }

}
