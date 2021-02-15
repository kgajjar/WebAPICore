using ParkyAPI.Data;
using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;

        //Constructor
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalPark.OrderBy(a => a.Name).ToList();
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalPark.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalPark.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkID)
        {
            return _db.NationalPark.First(a => a.Id == nationalParkID);
        }

        //Update National Park
        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalPark.Update(nationalPark);
            return Save();
        }

        //Save changes to DB
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool NationalParkExists(string parkName)
        {
            return _db.NationalPark.Any(a => a.Name == parkName);
        }

        public bool NationalParkExists(int Id)
        {
            return _db.NationalPark.Any(a => a.Id == Id);
        }
    }
}
