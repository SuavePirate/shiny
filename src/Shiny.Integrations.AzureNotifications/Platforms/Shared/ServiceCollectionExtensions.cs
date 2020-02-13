﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Shiny.Push;
using Shiny.Push.AzureNotifications;


namespace Shiny
{
    public static class ServiceCollectionExtensions
    {
        public static bool UsePushAzureNotificationHubs(this IServiceCollection services,
                                                        Type delegateType,
                                                        string listenerConnectionString,
                                                        string hubName,
                                                        bool requestAccessOnStart = false)
        {
#if NETSTANDARD2_0
            return false;
#else
            services.RegisterModule(new PushModule(
                typeof(Shiny.Integrations.AzureNotifications.PushManager),
                delegateType,
                requestAccessOnStart
            ));
            services.AddSingleton(new AzureNotificationConfig(listenerConnectionString, hubName));
            return true;
#endif
        }


        public static bool UsePushAzureNotificationHubs<TPushDelegate>(this IServiceCollection services,
                                                                       string listenerConnectionString,
                                                                       string hubName,
                                                                       bool requestAccessOnStart = false)
            where TPushDelegate : class, IPushDelegate
            => services.UsePushAzureNotificationHubs(
                typeof(TPushDelegate),
                listenerConnectionString,
                hubName,
                requestAccessOnStart
            );
    }
}
