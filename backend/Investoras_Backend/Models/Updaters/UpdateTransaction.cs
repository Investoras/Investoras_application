namespace Investoras_Backend.Models.Updaters
{
    public class UpdateTransaction
    {
        public float Amount { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
    }
}
