using Domain.Entities;
using Frolf.Api.Models.Users;
using Security.Contracts;
using System;
using System.Security.Cryptography;

namespace Frolf.Api.Composers.Users
{
    public interface IUserComposer
    {
        AppUser NewAppUser(AppUserCreationModel creationRequest);
    }

    public class UserComposer : IUserComposer
    {
        private readonly IHashManager hasher;
        private readonly IFriendCodeGenerator friendCodeGenerator;
        private readonly ISaltShaker saltShaker;

        public UserComposer(
            IHashManager hasher,
            IFriendCodeGenerator friendCodeGenerator,
            ISaltShaker saltShaker )
        {
            this.hasher              = hasher;
            this.friendCodeGenerator = friendCodeGenerator;
            this.saltShaker          = saltShaker;
        }

        public AppUser NewAppUser(AppUserCreationModel creationRequest)
        {
            if ( string.IsNullOrEmpty(creationRequest.LoginName ) )       throw new ArgumentNullException(nameof(creationRequest.LoginName));
            if ( string.IsNullOrEmpty(creationRequest.Handle) )           throw new ArgumentNullException(nameof(creationRequest.Handle));
            if ( string.IsNullOrEmpty(creationRequest.Password) )         throw new ArgumentNullException(nameof(creationRequest.Password));
            if ( string.IsNullOrEmpty(creationRequest.ConfirmPassword) )  throw new ArgumentNullException(nameof(creationRequest.ConfirmPassword));

            if ( creationRequest.Password != creationRequest.ConfirmPassword )
            {
                throw new Exception("Password doesn't match");
            }

            var newUser = new AppUser
            {
                LoginName   = creationRequest.LoginName,
                Handle      = creationRequest.Handle,
                FriendCode  = friendCodeGenerator.Generate()
            };

            SetSaltyPassword(newUser, creationRequest.Password);

            return newUser;
        }

        private void SetSaltyPassword(AppUser newUser, string userPassword)
        {
            var salt = saltShaker.Shake(32);

            newUser.Password = hasher.Hash(userPassword, salt);
            newUser.Salt     = salt;
        }
    }
}