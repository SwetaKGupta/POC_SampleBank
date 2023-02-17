using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SampleBank.Web.Pages.PageModels
{
    public class TransferDataModel
    {
            [Display(Name = "Amount to Transfer")]
            [DataType(DataType.Currency)]
            public decimal Amount { get; set; }
       
            [Required]
            [Display(Name = "Enter Bank Account Number of Payee")]
            public string ToBankAccountNo { get; set; }
    }
}
