using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleBank.Presentation;
using SampleBank.Web.Pages.PageModels;

namespace SampleBank.Web.Pages.User
{
    [Authorize]
    [ValidateAntiForgeryToken]

    public class TransferHistoryModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BankingService _bankingService;

        public TransferHistoryModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            BankingService bankingService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bankingService = bankingService;
        }

        [BindProperty]
        public List<HistoryDisplayModel> Transactions { get; set; }
        public void OnGet()
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                var allTransactions = _bankingService.GetAllTransactionsOfUser(new Guid(userId));
                var allAccountNumber = _bankingService.GetAllAccounts();
                Transactions = allTransactions.Select(x => new HistoryDisplayModel()
                {
                    TransactionId = x.TransactionReference,
                    Amount = x.Amount.ToString("0.00"),
                    DestinationAccountNumber = allAccountNumber.FirstOrDefault(a=>a.Id==x.DestinationAccountId).AccountNumber,
                    SourceAccountNumber = allAccountNumber.FirstOrDefault(a => a.Id == x.SourceAccountId).AccountNumber,
                    Date = x.CreatedOn.ToString("dd MMMM yyyy HH:mm:ss")
                }).ToList();
            }
        }
    }    
}
