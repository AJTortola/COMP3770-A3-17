using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public int TotalHealth;
    public int TotalMana;
    public int max_mana;

    // Start is called before the first frame update
  public int smallHeal(){

    if(TotalMana >= 5){
      this.TotalMana -= 5;
      return Random.Range(1, 6);
    }

    return -1;

  }

    public int healType() {
        return Random.Range(1, 2);
    }

  //If there is enough TotalMana decrement by 8 and return true, else return false
  public bool bigHeal(){

    if(this.TotalMana >= 8)  //Enough TotalMana to heal
    {
            this.TotalMana -= 8;
            return true;
    }
        return false;

  }

  // regens TotalMana by amount
  public void regenMana(int amount){

    if(this.max_mana < this.TotalMana + amount){
      this.TotalMana = this.max_mana;
    }
    else{
      this.TotalMana += amount;
    }

  }

    public int getHealth()
    {
        int TotalHealth = this.TotalHealth;
        return TotalHealth;
    }

    
    public void setHealth(int TotalHealth)
    {
        this.TotalHealth = TotalHealth;
    }

    public void setMana(int totalMana)
    {
        this.TotalMana = TotalMana;
    }

    public void takeDamage(int Damage)
    {

        this.TotalHealth -= Damage;

    }

    public void heal (int healAmt){
        this.TotalHealth += healAmt;
    }

}
