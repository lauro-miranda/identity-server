using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IdentityServer.Messages.Requests
{
    [DataContract]
    public class UserRequestMessage
    {
        [DataMember]
        public Guid Code { get; set; }

        [DataMember]
        public string Identification { get; set; } = "";

        [DataMember]
        public string Password { get; set; } = "";

        [DataMember]
        public List<ClaimRequestMessage> Claims { get; set; } = new List<ClaimRequestMessage>();

        [DataContract]
        public class ClaimRequestMessage
        {
            [DataMember]
            public string Type { get; set; } = "";

            [DataMember]
            public string Value { get; set; } = "";
        }
    }
}