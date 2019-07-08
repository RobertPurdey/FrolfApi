using System;

namespace Frolf.Api.Models.Clients
{
    public class ClientRequestModel
    {
        public string Token { get; set; }
        public string Command { get; set; }
        public Guid GameId { get; set; }
    }
}