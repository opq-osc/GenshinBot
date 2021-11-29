using GenshinBotCore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services.Workers
{
    internal class ControllerWrapper : BackgroundService
    {
        public ControllerWrapper(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private readonly IServiceProvider serviceProvider;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            ILogger logger = scope.ServiceProvider.GetService<ILogger<ControllerWrapper>>() ?? throw new Exception();

            var controllerList = new List<Type>()
            {
                typeof(DailyNoteController),
                typeof(EmoticonController),
                typeof(HelpController),
                typeof(IndexInfoController),
                typeof(MihoyoLoginController)
            };

            foreach(var controller in controllerList)
            {
                logger.LogInformation("Controller loaded: {type}", controller.Name);
            }

            return Task.CompletedTask;
        }
    }
}
