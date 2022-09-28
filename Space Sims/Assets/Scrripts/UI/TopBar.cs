using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _fuel, _food, _minerals;
    [SerializeField]
    private TextMeshProUGUI _deltaFuel, _deltaFood, _deltaMinerals;
    [SerializeField]
    private TextMeshProUGUI _numberofPeopel, _maxPeople;


    #region publicMethods
    
    /// <summary>
    /// Set the values of the topBar.
    /// </summary>
    /// <param name="currentValues"></param>
    /// <param name="deltaValues"></param>
    /// <param name="peopleCount"> maxPeople </param>
    public void SetValues(GameResources currentValues, GameResources deltaValues, int numberofPeople, int maxPeopleCount)
    {
        _numberofPeopel.text  = numberofPeople.ToString();
        _maxPeople.text       = maxPeopleCount.ToString();
        _fuel.text            =  currentValues.Fuel.ToString("+0;-#");
        _deltaFuel.text       =  deltaValues.Fuel.ToString("+0;-#");
        _food.text            =  currentValues.Food.ToString("+0;-#");
        _deltaFood.text       =  deltaValues.Food.ToString("+0;-#");
        _minerals.text        =  currentValues.Minerals.ToString("+0;-#");
        _deltaMinerals.text   =  deltaValues.Minerals.ToString("+0;-#");
    }

    #endregion
}
