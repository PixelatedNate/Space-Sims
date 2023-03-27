public class QuestLine : ISaveable<QuestLineSaveData>
{

    public QuestLine(QuestLineData data)
    {
        questLineData = data;
        index = 0;
    }
    public QuestLine(QuestLineSaveData data)
    {
        questLineData = ResourceHelper.QuestHelper.GetQuestLineData(data.QuestLineDataName);
        index = data.index;
    }


    public int index = 0;
    public QuestLineData questLineData { get; set; }

    bool completed = false;

    public void AddNextQuest()
    {
        if (!completed)
        {
            QuestManager.AddNewQuest(questLineData.quests[index].CreateQuest(this));
            index++;
            if (index == questLineData.quests.Length)
            {
                completed = true;
            }
        }
    }

    public QuestLineSaveData Save()
    {
        QuestLineSaveData data = new QuestLineSaveData(this);
        data.Save();
        return data;

    }

    public void Load(string Path)
    {
        throw new System.NotImplementedException();
    }

    public void Load(QuestLineSaveData data)
    {
        throw new System.NotImplementedException();
    }
}
