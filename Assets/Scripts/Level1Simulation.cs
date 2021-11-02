using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    //Character objects
    public GameObject boss, warrior, rogue, mage, druid, priest;

   

    public class Boss
    {
        public string name = "Boss";
        int health = 5000; 

    }

    public class Warrior
    {

        public string name = "Warrior";
        int health = 3000; 

    }


    public class Rogue
    {

        public string name = "Rogue";
        int health = 1500;


    }


    public class Mage
    {

        public string name = "Mage";
        int health = 1000;

    }


    public class Druid
    {

        public string name = "Druid";
        int health = 1250;

    }


    public class Priest
    {

        public string name = "Priest";
        int health = 900;
        int mana = 1000;

    }




}
