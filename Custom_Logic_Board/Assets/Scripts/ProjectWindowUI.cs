using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProjectWindowUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField projectNameInput;
    public Button createProjectButton;
    public Transform projectListParent;     // ScrollView → Content
    public GameObject projectButtonPrefab;  // Prefab with Button + TMP_Text
    public GameObject optionsPopup;         // Panel with Edit/View/Details
    public TMP_Text selectedProjectText;

    private ProjectData selectedProject;

    void Awake()
    {
        // Load saved projects when scene starts
        ProjectManager.Load();
    }

    void Start()
    {
        if (createProjectButton != null)
            createProjectButton.onClick.AddListener(OnCreateProject);

        RefreshProjectList();

        if (optionsPopup != null)
            optionsPopup.SetActive(false);
    }

    private void OnCreateProject()
    {
        string newName = projectNameInput.text.Trim();
        if (string.IsNullOrEmpty(newName))
        {
            Debug.LogWarning("⚠️ Project name cannot be empty.");
            return;
        }

        // Create new project and add to ProjectManager
        ProjectData newProj = new ProjectData { projectName = newName };
        ProjectManager.projectList.projects.Add(newProj);
        ProjectManager.Save(); // Save to disk immediately

        // Clear input
        projectNameInput.text = "";

        // Refresh UI
        RefreshProjectList();

        // Automatically select the newly created project
        OnProjectClicked(newProj);
    }

    public void RefreshProjectList()
    {
        // Clear existing buttons
        foreach (Transform child in projectListParent)
            Destroy(child.gameObject);

        // Create buttons for each project
        foreach (ProjectData proj in ProjectManager.projectList.projects)
        {
            GameObject btnObj = Instantiate(projectButtonPrefab, projectListParent);
            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
            btnText.text = proj.projectName;

            Button btn = btnObj.GetComponent<Button>();
            btn.onClick.AddListener(() => OnProjectClicked(proj));
        }
    }

    private void OnProjectClicked(ProjectData proj)
    {
        selectedProject = proj;

        // Set current project index
        ProjectManager.currentProjectIndex = ProjectManager.projectList.projects.IndexOf(selectedProject);

        if (selectedProjectText != null)
            selectedProjectText.text = proj.projectName;

        if (optionsPopup != null)
            optionsPopup.SetActive(true);
    }

    // ====== Option Popup Buttons ======
    public void OnEditClicked()
    {
        if (selectedProject == null) return;

        ProjectManager.currentProjectIndex = ProjectManager.projectList.projects.IndexOf(selectedProject);
        SceneManager.LoadScene("scene10 1");
    }

    public void OnViewClicked()
    {
        if (selectedProject == null) return;

        ProjectManager.currentProjectIndex = ProjectManager.projectList.projects.IndexOf(selectedProject);
        SceneManager.LoadScene("scene2");
    }

    public void OnDetailsClicked()
    {
        if (selectedProject == null) return;

        Debug.Log("📌 Project: " + selectedProject.projectName);
        Debug.Log("Home Key: " + selectedProject.returnKey);

        foreach (var obj in selectedProject.objects)
        {
            Debug.Log($"Object: {obj.prefabName}, Pos: {obj.position}, Rot: {obj.rotation}, Scale: {obj.scale}");
        }
    }

    public void OnClosePopup()
    {
        // Delete the currently selected project if it exists
        if (selectedProject != null)
        {
            // Remove from the list
            ProjectManager.projectList.projects.Remove(selectedProject);

            // Save the updated list to disk
            ProjectManager.Save();

            // Clear selection
            selectedProject = null;

            if (selectedProjectText != null)
                selectedProjectText.text = "";

            // Refresh the UI
            RefreshProjectList();
        }

        // Close the popup panel
        if (optionsPopup != null)
            optionsPopup.SetActive(false);


    }
}
