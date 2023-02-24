using System;

[Serializable]
public class GlobalStatsSaving
{

    public long SaveTimeTickUTC;

    public int PlayerFuel;
    public int PlayerFood;
    public int PlayerMinerals;
    public int PlayerPremimum;


    public int[] UnlockedRooms;

    public string[] UnlockedCloths;

    public GlobalStatsSaving(GameResources playerResources)
    {
        this.PlayerFuel = playerResources.Fuel;
        this.PlayerFood = playerResources.Food;
        this.PlayerMinerals = playerResources.Minerals;
        this.PlayerPremimum = playerResources.Premimum;
        this.SaveTimeTickUTC = DateTime.UtcNow.Ticks;

        this.UnlockedRooms = new int[UnlocksManager.UnlockedRooms.Count];
        int index = 0;
        foreach (RoomType room in UnlocksManager.UnlockedRooms)
        {
            this.UnlockedRooms[index] = (int)room;
            index++;
        }
        this.UnlockedCloths = new string[UnlocksManager.UnlockedCoths.Count];
        index = 0;
        foreach (string cloth in UnlocksManager.UnlockedCoths)
        {
            this.UnlockedCloths[index] = cloth;
            index++;
        }

    }




}
