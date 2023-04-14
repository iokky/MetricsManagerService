using System.Security.Cryptography;

namespace MetricsManagerService.Services;

public static class ShortLinkGeneratorService
{

    public static string Generate()
    {
        byte[] name = new byte[2];
        RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider();
        rngCrypt.GetBytes(name);
        return Convert.ToBase64String(name);
    }
}
