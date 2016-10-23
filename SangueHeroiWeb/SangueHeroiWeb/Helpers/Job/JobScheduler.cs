﻿using Quartz;
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

            var jobVerificaNiveisSanguineos = JobBuilder.Create<JobVerificaNiveisSanguineos>().Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(nameof(Job), "JobGroup")
                .StartNow()
                .WithSimpleSchedule(s => s 
                .WithIntervalInSeconds(120)
                //.WithIntervalInHours(24)
                .RepeatForever())
                .Build();

            scheduler.ScheduleJob(jobVerificaNiveisSanguineos, trigger);
        }
    }
}