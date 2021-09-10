using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Server.ServiceInterface;

namespace WCFServerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var binding = new NetTcpBinding(SecurityMode.TransportWithMessageCredential, true);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            var factory = new ChannelFactory<IAddUser>(binding,
                new EndpointAddress(new Uri("net.tcp://localhost:9002/userService"), EndpointIdentity.CreateDnsIdentity("User-Service")));
            factory.Credentials
                    .ServiceCertificate
                    .Authentication.CertificateValidationMode =
                System.ServiceModel.Security.X509CertificateValidationMode.None;
            factory.Credentials.UserName.UserName = "admin";
            factory.Credentials.UserName.Password = "admin2";

            var channel = factory.CreateChannel();
            channel.AddUser("Marko", "Markovic");
        }
    }
}
