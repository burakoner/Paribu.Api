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