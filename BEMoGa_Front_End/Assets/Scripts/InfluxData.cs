using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace InfluxBemoga
{
    /// <summary>
    /// Class for storing data. Data persists troughout scenes.
    /// </summary>
    public class InfluxData
    {
        Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
        private static InfluxData instance;

        // specific data for onecity. Consider moving to config file for other projects
        // This data need to change to be able to work on your specific project. Consider Influx to have an
        // instance of Influx data. And all of these options be available in there. 
        // And on creation of Influx object, serverurl and database name is required. 

        private string _serverURL = "http://localhost:8086";            // URL of the server
        private string _username  = "writer";                           // username for access the database
        private string _passowrd  = "1234pass";                         // password for access the database
        private string _db        = "test_db";                          // Name of the database


        ///<summary>Get a reference to the Global Data object. There exists only one Global Data object and it lasts the entire duration of the application.</summary>
        ///<returns>Returns a reference to the Global Data object, if it doesn't exist already it will be created</returns>
        public static InfluxData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InfluxData();
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
            var url = this._serverURL + "/query?u="+_username+"&p="+_passowrd;
            return url;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getWriteURl()
        {
            var url = this._serverURL+"/write?db="+_db + "&u="+_username+"&p="+_passowrd;
            return url;
        }

        /// <summary>
        ///  should not be used
        /// </summary>
        /// <returns></returns>
        public void setNameDB(string newName)
        {
            this._db = newName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getNameDB()
        {
            return this._db;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            var str = "database: " + this._db;
            str += "\nserverURL: " + this._serverURL;
            str += "\nusername: " + this._username;
            str += "\npassword: " + this._passowrd;
            str += "\nwriteURL: " + this.getWriteURl();
            str += "\nqueryURL: " + this.getQueryURl();
            return str;
        }
    }
}
