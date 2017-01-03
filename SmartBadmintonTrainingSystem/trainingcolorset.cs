using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBadmintonTrainingSystem
{
    public class TrainingColorSet
    {
        enum ColorEnum { RED, GREEN, BLUE, YELLOW };
        int[] dataset;
        int[] checker;
        public int[][] generatedData;//times, one-based pole
        public int times;
        Random randSeed = new Random();
        public TrainingColorSet(int[] dataset,int times) 
        {
            this.dataset = dataset;
            this.times = times;

            generatedData = new int[this.times][];
            for (int i=0;i<times;i++)
            {
                checker = new int[8];
                generatedData[i] = new int[5];
                for(int j = 0; j < 5; j++)
                {
                    generatedData[i][j] = randSeed.Next(1,8);
                    if (checker[generatedData[i][j]-1]!=0)
                    {
                        j--;
                    }
                    else
                    {
                        checker[generatedData[i][j] - 1] = 1;   
                    }
                }
                //세팅되어있습니다

            }

        }
    }
}
