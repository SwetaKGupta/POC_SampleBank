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
    public class TransferModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BankingService _bankingService;

        public TransferModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            BankingService bankingService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bankingService = bankingService;
        }

        [BindProperty]
        public TransferDataModel TransferData { get; set; }
      
        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = _userManager.GetUserId(User);
                    if (userId != null)
                    {
                        var transaction = _bankingService.TransferFund(new Guid(userId), TransferData.Amount, TransferData.ToBankAccountNo);
                        return RedirectToPage("TransferDetail", new { transactionId = transaction });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);                
            }
            
            return Page();
        }
    }

}
