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

    private static Vector3[] AvianColors = new Vector3[5]
    {
     new Vector3(105, 222, 55),
     new Vector3(55, 144, 222),
     new Vector3(166, 55, 222),
     new Vector3(222, 177, 55),
     new Vector3(222, 55, 55),
    };

    private static Vector3[] BovineColors = new Vector3[5]
{
    new Vector3(134, 110, 44),
     new Vector3(80, 77, 69),
     new Vector3(174, 174, 174),
     new Vector3(236, 236, 236),
     new Vector3(182, 150, 141),
};

    private static Vector3[] CanineColors = new Vector3[5]
{
    new Vector3(183, 110, 54),
     new Vector3(230, 219, 120),
     new Vector3(91, 91, 87),
     new Vector3(236, 236, 236),
     new Vector3(129, 158, 154),
};

    private static Vector3[] FelineColors = new Vector3[5]
{
    new Vector3(253, 175, 86),
     new Vector3(132, 130, 128),
     new Vector3(90, 90, 90),
     new Vector3(224, 235, 153),
     new Vector3(206, 150, 86),
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

    private static Vector3[] InsectoidColors = new Vector3[5]
    {
     new Vector3(150, 253, 157),
     new Vector3(150, 157, 253),
     new Vector3(253, 216, 150),
     new Vector3(253, 150, 150),
     new Vector3(216, 150, 253),
    };

    private static Vector3[] PlantColors = new Vector3[5]
{
    new Vector3(117, 162, 104),
     new Vector3(148, 162, 104),
     new Vector3(162, 148, 104),
     new Vector3(104, 162, 158),
     new Vector3(162, 104, 133),
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
                    returnColor = new Color(AvianColors[index].x / 255, AvianColors[index].y / 255, AvianColors[index].z / 255);
                    return returnColor;
                }

            case Race.Bovine:
                {
                    index = Random.Range(0, 4);
                    returnColor = new Color(BovineColors[index].x / 255, BovineColors[index].y / 255, BovineColors[index].z / 255);
                    return returnColor;
                }

            case Race.Canine:
                {
                    index = Random.Range(0, 4);
                    returnColor = new Color(CanineColors[index].x / 255, CanineColors[index].y / 255, CanineColors[index].z / 255);
                    return returnColor;
                }

            case Race.Feline:
                {
                    index = Random.Range(0, 4);
                    returnColor = new Color(FelineColors[index].x / 255, FelineColors[index].y / 255, FelineColors[index].z / 255);
                    return returnColor;
                }

            case Race.Human:
                {
                    index = Random.Range(0, 22);
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
                    index = Random.Range(0, 4);
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