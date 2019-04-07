using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{

    public class Crew
    {
        private Hub originHub;
        private bool timeUp;
        private DateTime elapsedTime;

        public Crew(Hub originHub)
        {
            this.originHub = originHub;
            timeUp = false;
        }

        public void UpdateTime(int minuteUpdate)
        {
            elapsedTime = elapsedTime.AddMinutes(minuteUpdate);

            if (elapsedTime.Hour >= 10)
            {
                timeUp = true;
            }
        }

        public Hub GetOriginHub()
        {
            return originHub;
        }

        public bool IsTimeUp()
        {
            return timeUp;
        }

        public DateTime GetElapsedTime()
        {
            return elapsedTime;
        }
    }
}
