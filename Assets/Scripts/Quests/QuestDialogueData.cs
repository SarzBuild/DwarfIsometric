using UnityEngine;

[CreateAssetMenu(menuName = "Quests/DialogueLine", fileName = "newLine")]
public class QuestDialogueData : ScriptableObject
{
    [field: SerializeField, TextArea] public string Text { get; private set; }
}
