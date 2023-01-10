using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TipsAndToutorial;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Toutorial/Toutorial", order = 1)]
public class ToutorialSegment : ScriptableObject
{
    public ToutorialMenus tipMenu;
    [SerializeField]
    public String[] HighlightedUIElements = null;
    public ToutorialSegmentPage[] pages;
    public bool completed = false;

    [Serializable]
    public class ToutorialSegmentPage
    {
        [SerializeField]
        public string pagetitle;
        [SerializeField, TextArea]
        public string pageText;
        [SerializeField]
        public string[] HighlightedUIElements = null;
    }


}
