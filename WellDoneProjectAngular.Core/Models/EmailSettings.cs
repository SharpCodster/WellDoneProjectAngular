using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellDoneProjectAngular.Core.Models
{
    public class EmailSettings
    {
        public SendgrindEmailSettings SendGridSettings{ get; set; }
        public SmtpEmailSettings SmtpSettings { get; set; }

        public string SenderEmailAddress { get; set; }
        public string SenderEmailFullname { get; set; }
    }

    public class SendgrindEmailSettings
    {
        public string SendGridApiKey { get; set; }
    }

    public class SmtpEmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; } = 587;
        public string Domain { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
