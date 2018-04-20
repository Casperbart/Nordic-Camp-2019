using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model.Users
{
    /// <summary>
    /// If anything happens to the user / camper the Guardian should be contacted
    /// </summary>
    public class Guardian
    {
        /// <summary>
        /// The unique userid used in the backend database
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The name which the user identifies it self with
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Also some places called the family name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        /// <summary>
        /// A electronic adress where we can send them mails electronicly
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// A number to the users phone s.t. we can call them if they do not anwser their mail
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Where the Guardian lives, including zip code, city,  street name, number, floor
        /// and left or right-hand-side
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// The Country where the Guardian is currently living
        /// </summary>
        [Required]
        public string Country { get; set; }
    }
}