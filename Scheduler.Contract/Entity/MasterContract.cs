using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Contract.Entity
{
    public abstract class MasterContract
    {
        public MasterContract()
        {
            ErrorMessage = new List<string>();
        }
        public List<string> ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
