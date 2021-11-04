using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2Simulation : MonoBehaviour
{
    //Actual character objects
    public GameObject boss, warrior, rogue, mage, druid, priest;
    //Game Object names in Unity
    public String bossObjectName = "Boss";
    public String warriorObjectName = "Warrior";
    public String rogueObjectName = "Rogue";
    public String mageObjectName = "Mage";
    public String druidObjectName = "Druid";
    public String priestObjectName = "Priest";
    public int wDMG, rDMG, mDMG, dDMG, pHeal, pHealL2;
    public int bossTotal, wTotal, rTotal, mTotal, dTotal;
    public Text dmgLabel, bossTotalText, wTotalText, rTotalText, mTotalText, dTotalText;
    public Text healthLabel, bossHealth, wHealth, rHealth, mHealth, dHealth, pHealth;

    //The scripts for all the characters
    DamageDealer bossScript, warriorScript, rogueScript, mageScript, druidScript;
    Priest priestScript;

    string path = "Assets/CSV/timeStepsL2.csv";
    StreamWriter writer;
    string pathDMG = "Assets/CSV/DamageDealtL2.csv";
    StreamWriter writerDMG;

    int timeStepCounter = 0;
    bool printOneTime = true;   //This will print the final line in the CSV file after it is found that a character died

    //Count all damage done in the simulation
    private int dmgByBoss = 0, dmgByParty = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Open the writer
        writer = new StreamWriter(path);
        writerDMG = new StreamWriter(pathDMG);
        dmgLabel.text = "Damage Dealt";
        healthLabel.text = "Health";

        //Write header. COMMAS DELIMIT THE LIST TO MAKE COLUMNS
        writer.WriteLine("Time-step,Boss,Warrior,Rogue,Mage,Druid,Priest");
        writerDMG.WriteLine("Time-step,Boss,Warrior,Rogue,Mage,Druid,Priest");



        //Assign the object to the character by name in the scene
        boss = GameObject.Find(bossObjectName);
        warrior = GameObject.Find(warriorObjectName);
        rogue = GameObject.Find(rogueObjectName);
        mage = GameObject.Find(mageObjectName);
        druid = GameObject.Find(druidObjectName);
        priest = GameObject.Find(priestObjectName);

        //Get the script of that character by the character script name
        bossScript = boss.GetComponent<DamageDealer>();
        warriorScript = warrior.GetComponent<DamageDealer>();
        rogueScript = rogue.GetComponent<DamageDealer>();
        mageScript = mage.GetComponent<DamageDealer>();
        druidScript = druid.GetComponent<DamageDealer>();
        priestScript = priest.GetComponent<Priest>();

        //Set the health of all characters on start
        instantiateCharacterAttributes();

    }

    // Update is called once per frame
    void Update()
    {
        //Increment
        timeStepCounter++;
        //Check the health of the character via the script attached to it
        if (!isSimulationOver())
        {
            TimeStepPrint();
            //Do all character actions (damage and healing)
            doCharacterActions();
        }
        else
        {
            Debug.Log("Character is dead");
            if (printOneTime)
            {
                printOneTime = false;
                TimeStepPrint();
                writer.Close();
                writerDMG.Close();


                //Write the score to Level2Score.csv if it is a high score
                String scorePath = "Assets/CSV/Level2Score.csv";

                StreamReader reader = new StreamReader(scorePath);
                int recordDBP, recordDBB;   //Record damage by party, damage by boss
                //Read from file
                recordDBP = int.Parse(reader.ReadLine());
                recordDBB = int.Parse(reader.ReadLine());

                reader.Close();

                Debug.Log("Old records: DBP: " + recordDBP + ", DBB: " + recordDBB);

                if (dmgByParty > recordDBP)
                {
                    recordDBP = dmgByParty;
                }
                if (dmgByBoss > recordDBB)
                {
                    recordDBB = dmgByBoss;
                }

                //Debug log new records
                Debug.Log("New records: DBP: " + recordDBP + ", DBB: " + recordDBB);

                //Open file for writing now
                StreamWriter scoreWriter = new StreamWriter(scorePath);
                scoreWriter.WriteLine(recordDBP);
                scoreWriter.WriteLine(recordDBB);

                //Close file
                scoreWriter.Close();

                //Loads in level over scene and unloads current level scene

                SceneManager.LoadScene("Level Over");
            }
        }


    }

    //Every timestep, print the timestep # and all character healths to the csv file
    void TimeStepPrint()
    {
        writer.WriteLine("" + timeStepCounter + "," + bossScript.getHealth() + "," + warriorScript.getHealth() + "," + rogueScript.getHealth() + "," + mageScript.getHealth() + "," + druidScript.getHealth() + "," + priestScript.getHealth());
        writerDMG.WriteLine("" + timeStepCounter + "," + dmgByBoss + "," + wDMG + "," + rDMG + "," + mDMG + "," + dDMG + "," + pHeal);
    }

    //If any character has no health, simulation is over
    Boolean isSimulationOver()
    {
        if (bossScript.getHealth() <= 0 || warriorScript.getHealth() <= 0 || rogueScript.getHealth() <= 0 || mageScript.getHealth() <= 0 || druidScript.getHealth() <= 0 || priestScript.getHealth() <= 0)
        {
            return true;
        }
        return false;
    }

    //This sets the health of all the characters to their starting value when it is called
    public void instantiateCharacterAttributes()
    {
        //Constant values in case they need to be changed
        int bossHealthMax = 5000;
        int warriorHealthMax = 3000;
        int rogueHealthMax = 1500;
        int mageHealthMax = 1000;
        int druidHealthMax = 1250;
        int priestHealthMax = 900;
        int priestManaMax = 1000;

        //Set health values
        bossScript.setHealth(bossHealthMax);
        warriorScript.setHealth(warriorHealthMax);
        rogueScript.setHealth(rogueHealthMax);
        mageScript.setHealth(mageHealthMax);
        druidScript.setHealth(druidHealthMax);
        priestScript.setHealth(priestHealthMax);

        //Set priest mana
        priestScript.setMana(priestManaMax);
    }

    //Do all the damage dealing and priest healing
    public void doCharacterActions()
    {
        //Hold the value for the damage to deal to a character
        int tempDmg;

        //Deal damage to tank from the Boss
        tempDmg = bossScript.calcDamageToDeal(40, 51);
        warriorScript.takeDamage(tempDmg);
        wHealth.text = "Warrior: " + warriorScript.getHealth().ToString();
        dmgByBoss += tempDmg;

        //Note, calcDamageToDeal is a range from inclusive minimum to exclusive maximum
        tempDmg = bossScript.calcDamageToDeal(5, 21);   //Damage for all damage dealers & priest that aren't the tank

        rogueScript.takeDamage(tempDmg);
        rHealth.text = "Rogue: " + rogueScript.getHealth().ToString();
        mageScript.takeDamage(tempDmg);
        mHealth.text = "Mage: " + mageScript.getHealth().ToString();
        druidScript.takeDamage(tempDmg);
        dHealth.text = "Druid: " + druidScript.getHealth().ToString();
        priestScript.takeDamage(tempDmg);
        //Add all this damage to dmgByBoss

        tempDmg *= 4;
        dmgByBoss += tempDmg;
        bossTotalText.text = "Boss: " + dmgByBoss.ToString();

        //Deal damage to boss from damage dealers
        wDMG = warriorScript.calcDamageToDeal(5, 11);
        bossScript.takeDamage(wDMG);   //From warrior
        bossHealth.text = "Boss: " + bossScript.getHealth().ToString();
        wTotal += wDMG;
        dmgByParty += wDMG;
        wTotalText.text = "Warrior: " + wTotal.ToString();

        rDMG = rogueScript.calcDamageToDeal(15, 26);
        bossScript.takeDamage(rDMG);
        bossHealth.text = "Boss: " + bossScript.getHealth().ToString();
        rTotal += rDMG;
        dmgByParty += rDMG;
        rTotalText.text = "Rogue: " + rTotal.ToString();

        mDMG = mageScript.calcDamageToDeal(5, 31);
        bossScript.takeDamage(mDMG);
        bossHealth.text = "Boss: " + bossScript.getHealth().ToString();
        mTotal += mDMG;
        dmgByParty += mDMG;
        mTotalText.text = "Mage: " + mTotal.ToString();

        dDMG = druidScript.calcDamageToDeal(5, 16);
        bossScript.takeDamage(dDMG);
        bossHealth.text = "Boss: " + bossScript.getHealth().ToString();
        dTotal += dDMG;
        dmgByParty += dDMG;
        dTotalText.text = "Druid: " + dTotal.ToString();

        //Do priest healing

        //Determine who the priest will heal with his function
        int charCode = priestScript.smallHeal();

        //If tank is under 1500 health, then use a random heal at no mana cost
        if (warriorScript.getHealth() <= 1500)
        {
            if (priestScript.healType() == 1)
            {
                rogueScript.heal(15);
                pHealL2 = 1;
            }

            if (priestScript.healType() == 2)
            {
                rogueScript.heal(25);
                pHealL2 = 1;
            }
        }

        if (charCode == 1)      //Rogue
        {
            rogueScript.heal(15);
            rHealth.text = "Rogue: " + rogueScript.getHealth().ToString();
            pHeal = 1;
        }
        else if (charCode == 2) //Mage
        {
            mageScript.heal(15);
            mHealth.text = "Mage: " + mageScript.getHealth().ToString();
            pHeal = 1;
        }
        else if (charCode == 3) //Druid
        {
            druidScript.heal(15);
            dHealth.text = "Druid: " + druidScript.getHealth().ToString();
            pHeal = 1;
        }
        else if (charCode == 4 || charCode == 5) //Priest. Gets double odds
        {
            priestScript.heal(15);
            pHealth.text = "Priest: " + priestScript.getHealth().ToString();
            pHeal = 1;
        }
        else        //Got a -1 as a value, so don't heal anyone there's not enough mana
        {
            Debug.Log("No mana for small heal");
        }

        //Try do big heal on warrior
        if (priestScript.bigHeal())
        {
            warriorScript.heal(25);
            wHealth.text = "Warrior: " + warriorScript.getHealth().ToString();
            pHeal = 1;
        }
        else
        {
            Debug.Log("No mana for big heal");
        }

        //Regen priest mana
        priestScript.regenMana(3);
    }
}
