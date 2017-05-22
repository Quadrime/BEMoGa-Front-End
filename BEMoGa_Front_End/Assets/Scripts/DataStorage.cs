using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DataStorage
{
    /// <summary>
    /// Class for storing data. Data persists troughout scenes.
    /// </summary>
    public class DataStoring
    {
        Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
        private static DataStoring instance;

        // specific data for onecity. Consider moving to config file for other projects
        private string _serverURL = "http://localhost:8086";            // URL of the server
        private string _username  = "writer";                           // username for access the database
        private string _passowrd  = "1234pass";                         // password for access the database
        private string _db        = "test_db";                          // Name of the database


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
            return this._serverURL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getQueryURl()
        {
            var url = _serverURL + "/query?u="+_username+"&p="+_passowrd;
            return url;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getWriteURl()
        {
            var url = _serverURL+"/write?db="+_db + "&u="+_username+"&p="+_passowrd;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getUsername()
        {
            return this._username;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        public void setUsername(string username)
        {
            this._username = username;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getPassword()
        {
            return this._passowrd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        public void sePassword(string password)
        {
            this._passowrd = password;
        }
    }
}
