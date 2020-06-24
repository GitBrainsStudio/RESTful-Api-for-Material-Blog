using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Models
{
    public class Error : LogManager
    {
        public void Write(string _error)
        {
            base.Write("errors", _error);
        }
    }
}
