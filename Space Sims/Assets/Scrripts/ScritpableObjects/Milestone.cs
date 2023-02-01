using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Milestone", order = 1)]
public class Milestone : ScriptableObject
{

    [SerializeField]
    public MilestoneHelper.Milestones MilestoneBeingTracking;

    int progressionIndex = 0;

    public MilstoneProgressions CurrentMilstone { get { return progressions[progressionIndex]; } }

    [Serializable]
    public class MilstoneProgressions
    {
        [SerializeField]
        public Sprite MilstonIcon;
        [SerializeField]
        public string title;
        [SerializeField]
        public string discription;
        [SerializeField]
        public int requiment;
        [SerializeField]
        public GameResources resoureceRewared;
    }

    [SerializeField]
    public MilstoneProgressions[] progressions;


}
