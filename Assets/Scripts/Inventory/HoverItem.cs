using UnityEngine;
using TMPro;

public struct HoverItem
{
    public GameObject Hover { get; private set; }
    public TextMeshProUGUI Name { get; private set; }
    public TextMeshProUGUI Description { get; private set; }
    
    public HoverItem(GameObject hover)
    {
        Hover = hover;
        Name = Hover.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Description = Hover.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateInfo(string name, string desc)
    {
        Name.text = name;
        Description.text = desc;
    }

    public void UpdatePosition(Vector3 pos)
    {
        Hover.transform.position = pos;
    }

    public void SetVisibility(bool isActive)
    {
        Hover.SetActive(isActive);
    }
}
