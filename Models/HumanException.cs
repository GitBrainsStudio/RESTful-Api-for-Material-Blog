using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Models
{
    public class HumanException : ArgumentException
    {
        public HumanException(string _exceptionMessage) : base(_exceptionMessage) { }
    }
}
