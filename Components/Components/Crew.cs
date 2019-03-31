using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    struct Schedule
    {
        string startTime;
        string endTime;
    }

    public class Crew
    {
        private Hub originHub;
        private bool timeUp;

        public Crew(Hub originHub)
        {
            this.originHub = originHub;
            timeUp = false;
        }


    }
}
