using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI Fuel, Food, Minerals;
    [SerializeField]
    private TextMeshProUGUI DeltaFuel, DeltaFood, DeltaMinerals;
    [SerializeField]
    private TextMeshProUGUI People;

    public void SetValues(GameResources currentValues, GameResources deltaValues, int peopleCount)
    {
        People.text          =  peopleCount.ToString();
        Fuel.text            =  currentValues.Fuel.ToString();
        DeltaFuel.text       =  deltaValues.Fuel.ToString();
        Food.text            =  currentValues.Food.ToString();
        DeltaFood.text       =  deltaValues.Food.ToString();
        if(deltaValues.Food < 0)
        {
            DeltaFood.color = Color.red;
        }
        Minerals.text        =  currentValues.Minerals.ToString();
        DeltaMinerals.text   =  deltaValues.Minerals.ToString();
    }
}
