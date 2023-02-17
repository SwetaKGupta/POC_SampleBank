using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleBank.Presentation;
using SampleBank.Web.Pages.PageModels;

namespace SampleBank.Web.Pages.User
{
    [Authorize]
    [ValidateAntiForgeryToken]

    public class TransferDetailModel : PageModel
    {
        private readonly BankingService _bankingService;

        public TransferDetailModel(BankingService bankingService)
        {
            _bankingService = bankingService;
        }
        [BindProperty]
        public TransactionDisplayModel Transaction { get; set; }

        public void OnGet(string transactionId)
        {
            var transaction = _bankingService.GetTransactionInfo(transactionId);
            if (transaction != null)
            {
                var fromAccount = _bankingService.GetAccountByAccountId(transaction.SourceAccountId);
                var toAccount = _bankingService.GetAccountByAccountId(transaction.DestinationAccountId);
                Transaction = new TransactionDisplayModel()
                {
                    Amount = transaction.Amount.ToString("0.00"),
                    FromAccount = fromAccount.AccountNumber,
                    ToAccount = toAccount.AccountNumber,
                    TransactionId = transaction.TransactionReference,
                    Type = transaction.Amount > 0 ? "Debit" : "Credit",
                    Date = transaction.CreatedOn.ToString("dd MMMM yyyy HH:mm:ss")
                };
            }
        }
    }
}
