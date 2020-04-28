using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreWeb.Models;
using Quartz;
using System.Xml;
using Newtonsoft.Json;
using System.Data;

namespace CoreWeb.Controllers
{
    public class HomeController : Controller
    {
        //code :: 
        private IScheduler _scheduler;
        public HomeController(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public IActionResult Index()
        {
            return View();
        }
        

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CheckAvailability(int intervalinSec = 5)
        {
            //TimeSpan runInterval = new TimeSpan(1060);
            ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity($"Check Availability-{DateTime.Now}")
             .StartNow()
             .WithSimpleSchedule(scheduleBuilder =>
                scheduleBuilder
                    //.WithInterval(runInterval)
                    //.WithInterval(runInterval)
                    .WithIntervalInSeconds(intervalinSec)
                    .RepeatForever())
             .Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //.WithIdentity($"Check Availability-{DateTime.Now}")
            //.StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(15)))
            //.WithPriority(1).
            //.Build();

            IDictionary<string, object> map = new Dictionary<string, object>()
            {
                {"Current Date Time", $"{DateTime.Now}" },
                {"Tickets needed", $"5" },
                {"Concert Name", $"Rock" }
            };

            IJobDetail job = JobBuilder.Create<CheckAvailabilityTask>()
                        .WithIdentity($"Check Availability#{intervalinSec}-{DateTime.Now}")
                        .SetJobData(new JobDataMap(map))
                        .Build();
            
            //Finally schedule the job
            await _scheduler.ScheduleJob(job, trigger);
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> ReserveTickets()
        //{
        //    ITrigger trigger = TriggerBuilder.Create()
        //     .WithIdentity($"Reserve Tickets-{DateTime.Now}")
        //     .StartNow()
        //     .WithPriority(1)
        //     .Build();

        //    IDictionary<string, object> map = new Dictionary<string, object>()
        //    {
        //    };

        //    IJobDetail job = JobBuilder.Create<ReserveTicketsTask>()
        //                .WithIdentity($"Reserve Tickets:{DateTime.Now.Ticks}")
        //                .SetJobData(new JobDataMap(map))
        //                .Build();

        //    await _scheduler.ScheduleJob(job, trigger);
        //    return RedirectToAction("Index");
        //}
    }
}
