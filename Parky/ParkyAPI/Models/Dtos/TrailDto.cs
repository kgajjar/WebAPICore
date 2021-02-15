using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkyAPI.Models.Trail;

namespace ParkyAPI.Models.Dtos
{
    public class TrailDto
    {
        //No need to specify [KEY] data annotation
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }

        [Required]
        public double Elevation { get; set; }

        //Variable to store difficulty type here
        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }
        /*
         -----------------------------WE JOIN TABLES HERE WITH PK AND FK----------------------------------
         */

        //Dont need FK ref
        //[ForeignKey("NationalParkId")]//What variable we want this to bind on
        public NationalParkDto NationalPark { get; set; }//The table it will bind to
        /*
        ---------------------------------------------------------------------------------------------------
        */


    }
}
