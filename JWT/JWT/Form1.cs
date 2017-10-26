using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace JWT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private string GetJWT(string Username, string PrivateKey)
        {
            var formattedPrivateKey = $"-----BEGIN PRIVATE KEY-----\n{PrivateKey}\n-----END PRIVATE KEY-----";

            // Read in the Private Key data in PEM format and extract the Key using BouncyCastle, as BCL does not support PEM out of the box
            var pemReader = new PemReader(new StringReader(formattedPrivateKey));
            var privateKey = pemReader.ReadObject() as RsaPrivateCrtKeyParameters;

            // the key is in BouncyCastle objects - convert to equivilent BCL objects
            var cspParams = new CspParameters
            {
                KeyContainerName = Guid.NewGuid().ToString(),
                KeyNumber = (int)KeyNumber.Exchange,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            var rsaProvider = new RSACryptoServiceProvider(cspParams);
            var parameters = new RSAParameters
            {
                Modulus = privateKey.Modulus.ToByteArrayUnsigned(),
                P = privateKey.P.ToByteArrayUnsigned(),
                Q = privateKey.Q.ToByteArrayUnsigned(),
                DP = privateKey.DP.ToByteArrayUnsigned(),
                DQ = privateKey.DQ.ToByteArrayUnsigned(),
                InverseQ = privateKey.QInv.ToByteArrayUnsigned(),
                D = privateKey.Exponent.ToByteArrayUnsigned(),
                Exponent = privateKey.PublicExponent.ToByteArrayUnsigned()
            };

            rsaProvider.ImportParameters(parameters);

            // Now create the RSA credentials from the converted Key
            var signingCredentialsRSA = new SigningCredentials(
                new RsaSecurityKey(rsaProvider),
                SecurityAlgorithms.RsaSha256Signature,
                SecurityAlgorithms.Sha256Digest);

            // build the JWT using the credentials and the claims format specified in the BuyerAPI
            JwtHeader header = new JwtHeader(signingCredentialsRSA);
            JwtPayload payload = new JwtPayload(new Claim[] { new Claim("iss", Username) });
            JwtSecurityToken jwtToken = new JwtSecurityToken(header, payload);

            // Endoce the token and grant request as specified
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string encodedToken = tokenHandler.WriteToken(jwtToken);

            string grantType = "urn:ietf:params:oauth:grant-type:jwt-bearer";
            string encodedGrantType = Uri.EscapeDataString(grantType);

            string returnAccessToken = null;
            string method = "auth/oauth-token";
            string requestParameter = "grant_type=" + encodedGrantType + "&assertion=" + encodedToken;

            // for authentication we use the AuthenticationClient, which is configured to not pass JSON in the request
            var restClient = new AdslotAuthenticationRestClient(BaseUrl);
            var result = ExecuteCall<AccessResult>(restClient, method, HttpVerbs.Post, requestParameter, null);
            if(result.Success)
                returnAccessToken = result.Result.AccessToken;
            else
                HandleError(result, "GetAccessToken");

            return returnAccessToken;
        }
    }
}
