using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

static class PersonSkin
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

    private static Color[] AvianColors = new Color[5]
    {
        new Color(255, 193, 144),
     new Color(239, 159, 126),
     new Color(244, 164, 129),
     new Color(230, 166, 128),
     new Color(233, 185, 149),
    };

    private static Color[] BovineColors = new Color[5]
{
    new Color(255, 193, 144),
     new Color(239, 159, 126),
     new Color(244, 164, 129),
     new Color(230, 166, 128),
     new Color(233, 185, 149),
};

    private static Color[] CanineColors = new Color[5]
{
    new Color(255, 193, 144),
     new Color(239, 159, 126),
     new Color(244, 164, 129),
     new Color(230, 166, 128),
     new Color(233, 185, 149),
};

    private static Color[] FelineColors = new Color[5]
{
    new Color(255, 193, 144),
     new Color(239, 159, 126),
     new Color(244, 164, 129),
     new Color(230, 166, 128),
     new Color(233, 185, 149),
};

    private static Vector3[] HumanColors = new Vector3[23]
    {new Vector3(255f, 193f, 144f),
     new Vector3(239f, 159f, 126f),
     new Vector3(244f, 164f, 129f),
     new Vector3(230f, 166f, 128f),
     new Vector3(233f, 185f, 149f),
     new Vector3(223, 177, 117),
     new Vector3(255, 204, 164),
     new Vector3(223, 186, 168),
     new Vector3(245, 213, 190),
     new Vector3(173, 108, 68),
     new Vector3(205, 161, 132),
     new Vector3(147, 97, 74),
     new Vector3(48, 21, 14),
     new Vector3(101, 54, 24),
     new Vector3(86, 34, 13),
     new Vector3(56, 22, 10),
     new Vector3(61, 19, 7),
     new Vector3(88, 40, 18),
     new Vector3(103, 38, 20),
     new Vector3(126, 66, 38),
     new Vector3(117, 57, 21),
     new Vector3(78, 48, 37),
     new Vector3(125, 80, 59)
    };

    private static Color[] InsectoidColors = new Color[5]
    {
        new Color(255, 193, 144),
     new Color(239, 159, 126),
     new Color(244, 164, 129),
     new Color(230, 166, 128),
     new Color(233, 185, 149),
    };

    private static Color[] PlantColors = new Color[5]
{
    new Color(255, 193, 144),
     new Color(239, 159, 126),
     new Color(244, 164, 129),
     new Color(230, 166, 128),
     new Color(233, 185, 149),
};

    private static Color[] ReptileColors = new Color[5]
{
    new Color(255, 193, 144),
     new Color(239, 159, 126),
     new Color(244, 164, 129),
     new Color(230, 166, 128),
     new Color(233, 185, 149),
};

    private static Color[] RoboticColors = new Color[5]
{
    new Color(255, 193, 144),
     new Color(239, 159, 126),
     new Color(244, 164, 129),
     new Color(230, 166, 128),
     new Color(233, 185, 149),
};


    public static Color GetRandomColor(Race race)
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
                    index = Random.Range(0, 4);
                    returnColor = AvianColors[index];
                    return returnColor;
                }

            case Race.Bovine:
                {
                    index = Random.Range(0, 4);
                    returnColor = BovineColors[index];
                    return returnColor;
                }

            case Race.Canine:
                {
                    index = Random.Range(0, 4);
                    returnColor = CanineColors[index];
                    return returnColor;
                }

            case Race.Feline:
                {
                    index = Random.Range(0, 4);
                    returnColor = FelineColors[index];
                    return returnColor;
                }

            case Race.Human:
                {
                    index = Random.Range(0, 22);
                    returnColor = new Color(HumanColors[index].x / 255, HumanColors[index].y / 255, HumanColors[index].z / 255);
                    Debug.Log("I have chose index " + index + " which has a value of " + HumanColors[index]);
                    return returnColor;
                }

            case Race.Insectoid:
                {
                    index = Random.Range(0, 4);
                    returnColor = InsectoidColors[index];
                    return returnColor;
                }

            case Race.Plant:
                {
                    index = Random.Range(0, 4);
                    returnColor = PlantColors[index];
                    return returnColor;
                }

            case Race.Reptile:
                {
                    index = Random.Range(0, 4);
                    returnColor = ReptileColors[index];
                    return returnColor;
                }

            case Race.Robotic:
                {
                    index = Random.Range(0, 4);
                    returnColor = RoboticColors[index];
                    return returnColor;
                }
        }

        return returnColor;
    }

 }