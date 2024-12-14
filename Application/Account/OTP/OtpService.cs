using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Application.AccountManagement.OTP
{
    public static class OtpService
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly TimeSpan Timestep = TimeSpan.FromMinutes(3);
        private static readonly Encoding Encoding = new UTF8Encoding(false, true);

        public static int GenerateCode(SecurityToken securityToken, string modifier = null, int numberOfDigits = 4)
        {
            var timestepNumber = GetCurrentTimeStepNumber();
            using (var hashAlgorithm = new HMACSHA1(securityToken.GetDataNoClone()))
            {
                return ComputeTotp(hashAlgorithm, timestepNumber, modifier, numberOfDigits);
            }
        }

        public static bool ValidateCode(SecurityToken securityToken, int code, string modifier = null, int numberOfDigits = 6)
        {
            var timestepNumber = GetCurrentTimeStepNumber();
            using (var hashAlgorithm = new HMACSHA1(securityToken.GetDataNoClone()))
            {
                for (var i = -2; i <= 2; i++)
                {
                    if (ComputeTotp(hashAlgorithm, timestepNumber + (ulong)i, modifier, numberOfDigits) == code)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static ulong GetCurrentTimeStepNumber()
        {
            var delta = DateTime.UtcNow - UnixEpoch;
            return (ulong)(delta.Ticks / Timestep.Ticks);
        }

        private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timestepNumber, string modifier, int numberOfDigits)
        {
            var mod = (int)Math.Pow(10, numberOfDigits);
            var timestepAsBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
            var hash = hashAlgorithm.ComputeHash(ApplyModifier(timestepAsBytes, modifier));
            var offset = hash[hash.Length - 1] & 0xf;
            var binaryCode = (hash[offset] & 0x7f) << 24
                             | (hash[offset + 1] & 0xff) << 16
                             | (hash[offset + 2] & 0xff) << 8
                             | hash[offset + 3] & 0xff;
            return binaryCode % mod;
        }

        private static byte[] ApplyModifier(byte[] input, string modifier)
        {
            if (string.IsNullOrEmpty(modifier)) return input;
            var modifierBytes = Encoding.GetBytes(modifier);
            var combined = new byte[input.Length + modifierBytes.Length];
            Buffer.BlockCopy(input, 0, combined, 0, input.Length);
            Buffer.BlockCopy(modifierBytes, 0, combined, input.Length, modifierBytes.Length);
            return combined;
        }
    }
}
