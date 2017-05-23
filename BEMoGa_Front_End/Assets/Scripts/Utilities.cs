using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Utilties used by the influx bemoga wrapper
///</summary>
namespace InfluxBemoga
{
    [System.Serializable]
    public class influxUtil
    {
        // Determine if we are going to print the results
        private bool usePrint = false;

        /// <summary>
        /// Set the usePrint to the parameter
        /// </summary>
        /// <param name="print"> bool object </param>
        public void setUsePrint(bool print)
        {
            this.usePrint = print;
        }

        /// <summary>
        /// Get if the print is active or not
        /// </summary>
        /// <returns>returns true if we shall print, false if not</returns>
        public bool isPrint()
        {
            return this.usePrint;
        }
    }
}
