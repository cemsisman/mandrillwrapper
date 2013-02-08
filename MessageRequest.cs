using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CS.Mandrill
{

    [DataContract]
    public class MessageRequest
    {

        [DataMember(Name = "html")]
        public string Html { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "from_email")]
        public string FromEmail { get; set; }

        [DataMember(Name = "from_name")]
        public string FromName { get; set; }

        [DataMember(Name = "to")]
        public To[] To { get; set; }

        [DataMember(Name = "tags")]
        public string[] Tags { get; set; }

        [DataMember(Name = "track_opens")]
        public bool TrackOpens { get; set; }

        [DataMember(Name = "track_clicks")]
        public bool TrackClicks { get; set; }

        [DataMember(Name = "attachments")]
        public string[] Attachments { get; set; }

    }

    [DataContract]
    public class To
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}
