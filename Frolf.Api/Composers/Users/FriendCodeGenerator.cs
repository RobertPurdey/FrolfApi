using System;

namespace Frolf.Api.Composers.Users
{
    public interface IFriendCodeGenerator
    {
        string Generate();
    }

    public class FriendCodeGenerator : IFriendCodeGenerator
    {
        public string Generate()
        {
            return RandomFriendCode(10);
        }

        private string RandomFriendCode(int length)
        {
            var rando = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string value = string.Empty;

            for (int i = 0; i < length; i++)
            {
                value += chars[rando.Next(chars.Length)];
            }

            return value;
        }
    }
}