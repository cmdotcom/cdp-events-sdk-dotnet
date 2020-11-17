using System;

namespace CM.Cdp.Events.Sdk.Models
{
    public class ApiEvent
    {
        /// <summary>
        /// Every event has a corresponding event type that defines the message fields and can be viewed as a contract on how to interpret a field.
        /// Event types with their corresponding Id's found in the CDP Enviroment under the menu "Sources".
        /// </summary>
        public Guid EventTypeId { get; set; }

        /// <summary>
        /// A JSON serializable object that adheres the the format specified in the Event Type. Sample events could be a CRP Person, a website login event or any other
        /// event with user data.
        /// </summary>
        public object Event { get; set; }
    }
}
