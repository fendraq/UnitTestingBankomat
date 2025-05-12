namespace TestBankomat;
using banko;

public class BankomatTest
{
  //Sätt in ett kort i bankomaten. (Bankomaten ska veta att ett kort är inne)
  [Fact]
  public void TestInsertCard()
  {
    //Setup
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);
    //Test
    bool result = bankomat.insertCard(card);
    Assert.True(result);
  }
  
  //Testa rätt/fel pinkod
  [Theory]
  [InlineData("1234", false)]
  [InlineData("0123", true)]
  public void TestEnterPin(string pin, bool expectedResult)
  {
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);
    bankomat.insertCard(card);
    bool result = bankomat.enterPin(pin);
    Assert.Equal(result, expectedResult);
  }
  
  //TESTER AV SEPARATA FUNKTIONER
  
  //TestGetMachineBalance
  [Fact]
  public void TestGetMachineBalance()
  {
    Bankomat bankomat = new();
    int result = bankomat.GetMachineBalance();
    Assert.Equal(11000, result);
  }
  
  //TestAddToMachineBalance
  [Theory]
  [InlineData(4000, 15000)]
  [InlineData(8000, 19000)]// test med negative fails
  public void TestAddToMachineBalance(int amount, int expectedBalance)
  {
    Bankomat bankomat = new();
    bankomat.AddToMachineBalance(amount);
    int result = bankomat.GetMachineBalance();
    Assert.Equal(expectedBalance, result);
  }
  
  //TestGetMessage
  /*check if return not null or list? Get string/remove string*/
  [Fact]
  public void TestGetMessage()
  {
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);
    Assert.Empty(bankomat.Messages);
    bankomat.insertCard(card);
    Assert.Contains("Card inserted", bankomat.Messages);

  }
  //TestEjectCard
  [Fact]
  public void TestEjectCard()
  {
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);
    bankomat.insertCard(card);
    bankomat.ejectCard();
    bool result = bankomat.IsCardInserted;
    Assert.False(result);
  }
  
  
  
  //TestWithdraw 5000
  [Fact]
  public void TestWithdraw5000()
  {
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);
    bankomat.insertCard(card);
    int result = bankomat.Withdraw(5000);
    Assert.Contains("Withdrawing 5000", bankomat.Messages);
    Assert.Equal(5000, result);
    
  }
  //TestWithdraw 7000 - more than account
  [Fact]
  public void TestWithdraw7000()
  {
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);
    bankomat.insertCard(card);
    int result = bankomat.Withdraw(7000);
    Assert.Contains("Card has insufficient funds", bankomat.Messages);
    Assert.Equal(0, result);
  }
  
  // TestWithdraw12000 - more than ATM
  [Fact]
  public void TestWithdraw12000()
  {
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);
    bankomat.insertCard(card);
    int result = bankomat.Withdraw(12000);
    Assert.Contains("Machine has insufficient funds", bankomat.Messages);
    Assert.Equal(0, result);
  }
  
  //TestWithdraw0 - not viable
  [Fact]
  public void TestWithdraw0()
  {
    Bankomat bankomat = new();
    Account account = new();
    Card card = new(account);
    bankomat.insertCard(card);
    int result = bankomat.Withdraw(0);
    Assert.Contains("You can not withdraw 0 or less money", bankomat.Messages);
    Assert.Equal(0, result);
  }
  
}





