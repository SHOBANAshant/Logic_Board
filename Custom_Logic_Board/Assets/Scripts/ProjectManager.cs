using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class ProjectManager
{
    private static string savePath = Application.persistentDataPath + "/projects.json";
    public static ProjectListWrapper projectList = new ProjectListWrapper();
    public static int currentProjectIndex = -1;

    public static void Save()
    {
        string json = JsonUtility.ToJson(projectList, true);
        File.WriteAllText(savePath, json);
    }

    public static void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            projectList = JsonUtility.FromJson<ProjectListWrapper>(json);
        }
        else
        {
            projectList = new ProjectListWrapper();
        }
    }

    public static ProjectData GetCurrentProject()
    {
        if (currentProjectIndex >= 0 && currentProjectIndex < projectList.projects.Count)
            return projectList.projects[currentProjectIndex];
        return null;
    }
}
