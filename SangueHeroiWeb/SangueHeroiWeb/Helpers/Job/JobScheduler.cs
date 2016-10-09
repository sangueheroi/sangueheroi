using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SangueHeroiWeb.Helpers.Job
{
    public class JobScheduler
    {

        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            var job = JobBuilder.Create<Job>().Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(nameof(Job), "JobGroup")
                .StartNow()
                .WithSimpleSchedule(s => s 
                .WithIntervalInSeconds(500)
                //.WithIntervalInHours(24)
                .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}