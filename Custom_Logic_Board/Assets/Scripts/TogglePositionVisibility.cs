using UnityEngine;
using UnityEngine.UI;

public class TogglePositionVisibility : MonoBehaviour
{
    public Toggle positionToggle;
    public GameObject posX;
    public GameObject posY;
    public GameObject posZ;
    public GameObject positionLabel;

    void Awake()
    {
      
        positionToggle.isOn = false;

        
        ApplyToggleState(false);
    }

    void Start()
    {
        positionToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        ApplyToggleState(isOn);
    }

    void ApplyToggleState(bool isOn)
    {
        if (posX != null) posX.SetActive(isOn);
        if (posY != null) posY.SetActive(isOn);
        if (posZ != null) posZ.SetActive(isOn);
        if (positionLabel != null) positionLabel.SetActive(!isOn);
    }
}
