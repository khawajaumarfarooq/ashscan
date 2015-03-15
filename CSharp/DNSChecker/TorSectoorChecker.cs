﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ExploitChecker
{
    public class TorSectoorExploitChecker : IPAddressExploitChecker
    {
        public TorSectoorExploitChecker()
            : base()
        {
        }

        protected override IEnumerable<ExploitType> TestIPAddress(byte[] addressBytes)
        {
            var address = string.Format
            (
                "{0}.{1}.{2}.{3}.tor.dnsbl.sectoor.de",
                Convert.ToInt32(addressBytes[3]),
                Convert.ToInt32(addressBytes[2]),
                Convert.ToInt32(addressBytes[1]),
                Convert.ToInt32(addressBytes[0])
            );

            try
            {
                var hostAddresses = Dns.GetHostAddresses(address);

                var exploitTypes = hostAddresses
                    .Select(x => Convert.ToInt32(x.GetAddressBytes()[3]))
                    .Where(x => x == 2)
                    .Select(x => ExploitType.TorSectoor_Blacklisted);

                return exploitTypes.ToArray();
            }
            catch
            {
                // TODO : log exception -- Diabolic 15/03/2015

                return Enumerable.Empty<ExploitType>();
            }
        }
    }
}