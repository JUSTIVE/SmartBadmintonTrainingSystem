using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBadmintonTrainingSystem
{
    class userData
    {
        public float time;
        public int number;
        public int type;

        public void setTime(float t)
        {
            this.time = t;
        }
        public void setNumber(int n)
        {
            this.number = n;
        }
        public void setType(int t)
        {
            this.type = t;
        }
    }
}
