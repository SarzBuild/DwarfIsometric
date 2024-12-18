using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct QuestPanel 
{
    public GameObject Panel;
    public TextMeshProUGUI Text;
    public Button Button;
    public TextMeshProUGUI ButtonText;
    
    public QuestPanel(GameObject panel)
    {
        Panel = panel;
        Text = Panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Button = Panel.transform.GetChild(1).GetComponent<Button>();
        ButtonText = Button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateInfo(string text, bool showButton = false, string buttonText = "")
    {
        Text.text = text;
        Button.gameObject.SetActive(showButton);
        ButtonText.text = buttonText;
    }

    public void UpdatePosition(Vector3 pos)
    {
        Panel.transform.position = pos;
    }

    public void UpdateRotation(Quaternion rot)
    {
        Panel.transform.rotation = rot;
    }

    public void SetVisibility(bool isActive)
    {
        Panel.SetActive(isActive);
    }

    public Button GetButton()
    {
        return Button;
    }
}
