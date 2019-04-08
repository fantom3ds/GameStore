using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameStore.Models
{
    public class Game
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public double Price { get; set; }

        public List<User> OrderedUsers { get; set; }
    }
}