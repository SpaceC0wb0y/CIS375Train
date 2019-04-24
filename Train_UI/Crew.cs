using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_UI
{

    //This is a class representing arbitrary crews on a given train that work on it for a 10 hour shift
    public class Crew
    {
        private Hub originHub;  //which hub they start from and return to
        private bool timeUp;  //is their shift over?
        private DateTime elapsedTime;  //how much time has passed on their shift?
        //private static double averageCrewTime;  //average shift time among all crews
        //private static double totalCrewTimeWorked;  //total crew time worked in a day

        //public void ComputeAvgTime()
        //{
        //    int numCrews;

        //    totalCrewTimeWorked += elapsedTime.Hour;

        //    averageCrewTime = totalCrewTimeWorked / numberOfCrews;
        //}

        //Description: Constructor method
        //Pre-Condition: None
        //Post-Condition: New Crew object is made
        public Crew(Hub originHub)
        {
            this.originHub = originHub;
            timeUp = false;
            elapsedTime = new DateTime(0, 0, 0, 0, 0, 0);
        }

        //Description: Manually update the crew's worked time
        //Pre-Condition: None
        //Post-Condition: Worked time (elapsed time) is updated
        public void UpdateTime(int minuteUpdate)
        {
            elapsedTime = elapsedTime.AddMinutes(minuteUpdate);

            //shift cannot exceed 10 hours
            if (elapsedTime.Hour >= 10)
            {
                timeUp = true;
            }
        }

        //Description: Get the origin hub
        //Pre-Condition: None
        //Post-Condition: Returns origin hub
        public Hub GetOriginHub()
        {
            return originHub;
        }

        //Description: Checks if crew shift is over
        //Pre-Condition: None
        //Post-Condition: Return true if shift is over, false if not
        public bool IsTimeUp()
        {
            return timeUp;
        }

        //Description: Returns how much time has passed on shift
        //Pre-Condition: None
        //Post-Condition: Returns elapsed time for crew shift
        public DateTime GetElapsedTime()
        {
            return elapsedTime;
        }
    }
}
