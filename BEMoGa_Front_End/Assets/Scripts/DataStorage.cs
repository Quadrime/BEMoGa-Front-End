using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DataStorage
{
    /// <summary>
    /// Class for storing data. Data persists troughout scenes.
    /// WARNING! All DataReceptionContainer functions (found at the bottom) must be updated in accordance with each and every project. Create more as necessary.
    /// </summary>
    public class DataStoring
    {
        //DataReceptionContainer dataContainer = new DataReceptionContainer();
        Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
        private static DataStoring instance;
        private static string serverURL = "http://localhost:8086";
        private static string _db = "test_db"; // Change this


        ///<summary>Get a reference to the Global Data object. There exists only one Global Data object and it lasts the entire duration of the application.</summary>
        ///<returns>Returns a reference to the Global Data object, if it doesn't exist already it will be created</returns>
        public static DataStoring Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataStoring();
                }
                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getServerURL()
        {
            return serverURL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getQueryURl()
        {
            var url = getServerURL() + "/query";
            return url;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getWriteURl()
        {
            var url = getServerURL()+"/write?db=" + getNameDB();
            return url;
        }

        /// <summary>
        ///  should not be used
        /// </summary>
        /// <returns></returns>
        public void setNameDB(string newName)
        {
            _db = newName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getNameDB()
        {
            return _db;
        }
    }
}
