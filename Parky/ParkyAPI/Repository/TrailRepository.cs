using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _db;

        //Constructor
        public TrailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trails.Include(c => c.NationalPark).OrderBy(a => a.Name).ToList();
        }
        
        public ICollection<Trail> GetTrailsInNationalPark(int npId)
        {
            //Include allows us to join Trails and National Park
            return _db.Trails.Include(c => c.NationalPark).Where(c => c.NationalParkId == npId).ToList();         
        }

        public bool CreateTrail(Trail trail)
        {
            _db.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _db.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int trailId)
        {
            return _db.Trails.Include(c => c.NationalPark).FirstOrDefault(a => a.Id == trailId);
        }

        public bool TrailExists(string trailName)
        {
            return _db.NationalPark.Any(a => a.Name == trailName);
        }

        public bool TrailExists(int Id)
        {
            return _db.Trails.Any(a => a.Id == Id);
        }

        public bool UpdateTrail(Trail trail)
        {
            _db.Trails.Update(trail);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
