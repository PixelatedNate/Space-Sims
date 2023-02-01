using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneView : MonoBehaviour
{


    [SerializeField]
    TextMeshProUGUI Title, Discription, Rewared, ProgressBarText;

    [SerializeField]
    Image milestoneIcon;
        
    [SerializeField]
    Milestone milstone;

    // Start is called before the first frame update
    void Start()
    {
        UpdateValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateValues()
    {
        Title.text = milstone.CurrentMilstone.title;
        Discription.text = milstone.CurrentMilstone.discription;
        milestoneIcon.sprite = milstone.CurrentMilstone.MilstonIcon; 
    }


    public void UpdateProgressBar()
    {
        ProgressBarText.text = MilestoneHelper.GetMilstoneValue(milstone.MilestoneBeingTracking) + "/" + milstone.CurrentMilstone.requiment;
    }

}
