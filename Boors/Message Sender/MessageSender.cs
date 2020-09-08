using Boors.Entities;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Boors.Message_Sender
{
    public class MessageSender
    {
        public TelegramBotClient botClient;
        public string message = "***   پایش بورس   *** \n";
        public string smsMessage;
        public string telegramMessage;
        public DiscordSocketClient _client;
        public DiscordSocketClient _client2;


        List<TransactionHourlyChangeStage> ResultList;
        public MessageSender(List<TransactionHourlyChangeStage> ResultList)
        {
            try
            {
                _client = new DiscordSocketClient();
                botClient = new TelegramBotClient("1048171007:AAGpGyR66OFmXr7E03j1bSFNkucoBR4xoWg");
            }
            catch (Exception)
            {

            }
            this.ResultList = ResultList;
            message += $"تعداد {ResultList.Count} رکورد پیدا شد. \n";
            message += $"تعداد مشتری : {ResultList.Select(x => x.BourseCode).Distinct().Count()} \n";
            message += $"تعداد نماد : {ResultList.Select(x => x.ShareName).Distinct().Count()} \n";
            message += "اطلاعات تکمیلی در تلگرام ارسال شده است . \n";

            smsMessage = message + "همچنین اطلاعات رکورد اول، به شرح زیر میباشد : \n";
            smsMessage += MessageGenerator(ResultList[0]);
            telegramMessage = message + "همچنین اطلاعات رکورد ها، به شرح زیر میباشد : \n";
        }
        public bool SendSms(string receptor)
        {
            string apiAdress = @"https://api.kavenegar.com/v1/373873544136484272752B70663544763641692B415A33383667385879727849/sms/send.json?";
            apiAdress += "receptor=" + receptor;
            apiAdress += "&message=" + smsMessage;


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiAdress);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                reader.ReadToEnd();
            }
            return true;
        }

        public bool SendTelegram()
        {
            try
            {
                _ = botClient.SendTextMessageAsync("-1001462296899", telegramMessage).Result;
                foreach (var item in ResultList)
                {
                    _ = botClient.SendTextMessageAsync("-1001462296899", MessageGenerator(item)).Result;
                }
            }
            catch (Exception)
            {
            }
            return true;
        }

        public async Task<bool> SendDiscord()
        {
            _client = new DiscordSocketClient();
            await _client.LoginAsync(TokenType.Bot,
                               "Njk2OTg3NjI2ODEyNzM1NTE4.Xoxn4w.ItOgKT7LxVkzfnkMR7AZ7rWmRxE");
            await _client.StartAsync();
            await _client.SetStatusAsync(UserStatus.Online);
            _client.Ready += _client_Connected;

            _client2 = new DiscordSocketClient();
            await _client2.LoginAsync(TokenType.Bot,
                               "Njk2OTg3NjI2ODEyNzM1NTE4.Xoxn4w.ItOgKT7LxVkzfnkMR7AZ7rWmRxE");
            await _client2.StartAsync();
            await _client2.SetStatusAsync(UserStatus.Online);
            _client2.Ready += _client2_Connected;
            return true;
        }

        private Task _client2_Connected()
        {
            var server = _client.GetGuild(701739762289082499);
            var channel = server.GetTextChannel(701739762289082502);
            channel.SendMessageAsync(telegramMessage);
            foreach (var item in ResultList)
            {
                channel.SendMessageAsync(MessageGenerator(item));
            }

            return null;
        }

        private Task _client_Connected()
        {
            var server = _client.GetGuild(697013219486597211);
            var channel = server.GetTextChannel(697051540552155216);
            channel.SendMessageAsync(telegramMessage);
            foreach (var item in ResultList)
            {
                channel.SendMessageAsync(MessageGenerator(item));
            }

            return null;

        }
        public string MessageGenerator(TransactionHourlyChangeStage transaction)
        {
            string Temp = "";
            Temp += "کد بورس : " + transaction.BourseCode + "\n";
            Temp += "سهم : " + transaction.ShareName + "\n";
            Temp += "ارزش : " + transaction.Count * transaction.Price + "\n";
            Temp += "خرید/فروش : " + transaction.Action + "\n";
            Temp += "تعداد : " + transaction.Count + "\n";
            Temp += "قیمت : " + transaction.Price + "\n";
            Temp += "تاریخ و ساعت : " + transaction.CreationTime + "\n";

            return Temp;
        }
    }
}
