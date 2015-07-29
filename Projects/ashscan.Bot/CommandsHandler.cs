﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ashscan.Bot
{
    using Extensibility;

    using Ashscan.Bot.Properties;

    using Mono.Addins;

    [Extension]
    public class CommandsHandler : ICommandHandler
    {

        private IIrcController controller = null;
        public CommandsHandler()
        {
            this.Commands = new[] { "tolerance", "verbose" };
            this.controller = ExtensionManager.GetController();
        }
        public IEnumerable<string> Commands { get; private set; }

        public bool OperatorsOnly
        {
            get
            {
                return true;
            }
        }

        public void Handle(IUserInfo oper, IEnumerable<string> tokens)
        {            
            var split = tokens.ToArray();

            var command = split[0];

            if (command == "tolerance")
            {
                if (split.Length > 1)
                {
                    int newlevel;
                    if (!int.TryParse(split[1], out newlevel) || newlevel < 0 || newlevel > 10)
                    {
                        controller.Say(oper.Nick, string.Format("Invalid level. Choose between 0-10"));
                    }
                    else
                    {
                        ConfigHelper.Config.ToleranceLevel = newlevel;
                        //Settings.Default.Save();
                        controller.Say(
                            oper.Nick,
                            string.Format("Current Tolerance level is now set to: {0}", ConfigHelper.Config.ToleranceLevel));
                    }
                }
                else
                {
                    controller.Say(
                        oper.Nick,
                        string.Format("Current Tolerance level is: {0}", ConfigHelper.Config.ToleranceLevel));
                }
                
            }
            else if (command == "verbose")
            {
                if (split.Length == 2)
                {
                    var val = split[1].ToLower();
                    if (val != "off" && val != "on")
                    {
                        controller.Say(oper.Nick, string.Format("Invalid value: It has to be on or off"));
                    }
                    else
                    {
                        ConfigHelper.Config.BeVerbose = val != "off";
                        //Settings.Default.Save();
                        controller.Say(
                            oper.Nick,
                            string.Format("Verbose is now set to: {0}", val.ToUpper()));
                    }
                }
                else
                {
                    controller.Say(
                        oper.Nick,
                        string.Format("Verbose is: {0}", ConfigHelper.Config.BeVerbose));
                }
            }
        }
    }
}