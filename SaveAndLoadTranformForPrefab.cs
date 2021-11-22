using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveAndLoadTranformForPrefab : MonoBehaviour
{
    
    private TextMeshProUGUI text;

    public void Awake()
    {
        SaveSystem.Init();
        text = FindObjectOfType<TextMeshProUGUI>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

     public void LoadData()
    {
        string data = SaveSystem.LoadFromFile("qr");

        if(data != null)
        {
            SaveTranformFromQr gameSaving = JsonUtility.FromJson<SaveTranformFromQr>(data);
            text.text = gameSaving.toString();
        }
        else{
            // No save exist
        }
    }
 
    public void SaveData()
    {
        //Find Prefab
        GameObject prefab = GameObject.Find("Arc_of_Galerius");
        GameObject[] anchors = GameObject.FindGameObjectsWithTag("Anchor"); 

        //References
        SaveTranformFromQr gameSaving = new SaveTranformFromQr();

        gameSaving.position = prefab.transform.position;
        gameSaving.rotation = prefab.transform.rotation;
        gameSaving.qrName = "qr";
        gameSaving.prefabName = "Arc_of_Galerius";

        string jsonData = JsonUtility.ToJson( gameSaving , true );
        SaveSystem.SaveToFile("qr",jsonData);
    }
 
}