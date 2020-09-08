using Boors.Entities;
using Boors.Message_Sender;
using Boors.Services;
using InstaMarket.Web.Core.Data;
using InstaMarket.Web.Core.DTOModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaMarket.Web.Core.Business
{
    public class Monitoring : IMonitoring
    {
        DataBase _dataBase;
        public Monitoring()
        {
            _dataBase = new DataBase();
        }

        public ReturnClass StartMonitor()
        {
            bool IsReadyToSendMsg = false;
            MessageSender messageSender;
            List<TransactionHourlyChangeStage> ResultList = null;
            List<Monitor> monitors = ReadMonitor();
            foreach (var item in monitors)
            {
                ResultList = new ConditionClass().GetConditionResult(item.Condition).ToList();
                if (ResultList.Count > 0)
                {
                    messageSender = new MessageSender(ResultList);
                    Task.Factory.StartNew(() => messageSender.SendSms(string.Join(",", item.UserMonitor.Select(x => x.User.Phone).ToList())));
                    Task.Factory.StartNew(() => messageSender.SendTelegram());
                    Task.Run(() => messageSender.SendDiscord());
                    IsReadyToSendMsg = true;
                }
            }
            return new ReturnClass
            {
                Result = new { IsReadyToSendMsg, ResultList },
                ReturnType = Enum.ReturnType.Ok
            };
        }

        private List<Monitor> ReadMonitor()
        {
            var AllMonitors = new DataBase().Monitor.GetAll().ToList();
            try
            {
                return AllMonitors.Where(x => x.MonitorDay.Any(y => y.DayName.ToString() == DateTime.Now.DayOfWeek.ToString() && y.StartTime.Add(TimeSpan.FromSeconds(x.MaxPeriod)) <= DateTime.Now.TimeOfDay &
                  y.EndTime >= TimeRound.TimeRoundDown(DateTime.Now, TimeSpan.FromMinutes(30)).TimeOfDay)).ToList();

            }
            catch (Exception)
            {
                return new List<Monitor>();
            }
        }
    }
}
