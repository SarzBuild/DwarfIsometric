using UnityEngine;

public enum EQuestStatus
{
    UNOBTAINED,
    ACCEPTED,
    COMPLETED
}

[CreateAssetMenu(menuName = "Quests/Quest", fileName = "newQuest")]
public class QuestData : ScriptableObject
{
    public EQuestStatus Status { get; private set; } = default;

    public void NextStatus()
    {
        Status++;
    }

    public void ResetStatus()
    {
        Status = EQuestStatus.UNOBTAINED;
    }
}
