//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CommonLibrary;
using RESTfulSignalRService.MessageBroadCaster;
using RESTfulSignalRService.Interfaces;

namespace RESTfulSignalRService.Controllers
{
    /// <summary>
    /// Message broadcaster ApiController class
    /// </summary>
    public class MessageBroadCastController : ApiController
    {
        #region Private Variable

        private IBroadCast _broadCast;

        #endregion

        #region Constructors

        /// <summary>
        /// Message broadcaster ApiController class
        /// </summary>
        /// <param name="broadCast">IBroadCast value</param>
        public MessageBroadCastController(IBroadCast broadCast)
        {
            _broadCast = broadCast;
        }

        #endregion

        #region Public API Methods

        /// <summary>
        /// BroadCast message
        /// </summary>
        /// <param name="message">Message value</param>
        /// <param name="eventName">EventName value</param>
        /// <returns>string message</returns>
        [HttpPost]
        public string BroadCast(string message, string eventName)
        {
            return ProcessMessageRequest(message, eventName);
        }

        /// <summary>
        /// BroadCast message with default EventName(onMessageListened)
        /// </summary>
        /// <param name="message">Message value</param>
        /// <returns>string message</returns>
        [HttpPost]
        public string BroadCast(string message)
        {
            return ProcessMessageRequest(message, EventNameEnum.ON_MESSAGE_LISTENED.EnumDescription());
        }

        /// <summary>
        /// BroadCast message
        /// </summary>
        /// <param name="messageRequest">MessageRequested value</param>
        /// <returns>string message</returns>
        [HttpPost]
        public string BroadCast(MessageRequest messageRequest)
        {
            string response = string.Empty;
            try
            {
                _broadCast.BroadCast(messageRequest);
                response = "Message successfully broadcasted !";
            }
            catch (Exception exception)
            {
                response = "Opps got error. ";
                response = string.Concat(response, "Excepion, Message : ", exception.Message);
            }
            return response;
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Process message request
        /// </summary>
        /// <param name="message">Message value</param>
        /// <param name="eventName">EventName value</param>
        /// <returns>string message</returns>
        private string ProcessMessageRequest(string message, string eventName)
        {
            string response = string.Empty;
            try
            {
                MessageRequest messageRequest = new MessageRequest()
                {
                    Message = message,
                    EventName = eventName.FindEnumFromDescription<EventNameEnum>()
                };
                _broadCast.BroadCast(messageRequest);
                response = "Message successfully broadcasted !";
            }
            catch (Exception exception)
            {
                response = "Opps got error. ";
                response = string.Concat(response, "Excepion, Message : ", exception.Message);
            }
            return response;
        }

        #endregion
    }
}
