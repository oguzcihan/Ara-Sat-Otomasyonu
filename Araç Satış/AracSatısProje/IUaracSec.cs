using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracSatısProje
{
    public delegate void aracsecildiHandle();
    interface IUaracSec
    {
        event aracsecildiHandle aracsecildi;
    }
}
