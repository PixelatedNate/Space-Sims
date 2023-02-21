using System;

[Serializable]
public class RoomSaveData
{

    public int[] roomPosition = new int[3];
    public int level;
    public int roomType;

    public string[] PeopleIds;

    public bool UnderCostruction;
    public string ConstructionTimerId;

    public RoomSaveData(AbstractRoom room)
    {

        this.roomPosition[0] = room.RoomPosition.x;
        this.roomPosition[1] = room.RoomPosition.y;
        this.roomPosition[2] = room.RoomPosition.z;
        this.level = room.Level;
        this.roomType = (int)room.RoomType;
        this.UnderCostruction = room.IsUnderConstruction;

        this.PeopleIds = new string[room.Workers.Count];

        if (room.IsUnderConstruction)
        {
            TimerSaveData data = room.ConstructionTimer.Save("Room Built", " Your new " + room.RoomType.ToString() + " room has been built");
            this.ConstructionTimerId = data.ID;
        }
        else
        {
            this.ConstructionTimerId = null;
        }

        int index = 0;
        foreach (PersonInfo person in room.Workers)
        {
            PersonSaveData personData = person.Save();
            PeopleIds[index] = personData.personId;
            index++;
        }



    }




}
