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

            var jobVerificaNiveisSanguineos = JobBuilder.Create<JobVerificaNiveisSanguineos>().Build();
            var triggerVerificaNiveisSanguineos = TriggerBuilder.Create()
                .WithIdentity(nameof(Job), "JobVerificaNiveisSanguineos")
                .StartNow()
                .WithSimpleSchedule(s => s 
                .WithIntervalInSeconds(60)
                //.WithIntervalInHours(24)
                .RepeatForever())
                .Build();

            var jobVerificaUltimaDataDoacaoUsuario = JobBuilder.Create<JobVerificaUltimaDataDoacaoUsuario>().Build();
            var triggerVerificaUltimaDataDoacaoUsuario = TriggerBuilder.Create()
               .WithIdentity(nameof(Job), "JobVerificaUltimaDataDoacaoUsuario")
               .StartNow()
               .WithSimpleSchedule(s => s
               //.WithIntervalInSeconds(60)
               //.WithIntervalInHours(24)
               .WithIntervalInHours(730)
               .RepeatForever())
               .Build();

            scheduler.ScheduleJob(jobVerificaNiveisSanguineos, triggerVerificaNiveisSanguineos);
            scheduler.ScheduleJob(jobVerificaUltimaDataDoacaoUsuario, triggerVerificaUltimaDataDoacaoUsuario);
        }
    }
}