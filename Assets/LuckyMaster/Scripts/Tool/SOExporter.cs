using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class SOExporter : MonoBehaviour
{
    public CharacterAttributes characterAttributes;

    [Button]
    public void ExportToJson()
    {
        if (characterAttributes != null)
        {
            string json = JsonUtility.ToJson(characterAttributes, true);
            string path = Path.Combine(Application.dataPath, "DataObject.json");
            File.WriteAllText(path, json);
            Debug.Log($"Data exported to {path}");
        }
        else
        {
            Debug.LogError("No DataObject assigned for export.");
        }
    }
}