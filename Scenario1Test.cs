namespace TestBankomat;
using banko;

public class Scenario1Test
{
      [Fact]
      public void TestScenario1()
      {
            //Arrange
            Bankomat bankomat = new();
            Account account = new();
            Card card = new(account);
            
            //Sätt in ett kort i bankomaten.
            bool resultInsertCard = bankomat.insertCard(card);
            Assert.True(resultInsertCard);
            
            //Mata in den felaktiga pinkoden 1234 i bankomaten. (Pinkoden ska vara sparad på kortet, men det är bankomaten som ska veta om rätt eller fel kod matats in)
            bool resultWrongPin = bankomat.enterPin("1234");
            Assert.False(resultWrongPin);
            
            //Mata in den korrekta pinkoden 0123.
            bool resultRightPin = bankomat.enterPin("0123");
            Assert.True(resultRightPin);
            
            //Ange 5000 kr att ta ut via bankomaten. Balansen ska tas från kontot som är kopplat till kortet.
            int resultWithdraw5000 = bankomat.Withdraw(5000);
            Assert.Equal(5000, resultWithdraw5000);
            Assert.Contains("Withdrawing 5000", bankomat.Messages);

            //Mata ut kortet ur bankomaten.
            bankomat.ejectCard();
            bool resultEjectCard = bankomat.IsCardInserted;
            Assert.False(resultEjectCard);
            Assert.Contains("Card removed, don't forget it!", bankomat.Messages);
            
            //Sätt in samma kort i samma bankomat igen.
            bool resultInsertSameCard = bankomat.insertCard(card);
            Assert.True(resultInsertSameCard);
            
            //Mata in pinkoden 0123.
            bool resultRightPinAgain = bankomat.enterPin("0123");
            Assert.True(resultRightPinAgain);
            
            //Ange 7000 att ta ut. (Nu ska det inte finnas pengar så det räcker på bankomaten)
            int resultWithdraw7000 = bankomat.Withdraw(7000);
            Assert.Equal(0, resultWithdraw7000);
            Assert.Contains("Machine has insufficient funds", bankomat.Messages);
            
            //Ange 6000 att ta ut. (Nu räckte bankomatens pengar precis, men inte pengarna på kontot)
            int resultWithdraw6000 = bankomat.Withdraw(6000);
            Assert.Equal(0, resultWithdraw6000);
            Assert.Contains("Card has insufficient funds", bankomat.Messages);

      }
      
      
}