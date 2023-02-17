using SampleBank.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using SampleBank.Application.Command;
using SampleBank.Application.Queries;
using SampleBank.Domain.Exceptions;
using SampleBank.Presentation;

namespace SampleBank.Test
{
    [TestFixture]
    public class BankingServiceTests
    {
        private BankingService _bankingService;
        private Mock<ILogger<BankingService>> _loggerMock;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<BankingService>>();
            _mediatorMock = new Mock<IMediator>();
            _bankingService = new BankingService(_loggerMock.Object, _mediatorMock.Object);
        }

        [Test]
        public void CreateCutomerProfile_ShouldCreateCustomerProfileAndAccount()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var email = "test@test.com";
            var firstName = "John";
            var lastName = "Doe";
            var phoneNumber = "123-456-7890";
            var customer = new Customer()
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                MemberSince = DateTime.Now,
                PhoneNumber = phoneNumber,
                CustomerId = $"C{DateTime.Now.ToString("MMddyyyy")}2"
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomersCreatedTodayQuery>(), default))
                .ReturnsAsync(2);
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCutomerProfileCommand>(), default));
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCustomerAccountCommand>(), default));

            // Act
            _bankingService.CreateCutomerProfile(userId, email, firstName, lastName, phoneNumber);

            // Assert
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetCustomersCreatedTodayQuery>(), default), Times.Once);
        }

        [Test]
        public void GetCustomerBalance_ShouldReturnCustomerBalance()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var balance = 1000m;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerBalanceQuery>(), default))
                .ReturnsAsync(balance);

            // Act
            var result = _bankingService.GetCustomerBalance(userId);

            // Assert
            Assert.AreEqual(balance, result);
        }

        [Test]
        public void TransferFund_WithValidData_ShouldReturnTransactionReference()
        {
            // Arrange
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            var fromAccount = new Account { Id = fromAccountId, AccountBalance = 1000 };
            var toAccount = new Account { Id = toAccountId, AccountBalance = 0 };
            var amount = 500;
            var toBankAccountNo = "1234";

            // Mock the mediator and queries
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByUserIdQuery>(), default(CancellationToken)))
                .ReturnsAsync(fromAccount);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByAccountNumberQuery>(), default(CancellationToken)))
                .ReturnsAsync(toAccount);

            // Act
            var result = _bankingService.TransferFund(fromAccountId, amount, toBankAccountNo);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void TransferFund_WithInvalidData_ShouldThrowException()
        {
            // Arrange
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            var fromAccount = new Account { Id = fromAccountId, AccountBalance = 500 };
            var toAccount = new Account { Id = toAccountId, AccountBalance = 0 };
            var amount = 1000; // Trying to transfer more than account balance
            var toBankAccountNo = "5678";

            // Mock the mediator and queries
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByUserIdQuery>(), default(CancellationToken)))
                .ReturnsAsync(fromAccount);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByAccountNumberQuery>(), default(CancellationToken)))
                .ReturnsAsync(toAccount);

            // Assert
            var ex = Assert.Throws<Exception>(() => _bankingService.TransferFund(fromAccountId, amount, toBankAccountNo));
            Assert.AreEqual("Insufficient Balance", ex.Message);
        }


        [Test]
        public void TransferFund_Should_TransferMoney_When_EnoughBalanceAvailable()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var amount = 100;
            var toBankAccountNo = "1234567890";

            var fromAccount = new Account { Id = Guid.NewGuid(), UserId = userId, AccountBalance = 200 };
            var toAccount = new Account { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), AccountNumber = toBankAccountNo, AccountBalance = 0 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByUserIdQuery>(), default(CancellationToken))).ReturnsAsync(fromAccount);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByAccountNumberQuery>(), default(CancellationToken))).ReturnsAsync(toAccount);

            // Act
            var result = _bankingService.TransferFund(userId, amount, toBankAccountNo);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(100, fromAccount.AccountBalance);
            Assert.AreEqual(100, toAccount.AccountBalance);
            _mediatorMock.Verify(m => m.Send(It.IsAny<FundTransferCommand>(), default(CancellationToken)), Times.Once);
        }

        [Test]
        public void TransferFund_Should_Throw_AccountNotFoundException_When_FromOrToAccountDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var amount = 100;
            var toBankAccountNo = "1234567890";

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByUserIdQuery>(), default(CancellationToken))).ReturnsAsync((Account)null);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByAccountNumberQuery>(), default(CancellationToken))).ReturnsAsync((Account)null);

            // Act & Assert
            Assert.Throws<AccountNotFoundException>(() => _bankingService.TransferFund(userId, amount, toBankAccountNo));
        }

        [Test]
        public void TransferFund_Should_Throw_Exception_When_InsufficientBalance()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var amount = 200;
            var toBankAccountNo = "1234567890";

            var fromAccount = new Account { Id = Guid.NewGuid(), UserId = userId, AccountBalance = 100 };
            var toAccount = new Account { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), AccountNumber = toBankAccountNo, AccountBalance = 0 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByUserIdQuery>(), default(CancellationToken))).ReturnsAsync(fromAccount);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountByAccountNumberQuery>(), default(CancellationToken))).ReturnsAsync(toAccount);

            // Act & Assert
            Assert.Throws<Exception>(() => _bankingService.TransferFund(userId, amount, toBankAccountNo));
        }
    }
}
