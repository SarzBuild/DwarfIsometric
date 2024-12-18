using UnityEngine;

public class Interactable : MonoBehaviour
{
    [field: SerializeField] public bool IsInteractable { get; set; }
    [field: SerializeField] public ItemData ItemData { get; private set; }
    [field: SerializeField] public float PickupTimeAmount { get; private set; }
}
