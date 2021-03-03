using System;
using Models;
using Services;
namespace AuthorizationApp
{
    class Program
    {
        static void Main(string[] args)
        {
           var password = " ";
           var login = " ";
           var fullName = " ";
           var email = " ";
           var path = " ";

            Data.ConfigurationService.Init();

           var hashPassword = EncryiptionService.GetHashString("password");
            /*Пользователь может сохранить информацию о себе: полное имя, почту (проверять на корректность), 
                путь на аватарку с раширениями png / jpg / jpeg.*/
            while (true)
            {
                Console.Write("\n1. Зарегистрироваться\n2. Войти\n0. Выход\nВыбор: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Пожалуйста, введите Логин");
                        login = Console.ReadLine();
                        Console.WriteLine("Придумайте пароль:");
                        password = Console.ReadLine();
                        Console.WriteLine("Введите полное имя:");
                        fullName = Console.ReadLine();
                        Console.WriteLine("Введите email:");
                        email = Console.ReadLine();
                        Console.WriteLine("Введите путь к аватарке:");
                        path = Console.ReadLine();
                        var profile = new Profile {FullName = fullName, Email = email, PathToAvatar = path };
                        if (AuthUtil.Registration(login, password, profile) == true)
                            {
                                Console.WriteLine("Вы успешно зарегистрированы!");
                            }
                            else
                            {
                                Console.WriteLine("Ошибка регистрации! Введены неверные данные, либо пользователь уже зарегистрирован");
                            }
                        break;
                    case "2":
                        Console.WriteLine("Пожалуйста, введите ваш Логин");
                        login = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        password = Console.ReadLine();
                            if (AuthUtil.Authorization(login, password) == true)
                            {
                                Console.WriteLine("Вы успешно авторизованы! Для продолжения нажмите любую клавишу");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Введены неверные данные, повторите попытку");
                                break;
                            }
                        break;
                    case "0":
                        return;
                }
            }
        }
    }
}
