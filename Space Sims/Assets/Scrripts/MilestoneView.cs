using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneView : MonoBehaviour
{


    [SerializeField]
    TextMeshProUGUI Title, Discription, Rewared, ProgressBarText;

    [SerializeField]
    Transform progressBar;

    [SerializeField]
    Image milestoneIcon;

    [SerializeField]
    Milestone milstone;


    bool IsCompleate { get; set; } = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GlobalStats.Instance.PlyaerRooms.Count <= 3)
        {
            resetMilstone();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        UpdateProgressBar();
    }


    public void resetMilstone()
    {
        milstone.reset();
    }

    public void UpdateValues()
    {
        Title.text = milstone.CurrentMilstone.title;
        Discription.text = milstone.CurrentMilstone.discription;
        milestoneIcon.sprite = milstone.CurrentMilstone.MilstonIcon;

        Rewared.text = milstone.CurrentMilstone.resoureceRewared.ToString();
    }



    public void ClaimRewared()
    {
        if (!IsCompleate)
        {
            return;
        }
        else
        {
            IsCompleate = false;
            milstone.Complete();
        }

    }


    public void UpdateProgressBar()
    {
        int currentValue = MilestoneHelper.GetMilstoneValue(milstone.MilestoneBeingTracking);


        if (currentValue < milstone.CurrentMilstone.requiment)
        {
            ProgressBarText.text = currentValue + "/" + milstone.CurrentMilstone.requiment;
            float percent = (float)milstone.CurrentMilstone.requiment / 100f;
            float currentprogresspercent = (currentValue / percent) / 100f;
            progressBar.transform.localScale = new Vector3(currentprogresspercent, 1, 1);
        }
        else
        {
            IsCompleate = true;
            ProgressBarText.text = "Complete";
            progressBar.transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
