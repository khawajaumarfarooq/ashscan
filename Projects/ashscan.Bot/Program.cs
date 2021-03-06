﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatSharp;
using ChatSharp.Events;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace Ashscan.Bot
{
    using Mono.Addins;

    class Program
    {
        static int Main(string[] args)
        {

            var exitCode = HostFactory.Run
            (
                c =>
                {
                    c.Service<Service>
                    (
                        sc =>
                        {
                            sc.ConstructUsing<Service>(() => new Service());
                            sc.WhenStarted<Service>(service => service.Start());
                            sc.WhenStopped<Service>(service => service.Stop());
                        });

                    c.SetServiceName("ashscan.Bot");
                    c.SetDisplayName("ashscan Bot");
                    c.SetDescription("An amazing bot");

                    c.EnableShutdown();

                    c.RunAsLocalSystem();
                });

            return (int)exitCode;
        }
    }
}