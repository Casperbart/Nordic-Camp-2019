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
        public User User { get; set; }
        
        /// <summary>
        /// The Activity that the user have signed up to 
        /// </summary>
        public Activity Activity { get; set; }
    }
}