using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
   public interface ITrailRepository
    {
        //Get list of all Trails
        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailsInNationalPark(int npId);
        
        //Create a Trail
        bool CreateTrail(Trail trail);

        //Delete a Trail
        bool DeleteTrail(Trail trail);

        //Get 1 Trail
        Trail GetTrail(int trailId);

        bool TrailExists(string trailName);

        bool TrailExists(int Id);

        bool UpdateTrail(Trail trail);
        
        bool Save();
    }
}
