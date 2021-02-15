using Microsoft.EntityFrameworkCore;
using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /*We create table from Model. When we run add-migration command. We need to run update-database command after this.
        If this table exists it will be updated. If not it will be added to DB.*/

        public DbSet<NationalPark> NationalPark { get; set; }

        public DbSet<Trail> Trails { get; set; }

        public DbSet<User> Users { get; set; }

        /*TO PUSH THESE CHANGES TO DB, WE NEED TO ADD A MIGRATION
        PACKAGE MANAGER CONSOLE - add-migration namemigration here e.g
       1) Install Microsoft.EntityFramework.Core.Tools Nuget package
       2) add-migration addCategoryToDatabase
       3) Wont see error if account was created with individual user account identity
       4) Run update-database command to push migration script to DB to create appropriate tables*/
    }
}
