using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAMM_3
{
    class Scheme
    {
        double _pi1, _pi2;
        Random rnd;
        public Int32 otkaz,
            w;

        public Scheme(double pi1, double pi2)
        {
            _pi1 = pi1;
            _pi2 = pi2;
            rnd = new Random();
        }

        public int[] Tact(int[] prevState)
        {
            int[] newState = new int[4];
            int pi1Prob = rnd.Next(100) + 1;//вероятность прохода заявки в pi1
            int pi2Prob = rnd.Next(100) + 1;//вероятность прохода заявки в pi2
            
            if ((pi1Prob > _pi1 * 100) && (prevState[2] == 1))
                newState[2] = 0;// заявка в первом канале выполнена
            else if (prevState[2] == 1)
                newState[2] = 1;
            
            if ((pi2Prob > _pi2 * 100) && (prevState[3] == 1))
                newState[3] = 0;// заявка во втором канале выполнена
            else if (prevState[3] == 1)
                newState[3] = 1;

            if (prevState[0] == 1 && prevState[1] == 1 && newState[2] == 1 && newState[3] == 1)
            {
                otkaz++;
            }
            
            switch (prevState[1]) //обработка заявок, находящихся в очереди
            {
                case 0:
                    break;
                case 1:
                    if (newState[2] == 0)
                        newState[2] = 1;
                    else
                    {
                        if (newState[3] == 0)
                            newState[3] = 1;
                        else
                            newState[1] = 1;
                    }
                    break;
                default:
                    break;
            }

            if (prevState[0] == 2)
            {
                newState[0] = 1;
            }
            else
            {
                if (prevState[0]==1)
                {
                    if (newState[1] != 1)
                    {
                        if (newState[2] == 0)
                        {
                            newState[2] = 1;
                        }
                        else if (newState[3] == 0)
                        {
                            newState[3] = 1;
                        }
                        else
                        {
                            newState[1] = 1;
                        }
                    }
                    newState[0] = 2;
                }
            }
            if (newState[1] == 1) w++;
            return newState;
        }
    }
}
