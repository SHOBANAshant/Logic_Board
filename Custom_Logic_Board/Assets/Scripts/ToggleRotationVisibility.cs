using UnityEngine;
using UnityEngine.UI;

public class ToggleRotationVisibility : MonoBehaviour
{
    public Toggle rotationToggle;
    public GameObject rotX;
    public GameObject rotY;
    public GameObject rotZ;
    public GameObject rotationLabel;

    void Awake()
    {
        
        rotationToggle.isOn = false;

      
        ApplyToggleState(false);
    }

    void Start()
    {
        rotationToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        ApplyToggleState(isOn);
    }

    void ApplyToggleState(bool isOn)
    {
        if (rotX != null) rotX.SetActive(isOn);
        if (rotY != null) rotY.SetActive(isOn);
        if (rotZ != null) rotZ.SetActive(isOn);
        if (rotationLabel != null) rotationLabel.SetActive(!isOn);
    }
}
