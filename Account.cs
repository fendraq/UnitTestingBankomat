namespace banko;

public class Account{
    int balance = 5000;
    
    public int Withdraw(int amount){
        if(amount > 0 && balance >= amount){
            balance -= amount;
            return amount;
        }else{
            return 0;
        }
    }

    public int getBalance(){
        return balance;
    }

}