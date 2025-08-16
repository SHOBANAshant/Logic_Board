using UnityEngine;

public class ModuleHandler : MonoBehaviour
{
    public GameObject objectInputPanel;
    public GameObject modulesPanel; 

    public void ShowObjectInputs()
    {
        objectInputPanel.SetActive(true);
        modulesPanel.SetActive(false);  
    }

    public void HideObjectInputs()
    {
        objectInputPanel.SetActive(false);
        modulesPanel.SetActive(true); 
    }
}
