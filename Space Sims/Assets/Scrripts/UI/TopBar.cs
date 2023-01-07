using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _fuel, _food, _minerals, _premiumCurancy;
    [SerializeField]
    private TextMeshProUGUI _deltaFuel, _deltaFood, _deltaMinerals;
    [SerializeField]
    private TextMeshProUGUI _numberofPeopel, _maxPeople;
    [SerializeField]
    Image _foodimg, _fuelimg, _minneralimg;


    #region publicMethods

    /// <summary>
    /// Set the values of the topBar.
    /// </summary>
    /// <param name="currentValues"></param>
    /// <param name="deltaValues"></param>
    /// <param name="peopleCount"> maxPeople </param>
    public void SetValues(GameResources currentValues, GameResources deltaValues, int numberofPeople, int maxPeopleCount, GameResources maxResources)
    {

        float foodPercentageStored = currentValues.Food / (maxResources.Food / 100f) / 100f;
        _foodimg.fillAmount = foodPercentageStored;
        setTopBarValue(foodPercentageStored, currentValues.Food, deltaValues.Food, _food, _deltaFood);

        float fuelPercentageStored = currentValues.Fuel / (maxResources.Fuel / 100f) / 100f;
        _fuelimg.fillAmount = fuelPercentageStored;
        setTopBarValue(fuelPercentageStored, currentValues.Fuel, deltaValues.Fuel, _fuel, _deltaFuel);

        float minneralPercentageStored = currentValues.Minerals / (maxResources.Minerals / 100f) / 100f;
        _minneralimg.fillAmount = minneralPercentageStored;
        setTopBarValue(minneralPercentageStored, currentValues.Minerals, deltaValues.Minerals, _minerals, _deltaMinerals);

        _numberofPeopel.text = numberofPeople.ToString();
        _maxPeople.text = maxPeopleCount.ToString();

        _premiumCurancy.text = currentValues.Premimum.ToString();
    }



    private void setTopBarValue(float percentage, int value, int deltaValue, TextMeshProUGUI textvalue, TextMeshProUGUI deltaTextValue)
    {
        Color valueTextColor = Color.white;
        Color deltaTextColor = Color.white;
        if (value <= 0)
        {
            valueTextColor = Color.red;
        }
        if (percentage == 1)
        {
            valueTextColor = Color.yellow;
        }
        if (deltaValue <= 0)
        {
            deltaTextColor = Color.red;
        }
        else
        {
            deltaTextColor = Color.green;
        }

        textvalue.color = valueTextColor;
        deltaTextValue.color = deltaTextColor;

        textvalue.text = value.ToString();
        deltaTextValue.text = deltaValue.ToString("+0;-#");
    }

    #endregion
}
