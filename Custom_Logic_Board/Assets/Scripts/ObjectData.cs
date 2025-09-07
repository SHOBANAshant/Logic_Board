using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ObjectData
{
    public string prefabName;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
     
}
[System.Serializable]
public class ProjectData
{
    public string projectName;
    public List<ObjectData> objects = new List<ObjectData>();
    public string returnKey;   // assigned home button
}

[System.Serializable]
public class ProjectListWrapper
{
    public List<ProjectData> projects = new List<ProjectData>();
}
