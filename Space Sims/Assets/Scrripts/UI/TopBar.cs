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
        People.text          =  peopleCount.ToString("+0;-#");
        Fuel.text            =  currentValues.Fuel.ToString("+0;-#");
        DeltaFuel.text       =  deltaValues.Fuel.ToString("+0;-#");
        Food.text            =  currentValues.Food.ToString("+0;-#");
        DeltaFood.text       =  deltaValues.Food.ToString("+0;-#");
        Minerals.text        =  currentValues.Minerals.ToString("+0;-#");
        DeltaMinerals.text   =  deltaValues.Minerals.ToString("+0;-#");
    }
}
