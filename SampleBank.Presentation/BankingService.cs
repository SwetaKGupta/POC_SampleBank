using SampleBank.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using SampleBank.Application.Command;
using SampleBank.Application.Queries;
using SampleBank.Domain.Entities;
using SampleBank.Domain.Exceptions;

namespace SampleBank.Presentation
{
    public delegate void UserCreated();


    public class BankingService
    {
        private readonly ILogger<BankingService> _logger;
        private readonly IMediator _mediator;

        public event UserCreated UserCreationComplete;
        public BankingService(
            ILogger<BankingService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates User Profile in our customer table and Account
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        public void CreateCutomerProfile(Guid userId, string email, string firstName, string lastName, string phoneNumber)
        {
            try
            {
                var numberOfCustomerRegisteredToday = _mediator.Send(new GetCustomersCreatedTodayQuery());
                var customer = new Customer()
                {
                    UserId = userId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    MemberSince = DateTime.Now,
                    PhoneNumber = phoneNumber,
                    CustomerId = $"C{DateTime.Now.ToString("MMddyyyy")}{numberOfCustomerRegisteredToday.Result}"
                };
                _mediator.Send(new CreateCutomerProfileCommand(customer));

                CreateCustomerAccount(customer);

                //Trigger Event
                UserCreationComplete?.Invoke();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
        public void CreateCustomerAccount(Customer customer)
        {
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                UserId = customer.UserId,
                AccountBalance = 5000, //Setting this as default for this
                AccountNumber = DateTime.Now.Ticks.ToString(),
            };
            _mediator.Send(new CreateCustomerAccountCommand(account));
        }

        /// <summary>
        /// Gets the balance of customer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public decimal GetCustomerBalance(Guid userId)
        {
            try
            {
                var customer = _mediator.Send(new GetCustomerBalanceQuery(userId));
                return customer.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return 0;
            }
        }

        /// <summary>
        /// This method is used to transfer payment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <param name="toBankName"></param>
        /// <param name="toBankAccountNo"></param>
        /// <param name="payeeName"></param>
        /// <param name="payeePhone"></param>
        /// <returns></returns>
        /// <exception cref="PaymentFailedException"></exception>
        public string TransferFund(Guid userId, decimal amount, string toBankAccountNo)
        {
            try
            {
                var fromAccount = _mediator.Send(new GetAccountByUserIdQuery(userId)).Result;
                var toAccount = _mediator.Send(new GetAccountByAccountNumberQuery(toBankAccountNo)).Result;
                if(fromAccount == null || toAccount == null)
                {
                    throw new AccountNotFoundException(toBankAccountNo);
                }
                if(fromAccount.AccountBalance < amount)
                {
                    throw new Exception("Insufficient Balance");
                }

                fromAccount.AccountBalance -= amount;
                toAccount.AccountBalance += amount;


                var transactionInfo = new Transaction()
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    SourceAccountId = fromAccount.Id,
                    CreatedOn = DateTime.Now,
                    DestinationAccountId = toAccount.Id,
                    TransactionReference = Guid.NewGuid().ToString().Substring(0, 18) // Dummy reference
                };

                
                _mediator.Send(new FundTransferCommand(fromAccount, toAccount, transactionInfo));

                return transactionInfo.TransactionReference;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Gets list of all transactions of this user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Transaction> GetAllTransactionsOfUser(Guid userId)
        {
            try
            {
                var account = _mediator.Send(new GetAccountByUserIdQuery(userId));
                if (account != null && account.Result != null)
                {
                    var transactionInfoList = _mediator.Send(new GetAllTransactionsOfUserQuery(account.Result.Id));
                    return transactionInfoList.Result.OrderByDescending(x => x.CreatedOn).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// Get Users Account Details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Account GetAccountOfUser(Guid userId)
        {
            try
            {
                var account = _mediator.Send(new GetAccountByUserIdQuery(userId));
                return account.Result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the transaction information based on transactionId
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public Transaction GetTransactionInfo(string transactionId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(transactionId))
                {
                    var transactionInfo = _mediator.Send(new GetTransactionInfoQuery(transactionId));
                    return transactionInfo.Result;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }


        /// <summary>
        /// Gets Customer Account by Id
        /// </summary>
        /// <param name="customer"></param>
        public Account GetAccountByAccountId(Guid accountId)
        {
            try
            {
                var account = _mediator.Send(new GetAccountByAccountIdQuery(accountId)).Result;
                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new AccountNotFoundException(accountId.ToString());
            }
        }

        public List<Account> GetAllAccounts()
        {
            try
            {
                var account = _mediator.Send(new GetAllAccountsQuery()).Result;
                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }
    }
}
