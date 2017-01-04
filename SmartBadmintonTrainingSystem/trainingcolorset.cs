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
        public int[] dataset;
        int[] checker;
        public int[][] generatedData;//times, one-based pole
        public int times;
        public int[] colorOrder;
        Random randSeed = new Random();
        /// <summary>
        /// 기본 생성자
        /// </summary>
        /// <param name="dataset"> 색상 순서가 저장된 배열</param>
        /// <param name="times"> 트레이닝 회차 </param>
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
                //임의의 숫자들로 채워진 것.
            }

        }
    }
}
