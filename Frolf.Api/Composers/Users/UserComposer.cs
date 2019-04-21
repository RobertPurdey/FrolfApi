using Domain.Entities;
using Frolf.Api.Models.Users;
using Security.Contracts;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Frolf.Api.Composers.Users
{
    public interface IUserComposer
    {
        AppUser NewAppUser(AppUserCreationModel creationRequest);
    }

    public class UserComposer : IUserComposer
    {
        private readonly IEncryptionManager encryption;

        public UserComposer(IEncryptionManager encryption)
        {
            this.encryption = encryption;
        }

        public AppUser NewAppUser(AppUserCreationModel creationRequest)
        {
            if ( creationRequest.Password != creationRequest.ConfirmPassword )
            {
                throw new Exception("Password doesn't match");
            }

            if ( string.IsNullOrEmpty(creationRequest.LoginName ) )       throw new ArgumentNullException(nameof(creationRequest.LoginName));
            if ( string.IsNullOrEmpty(creationRequest.Handle) )           throw new ArgumentNullException(nameof(creationRequest.Handle));
            if ( string.IsNullOrEmpty(creationRequest.Password) )         throw new ArgumentNullException(nameof(creationRequest.Password));
            if ( string.IsNullOrEmpty(creationRequest.ConfirmPassword) )  throw new ArgumentNullException(nameof(creationRequest.ConfirmPassword));

            var newUser = new AppUser();
    
            newUser.LoginName   = creationRequest.LoginName;
            newUser.Handle      = creationRequest.Handle;
            newUser.FriendCode  = GenerateFriendCode();

            GenerateHashPassword(newUser, creationRequest.Password);

            // todo: deal with storing emails ? perhaps use phone numbers
            newUser.Email       = "r@p.com";

            return newUser;
        }

        private void GenerateHashPassword(AppUser newUser, string userPassword)
        {
            var salt            = GenerateSalt();
            var saltyPassword   = userPassword + salt;

            newUser.Password = encryption.Hash(saltyPassword);
            newUser.Salt     = salt;
        }

        private string GenerateSalt()
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                return Convert.ToBase64String(tokenData);
            }
        }

        private string GenerateFriendCode()
        {
            return RandomString(10);
        }

        //todo: move this to a utility class
        private string RandomString(int length)
        {
            var rando       = new Random();
            var chars       = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string value    = string.Empty;

            for (int i = 0; i < length; i++)
            {
                value += chars[rando.Next(chars.Length)];
            }

            return value;
        }

    }
}