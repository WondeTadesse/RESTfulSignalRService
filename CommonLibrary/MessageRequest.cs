//|---------------------------------------------------------------|
//|                     COMMON LIBRARY                            |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                     COMMON LIBRARY                            |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    /// <summary>
    /// Message Request class
    /// </summary>
    public class MessageRequest
    {

        /// <summary>
        /// Get or set Message value
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get or set EventName
        /// </summary>
        public EventNameEnum EventName { get; set;}

        /// <summary>
        /// Message Request class
        /// </summary>
        public MessageRequest()
        {

        }
    }
}
