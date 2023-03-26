namespace Paribu.Api.Login;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Rest Api Client
        var api = new ParibuRestClient();
        Console.Title = "Paribu.Net - Login";
        Console.WriteLine("Paribu.Net - Login");
        Console.WriteLine("");
        Console.WriteLine("Cihaz Kimliği Nedir?");
        Console.WriteLine(" * Kullanıdığınız cihazlarınızın hatırlanması için gereklidir.");
        Console.WriteLine(" * 6-36 karakterli random bir değer. En doğru yöntem Guid kullanmak olabilir.");
        Console.WriteLine(" * Kendiniz rastgele bir değer üretebilir devam eden işlemlerde aynı kimliği kullanabilirsiniz.");
        Console.WriteLine("");

        Console.Write("Cihaz Kimliğini Giriniz: ");
        var device = Console.ReadLine();
        api.SetDeviceId(device);

        Console.Write("Telefon Numarasını Giriniz: ");
        var mobile = Console.ReadLine();

        Console.Write("Şifrenizi Giriniz: ");
        var password = Console.ReadLine();

        var login = await api.LoginAsync(mobile, password);
        if (login.Success)
        {
            Console.Write("OTP Şifresini Giriniz: ");
            var otp = Console.ReadLine();

            var loginOtp = await api.LoginTwoFactorAsync(login.Data.Token, otp);
            if (loginOtp.Success)
            {
                if (string.IsNullOrEmpty(loginOtp.Data.Token))
                {
                    Console.WriteLine("Yeni cihaz doğrulaması gerekli. Lütfen doğrulama linkini tıklayınız. Devamında bu uygulamayı aynı cihaz kimliği ile çalıştırınız.");
                }
                else
                {
                    Console.WriteLine("Giriş İşlemi Başarılı. Paribu Token: " + loginOtp.Data.Token);
                    Console.WriteLine("Bakiyeleri görüntülemek için <ENTER>'a basın.");
                    Console.ReadLine();

                    var balances = await api.GetBalancesAsync();
                    if (balances.Success)
                    {
                        foreach (var balance in balances.Data)
                        {
                            Console.WriteLine($"{balance.Key} Total:{balance.Value.Total} Available:{balance.Value.Available}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Hata:" + balances.Error.Message);
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