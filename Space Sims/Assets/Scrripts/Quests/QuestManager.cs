using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
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


   public List<Quest> CompleatedQuest = new List<Quest>();

    [SerializeField]
    List<Quest> AvalibleQuest = new List<Quest>();

    // Start is called before the first frame update
    void Start()
    {

        //for testing
        /*
        AvalibleQuest[0].UnassginAllPeopople();
        AvalibleQuest[0].AssginPerson(GlobalStats.Instance.PlayersPeople[0]);
        AvalibleQuest[0].StartQuest();
        */
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
