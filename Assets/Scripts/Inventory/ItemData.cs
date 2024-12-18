using UnityEngine;

[CreateAssetMenu(menuName = "Items/Data", fileName = "newItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public GameObject AssociatedPrefab { get; private set; }
}
