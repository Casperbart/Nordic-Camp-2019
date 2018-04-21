using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    /// <summary>
    /// Contains information about a page on the website
    /// </summary>
    public class Page : BaseEntity<string>
    {
        /// <summary>
        /// Url for the page
        /// </summary>
        [NotMapped]
        public string Url { get => Id; set => Id = value.ToLower(); }

        /// <summary>
        /// Content on the page
        /// </summary>
        public string Content { get; set; }
    }
}
