using System;
using System.Collections.Generic;
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
        public static Sprite GetHeadFromSpriteName(PersonInfo personInfo, string SpriteName)
        {
            string headPath = PeopleArtWorkPath + personInfo.Race.ToString() + "/" + personInfo.Gender.ToString() + "/Heads/" + SpriteName;
            Sprite headSprite = Resources.Load<Sprite>(headPath);
            return headSprite;
        }
        public static Sprite GetBodyFromSpriteName(PersonInfo personInfo, string spriteName)
        {
            string bodyPath = PeopleArtWorkPath + personInfo.Race.ToString() + "/" + personInfo.Gender.ToString() + "/Bodies/" + spriteName;
            Sprite headSprite = Resources.Load<Sprite>(bodyPath);
            return headSprite;
        }

        public static Sprite GetClothFromSpriteName(PersonInfo personInfo, string spriteName)
        {
            string clothPath = ClothsPath + personInfo.Gender;
            var values = Enum.GetValues(typeof(ClothRarity));
            foreach (var value in values)
            {
                string fullPath = clothPath + "/" + value.ToString() + "/" + spriteName;
                Sprite sprite = Resources.Load<Sprite>(fullPath);
                if (sprite != null)
                {
                    return sprite;
                }
            }
            //Sprite headSprite = Resources.Load<Sprite>(clothPath);
            return null;
        }
        public static Sprite[] GetAllCloths(PersonInfo personInfo, ClothRarity rarity)
        {
            string clothPath = ClothsPath + personInfo.Gender;
            string fullPath = clothPath + "/" + rarity.ToString();
            Sprite[] sprites = Resources.LoadAll<Sprite>(fullPath);
            return sprites;
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
        public static Sprite GetRandomCloths(PersonInfo personInfo, ClothRarity rarity = ClothRarity.Basic)
        {
            return (GetRandomCloths(personInfo.Gender, rarity));
        }

        public static Sprite GetRandomCloths(Gender gender, ClothRarity rarity)
        {
            string clothPath = ClothsPath + gender + "/" + rarity;
            return (GetRandomSpriteFromPath(clothPath));
        }

    }


    public static class QuestHelper
    {
        private const string WaittingQuestPath = "Quests/WaittingQuests/";
        private const string TranpsortQuestPath = "Quests/TransportQuests/";
        private const string questLinePath = "Quests/QuestLines/";
        private const string BuildQuestPath = "Quests/BuildRoomQuest/";

        private const string GenericQuestPath = "Quests/Generic/";

        public static WaittingQuest getRandomGenericWaittingQuests(PlanetType planetType)
        {
            return getRandomGenericWaittingQuests(1,planetType)[0];
        }

        public static WaittingQuest[] getRandomGenericWaittingQuests(int count, PlanetType planetType)
        {
           List<WaitingQuestData> AllQuests = new List<WaitingQuestData>(Resources.LoadAll<WaitingQuestData>(GenericQuestPath + "All/WaittingQuests"));
           AllQuests.AddRange(Resources.LoadAll<WaitingQuestData>(GenericQuestPath + planetType.ToString() + "/WaittingQuests"));

           WaittingQuest[] quests = new WaittingQuest[count];
           for(int i = 0; i < count; i++)
            {
                int index = Random.Range(0, AllQuests.Count);
                quests[i] = new WaittingQuest(AllQuests[index]);
                AllQuests.RemoveAt(index);
            }
            return quests;
        }


        public static WaitingQuestData GetWaittingQuestData(string name)
        {
            string path = WaittingQuestPath + name;
            WaitingQuestData questData = Resources.Load<WaitingQuestData>(path);
            return questData;
        }

        public static WaitingQuestData GetGenericWaittingQuestData(string name)
        {
            WaitingQuestData QuestData;
            string fullPathToCheckWatting = GenericQuestPath + "All/WaittingQuests/" + name;
            QuestData = Resources.Load<WaitingQuestData>(fullPathToCheckWatting);
            if (QuestData != null)
            {
                return QuestData;
            }
            else
            {
                foreach (PlanetType planetType in Enum.GetValues(typeof(PlanetType)))
                {
                    fullPathToCheckWatting = GenericQuestPath + planetType.ToString() + "/WaittingQuests/" + name;
                    QuestData = Resources.Load<WaitingQuestData>(fullPathToCheckWatting);
                    if (QuestData != null)
                    {
                        return QuestData;
                    }
                }
            }
            return null;
        }



        public static BuildRoomData GetBuildQuestData(string name)
        {
            string path = BuildQuestPath + name;
            BuildRoomData questData = Resources.Load<BuildRoomData>(path);
            return questData;
        }

        public static TransportQuestData GetTransprotQuestData(string name)
        {
            string path = TranpsortQuestPath + name;
            TransportQuestData questData = Resources.Load<TransportQuestData>(path);
            return questData;
        }


        public static QuestLineData GetQuestLineData(string name)
        {
            string path = questLinePath + name;
            QuestLineData questData = Resources.Load<QuestLineData>(path);
            return questData;
        }


    }

    public static class PlanetHelper
    {
        private const string PlanetPath = "Planets/";


        public static PlanetData GetPlanetData(string name)
        {
            string path = PlanetPath + name;
            PlanetData plant = Resources.Load<PlanetData>(path);
            return plant;
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
