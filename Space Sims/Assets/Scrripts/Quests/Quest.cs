using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{

    public string Name;
    
    public TextAsset Description;
    public int Duration;

    public class outcome
    {
        float chance;
        GameResources reward;
        PersonInfo[] person;
    }








}
