using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI prompText;
    [SerializeField] private TextMeshProUGUI infoText;

    public void UpdatePromptText (string promptMessage)
    {
        prompText.text = promptMessage;
    }
    public void UpdateInfoText(string infoMessage)
    {
        infoText.text = infoMessage;
    }
}
