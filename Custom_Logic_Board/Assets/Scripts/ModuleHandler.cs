using UnityEngine;

public class ModuleHandler : MonoBehaviour
{
    public GameObject objectInputPanel;
    public GameObject modulesPanel;  // <-- assign this in Inspector

    public void ShowObjectInputs()
    {
        objectInputPanel.SetActive(true);
        modulesPanel.SetActive(false);  // Hide background module buttons
    }

    public void HideObjectInputs()
    {
        objectInputPanel.SetActive(false);
        modulesPanel.SetActive(true);  // Show them again if needed
    }
}
