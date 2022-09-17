using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Alerts
{
    public class Alert
    {
        public string name;
        public string message;
    }



    //Put allerts here;
    static public Alert LowFood = new Alert {
        name = "Low Food",
        message = "AHHHHH no food"
    };

    static public Alert LowFuel = new Alert {
        name = "Low Fuel",
        message = "AHHHHH no fuel"
    };


}
