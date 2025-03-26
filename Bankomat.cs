using System.Collections.Generic;

namespace banko;

public class Bankomat {

  //bool cardInserted = false;
  Card card;
  int amount;
  int machineBalance = 11000;
  List<string> msgs = new List<string>();

  public bool IsCardInserted { get; private set; } //Added for better handling of private for testning
  public IReadOnlyList<string> Messages => msgs; //Added for testing messages without exposing msgs.
  public int GetMachineBalance()
  {
    return machineBalance;
  }

  public void AddToMachineBalance(int amount)
  {
    if (amount > 0)
    {
      machineBalance += amount;
    }
  }

  public string getMessage(){
    var msg = "";
    if(msgs.Count > 0){
      // return from shift left:
      msg = msgs[0]; // pick
      msgs.RemoveAt(0); // clear
    }
    return msg; // return 
  }

  // void != deterministic

  public bool insertCard(Card card){
    IsCardInserted = true;
    this.card = card;
    msgs.Add("Card inserted");
    return IsCardInserted;
  }

  public void ejectCard(){ //Change to bool?
    IsCardInserted = false;
    msgs.Add("Card removed, don't forget it!");
  }

  public bool enterPin(string pin){
    if(card.pin == pin){
      msgs.Add("Correct pin");
      return true;            
    }else{
      msgs.Add("Incorrect pin");
      return false;
    }
  }

  public int Withdraw(int amount){
    if(amount > 0 && amount <= machineBalance && amount <= card.account.getBalance()){
      machineBalance -= amount;
      card.account.Withdraw(amount);
      msgs.Add("Withdrawing " + amount);
      return amount;
    }else{
      if(amount > machineBalance){
        msgs.Add("Machine has insufficient funds"); 
      }else if(amount > card.account.getBalance()){
        msgs.Add("Card has insufficient funds");
      }
      else
      {
        msgs.Add("You can not withdraw 0 or less money");
      }                
      return 0;
    }
  }

}