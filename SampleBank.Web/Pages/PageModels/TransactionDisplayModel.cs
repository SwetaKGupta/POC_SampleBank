namespace SampleBank.Web.Pages.PageModels
{
    public class TransactionDisplayModel
    {
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
    }
}
