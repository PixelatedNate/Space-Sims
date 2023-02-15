public class QuestLine
{

    public QuestLine(QuestLineData data)
    {
        questLineData = data;
        index = 0;
    }

    int index = 0;
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

}
