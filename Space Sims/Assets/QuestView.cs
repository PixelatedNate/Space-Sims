using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestView : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI Title;
    [SerializeField]
    TextMeshProUGUI Discription;

    private Quest questSelected;
    // Start is called before the first frame update
    
    
    public void SelectQuest(Quest quest)
    {
        questSelected = quest;
        Title.text = quest.Title;
        Discription.text = quest.Description;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
