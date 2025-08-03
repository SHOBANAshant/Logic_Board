using UnityEngine;
using UnityEngine.UI;

public class ToggleUIManager : MonoBehaviour
{
    public Toggle positionToggle;
    public GameObject positionPanel;

    void Start()
    {
        positionToggle.onValueChanged.AddListener(OnPositionToggleChanged);
        positionPanel.SetActive(false); // Hide on start
    }

    void OnPositionToggleChanged(bool isOn)
    {
        positionPanel.SetActive(isOn);
    }
}
