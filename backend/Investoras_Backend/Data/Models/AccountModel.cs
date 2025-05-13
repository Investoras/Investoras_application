namespace Investoras_Backend.Data.Models
{
    public class AccountModel
    {
        public int AccountId { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }
        public int UserId { get; private set; }

        public static AccountModel Create(string name, decimal balance, int userId)
        {
            return new AccountModel
            {
                Name = name,
                Balance = balance,
                UserId = userId
            };
        }

    }
}
