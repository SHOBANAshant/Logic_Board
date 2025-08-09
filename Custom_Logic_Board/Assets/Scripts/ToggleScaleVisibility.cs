using UnityEngine;
using UnityEngine.UI;

public class ToggleScaleVisibility : MonoBehaviour
{
    public Toggle scaleToggle;
    public GameObject scaleX;
    public GameObject scaleY;
    public GameObject scaleZ;
    public GameObject scaleLabel;

    void Awake()
    {
     
        scaleToggle.isOn = false;

        
        ApplyToggleState(false);
    }

    void Start()
    {
        scaleToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        ApplyToggleState(isOn);
    }

    void ApplyToggleState(bool isOn)
    {
        if (scaleX != null) scaleX.SetActive(isOn);
        if (scaleY != null) scaleY.SetActive(isOn);
        if (scaleZ != null) scaleZ.SetActive(isOn);
        if (scaleLabel != null) scaleLabel.SetActive(!isOn);
    }
}
