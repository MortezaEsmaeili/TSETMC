using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Mapper
{
    public static class IncomingMapper
    {
        public static void ToAnalizing(this string[] message, double[][] matrix, long index)
        {
            int messageLength = message.Length;

            switch (messageLength)
            {
                case 8:
                    Length8(message, index, matrix);
                    break;

                case 10:
                    Length10(message, index, matrix);
                    break;

                default:
                    break;
            }
        }

        private static void Length8(string[] message, long index, double[][] matrix)
        {
            try
            {
                if (message[1] != "1")
                    return;

                if (double.TryParse(message[4], out double Bbp))
                    matrix[index][5] = Bbp;

                if (double.TryParse(message[5], out double Bsp))
                    matrix[index][3] = Bsp;

                if (double.TryParse(message[6], out double Bbq))
                    matrix[index][4] = Bbq;

                if (double.TryParse(message[7], out double Bsq))
                    matrix[index][2] = Bsq;
            }
            catch (Exception) { }
        }

        private static void Length10(string[] message, long index, double[][] matrix)
        {
            try
            {
                if (double.TryParse(message[3], out double Cp))
                    matrix[index][10] = Cp;

                if (double.TryParse(message[4], out double Ltp))
                    matrix[index][8] = Ltp;

                if (double.TryParse(message[5], out double Nt))
                    matrix[index][6] = Nt;

                if (double.TryParse(message[6], out double Nst))
                    matrix[index][7] = Nst;

                if (double.TryParse(message[7], out double Tv))
                    matrix[index][1] = Tv;
            }
            catch (Exception) { }
        }
    }
}
