using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPeaksSharp.Extensions
{
    public static class SoapExtensions
    {
        /// <summary>
        /// Converts DateTime into SOAP conform string format
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToSoapFormat(this DateTime time)
        {
            return time.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
