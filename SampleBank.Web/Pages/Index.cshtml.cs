using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleBank.Presentation;

namespace SampleBank.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BankingService _bankingService;

        public IndexModel(ILogger<IndexModel> logger, BankingService bankingService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _bankingService = bankingService;
            _userManager = userManager;
        }

        [BindProperty]
        public decimal AccountBalance { get; set; }

        [BindProperty]
        public string AccountNumber { get; set; }

        public void OnGet()
        {
            if (this.User != null)
            {
                var userId = _userManager.GetUserId(User);
                if(userId != null)
                {
                    var userAccount = _bankingService.GetAccountOfUser(new Guid(userId));
                    AccountBalance = userAccount.AccountBalance;
                    AccountNumber= userAccount.AccountNumber;
                }                
            }
        }
    }
}