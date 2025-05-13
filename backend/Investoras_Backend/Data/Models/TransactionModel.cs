namespace Investoras_Backend.Data.Models
{
    public class TransactionModel
    {
        public int TransactionId {  get; private set; }
        public decimal Amount {  get; private set; }
        public string Description {  get; private set; }
        public int AccountId {  get; private set; }
        public int CategoryId {  get; private set; }

        public static TransactionModel Create(decimal amount, string description, int accountId, int categoryId)
        {
            return new TransactionModel
            {
                Amount = amount,
                Description = description,
                AccountId = accountId,
                CategoryId = categoryId
            };
        }

    }
}
