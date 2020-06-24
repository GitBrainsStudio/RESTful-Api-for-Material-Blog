using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Models
{
    public class Info : LogManager
    {
        public void Write(string _event)
        {
            base.Write("events", _event);
        }
    }
}
