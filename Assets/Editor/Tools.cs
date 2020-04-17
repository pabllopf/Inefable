using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils.Data.Cloud;

public class Tools: Editor
{
    [MenuItem("Tools/Upgrade Resources")]
    private static void UpgradeResources()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            CloudData.SaveInDropboxAFolderAsync("/resources", Application.persistentDataPath + "/resources", new User(), new List<string>(new string[] { ".json", ".csv" }));
        };
    }
}
