using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories;
using ValetParking.Persistence.Repositories.Contracts;
using System;
using System.Linq;

namespace ValetParking.BusinessLogic.Helpers
{

    public interface IPasswordRecoveryManager
    {
        void CreateRecoveryPassword(string email);
        void ProcessPasswordRecovery(string email, string guid, string newPassword, string repeatPassword);
        }

    public class PasswordRecoveryManager : IPasswordRecoveryManager
    {
        private readonly IPasswordRecoveryRepository _passRepo;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        public PasswordRecoveryManager(IPasswordRecoveryRepository passRepo, IUserRepository userRepository,
                                       IUserService userService,
                                       IEmailSender emailSender)
        {
            _passRepo = passRepo;
            _userRepository = userRepository;
            _userService = userService;
            _emailSender = emailSender;
        }

        public void CreateRecoveryPassword(string email)
        {
            var guidToken = Guid.NewGuid().ToString();

            RecordRecoveryInDb(email, guidToken);

            SendEmailForRecovery(email, guidToken);
        }

        private void SendEmailForRecovery(string recipient, string guidToken)
        {
            string subject = "ValetParking Password Recovery";
            _emailSender.SendEmailAsync(recipient, subject, guidToken);
        }

        private void RecordRecoveryInDb(string email, string guidToken)
        {
            var passRecoveryEntity = new PasswordRecoveryEntity();
            passRecoveryEntity.token = Guid.Parse(guidToken);

            passRecoveryEntity.User = _userRepository.GetAll().Where(x => x.Email == email)?.First();
            _passRepo.Add(passRecoveryEntity);
            }

        public bool VerifyEmailAndToken(string email, string requestToken)
            {
              Guid guidRequestToken = Guid.Parse(requestToken);
            var userId = _userRepository.GetAll().Where(x => x.Email == email)?.First().Id;
            PasswordRecoveryEntity passwordRecoveryEntity = _passRepo.GetAll().Where(x => x.User.Id == userId)?.First();

            Guid storedToken = passwordRecoveryEntity.token;

            return (storedToken == guidRequestToken); 
}

        public void ProcessPasswordRecovery(string email, string guid, string newPassword, string repeatPassword)
            {
            if (VerifyEmailAndToken(email, guid) && newPassword == repeatPassword)
                {
                _userService.ChangePasswordByReset(email, newPassword);
                }
            }
        }
}
