using System.ComponentModel.DataAnnotations;

namespace Backend.Model.Users
{
    /// <summary>
    /// A event that is happning at the Camp for a smaller group of people.
    /// This could include: shopping trip to Rønne, Volly training, icecream teasting etc.
    /// </summary>        
    public class Activity
    {
        /// <summary>
        /// The id used in the backend database
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the activity
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the activity
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        /// <summary>
        /// Price for the activity in "øre"
        /// </summary>
        public int Price { get; set; }
    }
}