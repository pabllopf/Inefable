using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Utils.Data.Cloud;
using Utils.Data.Local;
using Utils.Updater;

public class Testt : MonoBehaviour
{
    public Item item;

    public Text text;

    private void Start()
    {
        

        

        //Item itemInstance = ScriptableObject.CreateInstance("Item") as Item;
        //DataItem dataItem = LocalData.Load<DataItem>(item.NameItem, Application.persistentDataPath + "/Resources");

        //itemInstance.NameItem = dataItem.NameItem;
        //text.text = itemInstance.NameItem;

        //CloudData.SaveInDropboxAFolder("/Resources", Application.persistentDataPath + "/Resources", new User(), new List<string>(new string[] { ".json", ".csv" }));

        //CloudData.LoadOfDropboxAFolder("/Resources", Application.persistentDataPath, new User(), new List<string>(new string[] { ".json", ".csv" }));

        //AutoUpdate.Now();

        //CloudData.LoadOfDropboxAFolder("/Resources", Application.persistentDataPath, new User("", "", "jqOpre72KvAAAAAAAAAAD3N52Cpc0--0EeBdyhz_NvEmLBqCViN-fI6dqpgsGBz-"), new List<string>(new string[] { ".asset", ".csv" }));

        //AssetDatabase.CreateAsset(AssetBundle.LoadFromFile("C:/Users/wwwam/AppData/LocalLow/Pabllopf/Inefable/Resources/items/coin.asset"), "C:/Users/wwwam/AppData/LocalLow/Pabllopf/Inefable/Resources/items");
        //Item slot = UnityEditor.AssetDatabase.LoadAssetAtPath("C:/Users/wwwam/AppData/LocalLow/Pabllopf/Inefable/Resources/items/coin.asset", typeof(Item)) as Item;
        //Debug.Log(slot.NameItem);

        //AssetDatabase.CreateAsset(AssetBundle.LoadFromFile("C:/Users/wwwam/AppData/LocalLow/Pabllopf/Inefable/Resources/items/coin.asset"), "C:/Users/wwwam/AppData/LocalLow/Pabllopf/Inefable/Resources/items");
    }
}
