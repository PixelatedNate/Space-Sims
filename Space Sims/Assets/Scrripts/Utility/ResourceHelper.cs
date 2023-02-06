using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ResourceHelper
{

    public static class PersonHelper
    {

        private const string NamePath = "TextData/Names/People/";
        private const string PeopleArtWorkPath = "ArtWork/People/";
        private const string ClothsPath = "ArtWork/Clothes/";
        public static string GetRandomName(PersonInfo personInfo)
        {
            return GetRandomName(personInfo.Gender);
        }

        public static string GetRandomName(Gender gender)
        {
            string namePath = NamePath + gender.ToString();
            return (PickRandomFromLocation(namePath)[0]);
        }

        public static Sprite GetRandomHead(PersonInfo personInfo)
        {
            return (GetRandomHead(personInfo.Race, personInfo.Gender));
        }

        public static Sprite GetRandomHead(Race race, Gender gender)
        {
            string headPath = PeopleArtWorkPath + race.ToString() + "/" + gender + "/Heads";
            return (GetRandomSpriteFromPath(headPath));
        }
        public static Sprite GetRandomBody(PersonInfo personInfo)
        {
            return (GetRandomBody(personInfo.Race, personInfo.Gender));
        }

        public static Sprite GetRandomBody(Race race, Gender gender)
        {
            string bodyPath = PeopleArtWorkPath + race.ToString() + "/" + gender + "/Bodies";
            return (GetRandomSpriteFromPath(bodyPath));
        }
        public static Sprite GetRandomCloths(PersonInfo personInfo)
        {
            return (GetRandomCloths(personInfo.Gender));
        }

        public static Sprite GetRandomCloths(Gender gender)
        {
            string clothPath = ClothsPath + gender;
            return (GetRandomSpriteFromPath(clothPath));
        }

    }



    private static Sprite GetRandomSpriteFromPath(string path)
    {
        Sprite[] sprits = Resources.LoadAll<Sprite>(path);
        int index = Random.Range(0, sprits.Length);
        return sprits[index];
    }


    public static string[] PickRandomFromLocation(string path, int number = 1)
    {
        string[] selectedValues = new string[number];
        TextAsset rawFile = Resources.Load<TextAsset>(path);
        string[] values = rawFile.text.Split('\n');

        int[] selectedValuesIndex = new int[number];
        for (int i = 0; i < number; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, values.Length);
            } while (Array.Exists(selectedValuesIndex, element => element == index));

            selectedValues[i] = values[index];
        }
        return selectedValues;

    }

}
