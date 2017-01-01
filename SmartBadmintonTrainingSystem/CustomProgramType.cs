using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBadmintonTrainingSystem
{   
    public class CustomProgramType
    {
        public string name;
        public string trainingSet;
        public CustomProgramType()
        {
            name = "CustomProgramType";
        }
        public CustomProgramType(string trainingSet):this()
        {
            this.trainingSet = trainingSet;
        }
        public CustomProgramType(string name, string trainingSet):this(trainingSet)
        {
            this.name = name;
        }
    }
}
