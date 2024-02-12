using System;
using System.Threading.Tasks;

namespace Paribu.Api.Login;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Rest Api Client
        var api = new ParibuRestClient();
        Console.Title = "Paribu.Api - Login";
        Console.WriteLine("Paribu.Api - Login");
        Console.WriteLine("");
        Console.WriteLine("Cihaz Kimliği Nedir?");
        Console.WriteLine(" * Kullanıdığınız cihazlarınızın hatırlanması için gereklidir.");
        Console.WriteLine(" * 6-32 karakterli random bir değer. En doğru yöntem Guid kullanıp tireleri silmek olabilir.");
        Console.WriteLine(" * Kendiniz rastgele bir değer üretebilir devam eden işlemlerde aynı kimliği kullanabilirsiniz.");
        Console.WriteLine(" * Boş bırakırsanız otomatik olarak rastgele bir kod oluşacaktır.");
        Console.WriteLine("");

        api.SetDeviceId("379d139715394dcaad27262ec5d06c78");
        api.SetAccessToken("edc1dd363585c858ec9a744b08627e0ed2e3ce16818a4253a15abb32d42e466116a3e36fb406b0255eade7e845c9983ff55caa81bd9f280ba765a57d9b3944130586a35d685ba5520f5585aee71d2d349cf9c090a3c582ab1776c026d894a13bd502e976ac23dd50d4516cb11089662acc9e172f60c017b3d2445bf7bf934768043f8c104a5ad3bf7a80866c7f52f87ffe35394e17920dcd6e58f4a85f387dd1f746379c47d9fe82f044835335ef179a2da0d45e0e58d745df4962e55bd1bc9dc2fcf490f94384cb2ad50cd6c4fc286e011d8bc91ac9e7");
        var user = await api.GetUserAccountAsync();
        if (user.Success)
        {
            foreach (var balance in user.Data.Balances)
            {
                Console.WriteLine($"{balance.Key} Total:{balance.Value.Total} Available:{balance.Value.Available}");
            }
        }
        else
        {
            Console.WriteLine("Hata:" + user.Error.Message);
        }
        Console.ReadLine();










        Console.Write("Ülke Kodu (+90)      : ");
        var country = Console.ReadLine();

        Console.Write("Telefon (5XXXXXXXXX) : ");
        var mobile = Console.ReadLine();

        Console.Write("Parola               : ");
        var password = Console.ReadLine();

        Console.Write("Cihaz Kimliği        : ");
        var device = Console.ReadLine();
        if (string.IsNullOrEmpty(device))
        {
            device = Guid.NewGuid().ToString().Replace("-", "");
            Console.WriteLine("Cihaz Kimliğiniz     : " + device);
        }
        api.SetDeviceId(device);

        var login = await api.LoginAsync(country, mobile, password);
        if (login.Success)
        {
            Console.Write("OTP Şifresini Giriniz: ");
            var otp = Console.ReadLine();

            var loginOtp = await api.LoginVerifyAsync(login.Data.VerificationToken, otp);
            if (loginOtp.Success)
            {
                if (string.IsNullOrEmpty(loginOtp.Data.AuthenticationToken))
                {
                    Console.WriteLine("Yeni cihaz doğrulaması gerekli. Lütfen doğrulama linkini tıklayınız. Devamında bu uygulamayı aynı cihaz kimliği ile çalıştırınız.");
                }
                else
                {
                    api.SetAccessToken(loginOtp.Data.AuthenticationToken);
                    Console.WriteLine("Giriş İşlemi Başarılı.");
                    Console.WriteLine("Authentication Token : " + loginOtp.Data.AuthenticationToken);
                    Console.WriteLine("Bakiyeleri görüntülemek için <ENTER>'a basın.");
                    Console.ReadLine();

                    var userAccount = await api.GetUserAccountAsync();
                    if (userAccount.Success)
                    {
                        foreach (var balance in userAccount.Data.Balances)
                        {
                            Console.WriteLine($"{balance.Key} Total:{balance.Value.Total} Available:{balance.Value.Available}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Hata:" + userAccount.Error.Message);
                    }
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Hata Oluştu. OTP kodu yanlış olabilir.");
            }
        }
        else
        {
            Console.WriteLine("Hata Oluştu. Kullanıcı Adı/Şifre yanlış olabilir.");
        }

        Console.WriteLine("\n\nÇıkmak için <ENTER>'a basın.");
        Console.ReadLine();
    }
}