using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonBackStoryView : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI textmeshPro;

    string RawText = null;


    public void SetBackStory(PersonInfo personInfo)
    {
        if(RawText == null)
        {
            RawText = textmeshPro.text;
        }
        string backStoryText = RawText;

        backStoryText = backStoryText.Replace("[Name]", personInfo.Name);
        backStoryText = backStoryText.Replace("[Planet]", personInfo.HomePlanet);
        backStoryText = backStoryText.Replace("[Job]", personInfo.Job);
        
        backStoryText = backStoryText.Replace("[Likes]", string.Join("\n", personInfo.Likes));
        backStoryText = backStoryText.Replace("[Dislikes]", string.Join("\n", personInfo.Dislikes));

        textmeshPro.text = backStoryText;
    }


}
