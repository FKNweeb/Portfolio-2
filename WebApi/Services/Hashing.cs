using System.Security.Cryptography;
using System.Text;

namespace WebApi.Services;

public class Hashing
{
    protected const int saltBitSize = 64;
    protected const byte saltByteSize = saltBitSize / 8;
    protected const int hashBitSize = 256;
    protected const int HashByteSize = hashBitSize / 8;

    private HashAlgorithm sha256 = SHA256.Create();
    protected RandomNumberGenerator rand = RandomNumberGenerator.Create();

    public (string hash, string salt) Hash(string password){
        byte[] salt = new byte[saltByteSize];
        rand.GetBytes(salt);
        string saltString = Convert.ToHexString(salt);
        string hash = HashSHA256(password, saltString);
        return (hash, saltString);
    }

    public bool Verify(string loginPassword, string hashedRegisteredPassword, string saltString){
        string hashedLoginPassword = HashSHA256(loginPassword, saltString);
        if (hashedLoginPassword == hashedRegisteredPassword) { return true; }
        return false;
    }

    private string HashSHA256(string password, string saltString){
        byte[] hashInput = Encoding.UTF8.GetBytes(saltString + password);
        byte[] hashOutput = sha256.ComputeHash(hashInput);
        return Convert.ToHexString(hashOutput);
    }
}
