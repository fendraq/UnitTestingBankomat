namespace TestBankomat;
using banko;

public class AccountTest
{
  //TestWithdraw
  [Fact]
  public void TestWithdraw()
  {
    Account account = new();
    int result = account.Withdraw(5000);
    Assert.Equal(5000, result);
  }
  //TestGetBalance
  [Fact]
  public void TestGetBalance()
  {
    Account account = new();
    int result = account.getBalance();
    Assert.Equal(5000, result);
  }
}