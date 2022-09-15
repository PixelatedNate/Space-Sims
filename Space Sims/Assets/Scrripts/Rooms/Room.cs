using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Serializable]
    public class RoomStats
    {
        [SerializeField]
        GameResources _upkeep;
        public GameResources Upkeep { get { return _upkeep; } }
        
        [SerializeField]
        GameResources _outPut;
        public GameResources OutPut { get { return _outPut; } }
        
        [SerializeField]
        GameResources _buildCost;
        public GameResources BuildCost { get { return _buildCost; } }

        [SerializeField]
        private int _maxWorkers;
        public int MaxWorkers { get { return _maxWorkers; } }
}
    private int level { get; set; } = 0; // overide setmethod at somepoint
    
    private RoomStats[] Roomlevels;

    public Vector3Int RoomPosition;

    [SerializeField]
    private RoomType roomType;

    [SerializeField]
    private List<PersonInfo> Workers;

    [SerializeField]
    private SkillsList DesiredSkill;

    [SerializeField]
    private RoomStats Level1RoomStat;
    [SerializeField]
    private RoomStats Level2RoomStat;
    [SerializeField]
    private RoomStats Level3RoomStat;
    //enum skin :TODO   


    private RoomStats roomStat { get { return Roomlevels[level]; } }

    void Start()
    {
        Roomlevels = new RoomStats[3] {Level1RoomStat, Level2RoomStat, Level3RoomStat };
    }

    public bool Upgrade()
    {
        if (level == Roomlevels.Length-1)
        {
            throw new Exception("trying to upgradeMaxLevel Room");
        }
        if (Roomlevels[level + 1].BuildCost < GlobalStats.Instance.PlayerResources)
        {
            level++;
            return true;
        }
        else
        {
            return false;
        }      
    }

    public bool addWorker(Person person)
    {
        if(Workers.Count == roomStat.MaxWorkers)
        {
            return false;
        }
        else
        {
            Workers.Add(person.PersonInfo);
            return true;
        }
    }

    public void RemoveWorker(Person person)
    {
        if (Workers.Count == 0)
        {
            throw new Exception("trying to reomve a person that dosn't exsist");
        }
        else
        {
            Workers.Remove(person.PersonInfo);
        }
    }


    private void OnTick (object source, EventArgs e)
    {

        //TODO: add modifer for skill of workser
        GlobalStats.Instance.PlayerResources += roomStat.OutPut;
        GlobalStats.Instance.PlayerResources -= roomStat.Upkeep;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
