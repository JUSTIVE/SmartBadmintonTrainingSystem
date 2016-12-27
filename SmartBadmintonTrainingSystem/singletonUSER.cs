using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBadmintonTrainingSystem
{
    class singletonUSER
    {
        private static singletonUSER instance = new singletonUSER();
        public string uID="";
        public string uName = "";
        public string uPW = "";
        public string LoginDate = "";

        private singletonUSER()
        {
            this.uID = "";
        }
        public void setInstance(string ID,string PW)
        {
            instance.uID = ID;
            instance.uPW = PW;
            instance.LoginDate = DateTime.Now.ToString("yyyy-MM-dd");
        }
        public static singletonUSER getInstance()
        {
            return instance;
        }
    }
}
