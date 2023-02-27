using System;
using UnityEngine;

public abstract class QuestData : ScriptableObject
{
    [Serializable]
    public class Reward
    {

        [SerializeField]
        public GameResources GameResourcesReward;

        [SerializeField]
        public PersonTemplate[] people;

        [SerializeField]
        public bool roomBlueprintUnlock;

        [SerializeField]
        public RoomType roomBlueprint;

        [SerializeField]
        public bool ClothUnlock;

        [SerializeField]
        public bool RandomCloths;
  
        [SerializeField]
        public ClothRarity CLothRarity;

        [SerializeField]
        public Sprite Cloth;

        public override string ToString()
        {
            String str = GameResourcesReward.ToString();
            if (people.Length != 0)
            {
                str += "\n" + Icons.GetPersonIconForTextMeshPro() + ":" + people.Length;
            }
            if (roomBlueprintUnlock)
            {
                str += "\n" + Icons.GetBluePrintIconForTextMeshPro() + ":" + roomBlueprint.ToString();
            }
            return str;
        }

    }

    [SerializeField]
    public bool IsGeneric;

    [SerializeField]
    public string Title;

    [SerializeField]
    public string Description;

    [SerializeField]
    public Reward reward = new Reward();


    [SerializeField]
    public int DialogIndex = -1;

    public abstract AbstractQuest CreateQuest(QuestLine questline = null);

}

