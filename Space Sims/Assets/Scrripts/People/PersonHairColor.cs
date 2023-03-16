using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

static class PersonHairColor 
{
    private static Vector3[] AlienColors = new Vector3[5]
{
     new Vector3(50f, 168f, 82f),
     new Vector3(159f, 166f, 161f),
     new Vector3(116f, 120f, 117f),
     new Vector3(29f, 128f, 57f),
     new Vector3(49f, 128f, 29f),
};

    private static Vector3[] AquaticColors = new Vector3[6]
{
     new Vector3(242, 145, 27),
     new Vector3(242, 185, 27),
     new Vector3(134, 242, 27),
     new Vector3(32, 247, 226),
     new Vector3(46, 39, 242),
     new Vector3(46, 39, 242),

};

    private static Vector3[] AvianColors = new Vector3[8]
{
     new Vector3(255, 0, 0),
     new Vector3(255, 125, 0),
     new Vector3(255, 255, 0),
     new Vector3(125, 255, 0),
     new Vector3(0, 255, 255),
     new Vector3(0, 125, 255),
     new Vector3(125, 0, 255),
     new Vector3(255, 0, 255)
};

    private static Vector3[] BovineColors = new Vector3[8]
{
     new Vector3(255, 0, 0),
     new Vector3(255, 125, 0),
     new Vector3(255, 255, 0),
     new Vector3(125, 255, 0),
     new Vector3(0, 255, 255),
     new Vector3(0, 125, 255),
     new Vector3(125, 0, 255),
     new Vector3(255, 0, 255)
};

    private static Vector3[] CanineColors = new Vector3[8]
{
     new Vector3(255, 0, 0),
     new Vector3(255, 125, 0),
     new Vector3(255, 255, 0),
     new Vector3(125, 255, 0),
     new Vector3(0, 255, 255),
     new Vector3(0, 125, 255),
     new Vector3(125, 0, 255),
     new Vector3(255, 0, 255)
};

    private static Vector3[] FelineColors = new Vector3[8]
{
     new Vector3(255, 0, 0),
     new Vector3(255, 125, 0),
     new Vector3(255, 255, 0),
     new Vector3(125, 255, 0),
     new Vector3(0, 255, 255),
     new Vector3(0, 125, 255),
     new Vector3(125, 0, 255),
     new Vector3(255, 0, 255)
};

    private static Vector3[] HumanColors = new Vector3[8]
{
     new Vector3(255, 0, 0),
     new Vector3(255, 125, 0),
     new Vector3(255, 255, 0),
     new Vector3(125, 255, 0),
     new Vector3(0, 255, 255),
     new Vector3(0, 125, 255),
     new Vector3(125, 0, 255),
     new Vector3(255, 0, 255)
};

    private static Vector3[] InsectoidColors = new Vector3[5]
 {
     new Vector3(150, 253, 157),
     new Vector3(150, 157, 253),
     new Vector3(253, 216, 150),
     new Vector3(253, 150, 150),
     new Vector3(216, 150, 253),
 };

    private static Vector3[] PlantColors = new Vector3[8]
{
     new Vector3(255, 0, 0),
     new Vector3(255, 125, 0),
     new Vector3(255, 255, 0),
     new Vector3(125, 255, 0),
     new Vector3(0, 255, 255),
     new Vector3(0, 125, 255),
     new Vector3(125, 0, 255),
     new Vector3(255, 0, 255)
};

    private static Vector3[] ReptileColors = new Vector3[5]
{
    new Vector3(33, 236, 60),
     new Vector3(110, 193, 37),
     new Vector3(193, 163, 39),
     new Vector3(195, 86, 39),
     new Vector3(39, 195, 158),
};

    private static Vector3[] RoboticColors = new Vector3[3]
{
    new Vector3(179, 179, 179),
     new Vector3(124, 124, 124),
     new Vector3(184, 115, 51)
};

    public static Color getColor(Race race)
    {
        Color returnColor = new Color();
        int index;
        switch (race)
        {
            case Race.Alien:
                {
                    index = Random.Range(0, 4);
                    returnColor = new Color(AlienColors[index].x / 255, AlienColors[index].y / 255, AlienColors[index].z / 255);
                    return returnColor;
                }

            case Race.Aquatic:
                {
                    index = Random.Range(0, 5);
                    returnColor = new Color(AquaticColors[index].x / 255, AquaticColors[index].y / 255, AquaticColors[index].z / 255);
                    return returnColor;
                }

            case Race.Avian:
                {
                    index = Random.Range(0, 7);
                    returnColor = new Color(AvianColors[index].x / 255, AvianColors[index].y / 255, AvianColors[index].z / 255);
                    return returnColor;
                }

            case Race.Bovine:
                {
                    index = Random.Range(0, 7);
                    returnColor = new Color(BovineColors[index].x / 255, BovineColors[index].y / 255, BovineColors[index].z / 255);
                    return returnColor;
                }

            case Race.Canine:
                {
                    index = Random.Range(0, 7);
                    returnColor = new Color(CanineColors[index].x / 255, CanineColors[index].y / 255, CanineColors[index].z / 255);
                    return returnColor;
                }

            case Race.Feline:
                {
                    index = Random.Range(0, 7);
                    returnColor = new Color(FelineColors[index].x / 255, FelineColors[index].y / 255, FelineColors[index].z / 255);
                    return returnColor;
                }

            case Race.Human:
                {
                    index = Random.Range(0, 7);
                    returnColor = new Color(HumanColors[index].x / 255, HumanColors[index].y / 255, HumanColors[index].z / 255);
                    return returnColor;
                }

            case Race.Insectoid:
                {
                    index = Random.Range(0, 4);
                    returnColor = new Color(InsectoidColors[index].x / 255, InsectoidColors[index].y / 255, InsectoidColors[index].z / 255);
                    return returnColor;
                }

            case Race.Plant:
                {
                    index = Random.Range(0, 7);
                    returnColor = new Color(PlantColors[index].x / 255, PlantColors[index].y / 255, PlantColors[index].z / 255);
                    return returnColor;
                }

            case Race.Reptile:
                {
                    index = Random.Range(0, 4);
                    returnColor = new Color(ReptileColors[index].x / 255, ReptileColors[index].y / 255, ReptileColors[index].z / 255);
                    return returnColor;
                }

            case Race.Robotic:
                {
                    index = Random.Range(0, 2);
                    returnColor = new Color(RoboticColors[index].x / 255, RoboticColors[index].y / 255, RoboticColors[index].z / 255);
                    return returnColor;
                }
        }

        return returnColor;
  
    }


}
