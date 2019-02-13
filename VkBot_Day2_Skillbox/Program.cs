using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace VkBot_Day2_Skillbox
{
    class Program
    {
        //
        //@MyFirstAnime016Bot
        static HttpClient httpClient = new HttpClient();
        static string token = "769031088:AAFkcLatNC60C4uI7VVY5W7Ua1yjj5vKD8s";
       

        static void Main(string[] args)
        {
            TelegramBotClient tbc = new TelegramBotClient(token);
            tbc.OnMessage +=
                delegate (object sender, MessageEventArgs e)
                {
                    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Sticker)
                    {
                        tbc.SendStickerAsync(
                           e.Message.Chat.Id,
                           e.Message.Sticker.FileId
                            );
                    }
                    else if(e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text) {
                        Console.WriteLine(e.Message.Text);
                        tbc.SendTextMessageAsync(
                           e.Message.Chat.Id,
                          getTemperature(e.Message.Text)
                            );
                    } else
                    {
                        tbc.SendTextMessageAsync(
                           e.Message.Chat.Id,
                          "Ты о чем?"
                            );
                    }
                };

                tbc.StartReceiving();
            Console.ReadKey();

        }
        static string getTemperature(string title) {
            try
            {
                string url = $"http://api.openweathermap.org/data/2.5/weather?q={title}&units=metric&APPID=5a0218be797c9e26e7e374e412f871c4";

                string data = httpClient.GetStringAsync(url).Result;

                dynamic r = JObject.Parse(data);

                return $"{r.main.temp} градусов";
            }
            catch (Exception) { return "Ошибка запроса"; }
        }
    }
}
