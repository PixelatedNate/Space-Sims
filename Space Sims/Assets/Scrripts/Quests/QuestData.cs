using System;
using UnityEngine;

public abstract class QuestData : ScriptableObject
{
    [Serializable]
    public class Reward
    {
        public Reward()
        {
            GameResourcesReward = new GameResources();
        }

        [SerializeField]
        public GameResources GameResourcesReward;

        [SerializeField]
        public PersonTemplate[] people;

        [SerializeField]
        public bool roomBlueprintUnlock;

        [SerializeField]
        public RoomType roomBlueprint;

        public override string ToString()
        {
            String str = GameResourcesReward.ToString();
            if(people.Length != 0)
            {
                str += "\n" + Icons.GetPersonIconForTextMeshPro() + ":" + people.Length;
            }
            if(roomBlueprintUnlock)
            {
                str += "\n" + Icons.GetBluePrintIconForTextMeshPro() + ":" +  roomBlueprint.ToString();
            }
            return str;
        }

    }

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

