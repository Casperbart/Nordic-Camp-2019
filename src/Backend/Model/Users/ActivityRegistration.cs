using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model.Users
{
    /// <summary>
    /// Here the link between the User and the chosen Activity is made.
    /// </summary>
    public class ActivityRegistration
    {
        /// <summary>
        /// The user that have signed up to the Activity
        /// </summary>
        [Required, ForeignKey(nameof(UserId))]
        public User User { get; set; }
        
        /// <summary>
        /// The Activity that the user have signed up to 
        /// </summary>
        [Required, ForeignKey(nameof(ActivityId))]
        public Activity Activity { get; set; }

        /// <summary>
        /// The userid for the User which have signed up to the Activity
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The activity id that the user have signed up to
        /// </summary>
        public int ActivityId { get; set; }
    }
}