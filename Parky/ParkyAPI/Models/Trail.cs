using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }

        [Required]
        public double Elevation { get; set; }
        public enum DifficultyType { Easy, Moderate, Difficult, Expert}

        //Variable to store difficulty type here
        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }
        /*
         -----------------------------WE JOIN TABLES HERE WITH PK AND FK----------------------------------
         */

        [ForeignKey("NationalParkId")]//What variable we want this to bind on
        public NationalPark NationalPark { get; set; }//The table it will bind to
        /*
        ---------------------------------------------------------------------------------------------------
        */
        public DateTime DateCreated { get; set; }//We wont expose this to our Dto's. We only have it on our Domain Model

    }
}
