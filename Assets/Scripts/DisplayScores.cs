using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayScores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

//LEVEL1
        String scorePath = "Assets/CSV/Level1Score.csv";
        StreamReader reader = new StreamReader(scorePath);
        //Read from file
        int recordDmgByPlayerLvl1 = int.Parse(reader.ReadLine());
        int recordDmgByBossLvl1 = int.Parse(reader.ReadLine());

        reader.Close();

        GameObject DmgByPlayerLvl1 = GameObject.Find("DmgByPlayerLvl1");
        DmgByPlayerLvl1.GetComponent<Text>().text = recordDmgByPlayerLvl1.ToString();
        GameObject DmgByBossLvl1 = GameObject.Find("DmgByBossLvl1");
        DmgByBossLvl1.GetComponent<Text>().text = recordDmgByBossLvl1.ToString();
//LEVEL 2

        String scorePath2 = "Assets/CSV/Level2Score.csv";
        StreamReader reader2 = new StreamReader(scorePath2);
        //Read from file
        int recordDmgByPlayerLvl2 = int.Parse(reader2.ReadLine());
        int recordDmgByBossLvl2 = int.Parse(reader2.ReadLine());

        reader2.Close();

        GameObject DmgByPlayerLvl2 = GameObject.Find("DmgByPlayerLvl2");
        DmgByPlayerLvl2.GetComponent<Text>().text = recordDmgByPlayerLvl2.ToString();
        GameObject DmgByBossLvl2 = GameObject.Find("DmgByBossLvl2");
        DmgByBossLvl2.GetComponent<Text>().text = recordDmgByBossLvl2.ToString();

//LEVEL 3
        String scorePath3 = "Assets/CSV/Level3Score.csv";
        StreamReader reader3 = new StreamReader(scorePath3);
        //Read from file
        int recordDmgByPlayerLvl3 = int.Parse(reader3.ReadLine());
        int recordDmgByBossLvl3 = int.Parse(reader3.ReadLine());

        reader3.Close();

        GameObject DmgByPlayerLvl3 = GameObject.Find("DmgByPlayerLvl3");
        DmgByPlayerLvl3.GetComponent<Text>().text = recordDmgByPlayerLvl3.ToString();
        GameObject DmgByBossLvl3 = GameObject.Find("DmgByBossLvl3");
        DmgByBossLvl3.GetComponent<Text>().text = recordDmgByBossLvl3.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
