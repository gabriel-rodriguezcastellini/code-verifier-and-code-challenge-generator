using System.Security.Cryptography;
using System.Text;

string codeVerifier = GenerateVerifier();

Console.WriteLine($"Generated Code Verifier: {codeVerifier}");
Console.WriteLine("generated code challenge: " + Convert.ToBase64String(SHA256.HashData(Encoding.ASCII.GetBytes(codeVerifier)))
    .TrimEnd('=')
    .Replace('+', '-')
    .Replace('/', '_'));
Console.Read();

static string GenerateVerifier()
{
    byte[] buffer = new byte[32];
    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
    {
        rng.GetBytes(buffer);
    }
    string base64 = Convert.ToBase64String(buffer).Replace('+', '-').Replace('/', '_').TrimEnd('=');

    return base64[..Math.Max(43, Math.Min(128, base64.Length))];
}