﻿namespace Authenticator.Domain.Aggregates.User.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IAccountRepository accountRepository;

        public RegistrationService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public void CreateAccount(IAccount account)
        {
            accountRepository.Create(account);
        }

        public bool CheckIfEmailExists(string email)
        {
            return accountRepository.Read(x => x.Email == email) != null;
        }
    }
}