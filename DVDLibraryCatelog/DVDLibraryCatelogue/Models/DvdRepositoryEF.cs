using Dapper;
using DVDLibraryCatelogue.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public class DvdRepositoryEF : IDvdRepository
    {
        public void DvdCreate(Dvd dvd)
        {
            var repo = new DVDLibraryEntities1();

            if (repo.Dvds.Any())
            {
                dvd.dvdId = repo.Dvds.Max(d => d.dvdId) + 1;
            }
            else
            {
                dvd.dvdId = 0;
            }
            repo.Dvds.Add(dvd);
            repo.SaveChanges();
        }

        public void DvdDelete(int dvdId)
        {
            var repo = new DVDLibraryEntities1();

            repo.Dvds.Remove(repo.Dvds.Single(d => d.dvdId == dvdId));
            repo.SaveChanges();
        }

        public void DvdEdit(int dvdId, Dvd dvd)
        {
            var repo = new DVDLibraryEntities1();

            repo.Dvds.Remove(repo.Dvds.Single(d => d.dvdId == dvdId));
            repo.Dvds.Add(dvd);
            repo.SaveChanges();
        }

        public IEnumerable<Dvd> GetAllDVDs()
        {
            var repo = new DVDLibraryEntities1();

            return repo.Dvds;
        }

        public Dvd GetDvdById(int dvdId)
        {
            var repo = new DVDLibraryEntities1();

            return repo.Dvds.FirstOrDefault(d => d.dvdId == dvdId);
        }

        public IEnumerable<Dvd> SearchTerm(string category, string term)
        {
            var repo = new DVDLibraryEntities1();

            IEnumerable<Dvd> found;

            switch (category)
            {
                case "title":
                    found = repo.Dvds.Where(d => d.title == term);
                    return found;
                case "realeaseYear":
                    if (int.TryParse(term, out int year))
                    {
                        found = repo.Dvds.Where(d => d.realeaseYear == year);
                        return found;
                    }
                    else
                    {
                        return null;
                    }
                case "director":
                    found = repo.Dvds.Where(d => d.director == term);
                    return found;
                case "rating":
                    found = repo.Dvds.Where(d => d.rating == term);
                    return found;
                default:
                    return null;
            }
        }
    }
}