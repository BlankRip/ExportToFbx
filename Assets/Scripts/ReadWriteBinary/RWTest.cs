using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RWTest : MonoBehaviour
{
    public string pStatsDiractory = "/data/player/";
    public string pStatesFileName = "playerStats.data";
    [SerializeField] Text hpText;
    [SerializeField] Text stmText;

    private string savePath;

    TestPlayerStats currentStats;

    private void Start() {
        currentStats = (TestPlayerStats) ReadFromPlayerStats();
        if(currentStats == null)
        {
            currentStats = new TestPlayerStats(100, 100);
            WriteToPlayerStats(currentStats);
        }
        UpdateUI();
    }


    private void Update() {
        if(Input.GetKeyDown(KeyCode.H))
        {
            currentStats.health += 5;
            UpdateUI();
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            currentStats.health -= 5;
            UpdateUI();
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            currentStats.stamina += 5;
            UpdateUI();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            currentStats.stamina -= 5;
            UpdateUI();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            WriteToPlayerStats(currentStats);
            Debug.Log("<color=green> Stats SAVED into file </color>");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            currentStats = (TestPlayerStats)ReadFromPlayerStats();
            UpdateUI();
            Debug.Log("<color=blue> Stats LOADED from file </color>");
        }

        // if(Input.GetKeyDown(KeyCode.Alpha0))
        // {
        //     DeletePlayerStatsFile();
        //     Debug.Log("<color=red> Stats file DELETED </color>");
        // }
    }

    private void UpdateUI() {
        hpText.text = "Health: " + currentStats.health.ToString();
        stmText.text = "Stamina: " + currentStats.stamina.ToString();
    }

    private void DeletePlayerStatsFile()
    {
        string file = Application.temporaryCachePath + pStatsDiractory + pStatesFileName;
        File.Delete(file);
    }

    public void WriteToPlayerStats(TestPlayerStats stats)
    {
        savePath = Application.temporaryCachePath + pStatsDiractory;
        Binary.WriteToFile( stats, savePath, pStatesFileName);
    }

    public object ReadFromPlayerStats()
    {
        savePath = Application.temporaryCachePath + pStatsDiractory;
        //Debug.Log(savePath);
        return Binary.ReadFromFile(savePath, pStatesFileName);
    }
}
