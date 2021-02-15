using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
   public interface INationalParkRepository
    {
        //Get list of all National Parks
        ICollection<NationalPark> GetNationalParks();
        
        //Create a National Park
        bool CreateNationalPark(NationalPark nationalPark);

        //Delete a National Park
        bool DeleteNationalPark(NationalPark nationalPark);

        //Get 1 National Park
        NationalPark GetNationalPark(int nationalParkID);

        bool NationalParkExists(string parkName);

        bool NationalParkExists(int Id);

        bool UpdateNationalPark(NationalPark nationalPark);
        
        bool Save();
    }
}
