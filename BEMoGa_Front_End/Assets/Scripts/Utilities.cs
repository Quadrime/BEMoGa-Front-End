using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Various containers and helpers for data transmissions and receptions.
///May need to be updated for each project. Below is merely a suggestion.
///WARNING! DataReceptionContainer (found at the bottom) must be updated in accordance with each and every project. Create more as necessary.
///</summary>
namespace Utilities
{
    ///<summary>JSON container for errors</summary>
    /// See the sendData for example
    [System.Serializable]
    public class ErrorContainer
    {
        //Error type, example "Error"
        public string name = "Null";

        //code 401 example
        public int status = -1;

        //Error message
        public string message = "Null";

        //code 401 example
        public int statusCode = -1;

        //All caps code
        public string code = "Null";

        //Stack trace to js code line
        public string stack = "Null";
    }

   
}
