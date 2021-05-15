using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheers.Models
{
    public class Stat : BindableBase
    {
        public string Name { get; private set; }
        public int Wins { get; private set; }

        public Stat(string name, int wins)
        {
            Name = name;
            Wins = wins;
        }
    }
}
