using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using Server.Identity;
using Server.ServiceInterface;
using Server.ServiceProviders;

namespace Server
{
    class Program
    {


        static void Main(string[] args)
        {
            string name = "userService";

            ServiceHost host = new ServiceHost(typeof(UserService));
            var binding = new NetTcpBinding(SecurityMode.TransportWithMessageCredential, true);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            host.AddServiceEndpoint(typeof(IAddUser), binding, "net.tcp://localhost:9002/" + name);
            host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new IdentityValidator();
            host.Credentials.ServiceCertificate.Certificate = GenerateSelfSignedCertificate();
            host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode =
                UserNamePasswordValidationMode.Custom;

            host.Open();

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        public static X509Certificate2 GenerateSelfSignedCertificate()
        {
            string secp256r1Oid = "1.2.840.10045.3.1.7";  //oid for prime256v1(7)  other identifier: secp256r1

            string subjectName = "User-Service";

            var ecdsa = ECDsa.Create(ECCurve.CreateFromValue(secp256r1Oid));

            var certRequest = new CertificateRequest($"CN={subjectName}", ecdsa, HashAlgorithmName.SHA256);

            //add extensions to the request (just as an example)
            //add keyUsage
            certRequest.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));

            X509Certificate2 generatedCert = certRequest.CreateSelfSigned(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddYears(10)); // generate the cert and sign!

            X509Certificate2 pfxGeneratedCert = new X509Certificate2(generatedCert.Export(X509ContentType.Pfx)); //has to be turned into pfx or Windows at least throws a security credentials not found during sslStream.connectAsClient or HttpClient request...

            return pfxGeneratedCert;
        }
    }
}
