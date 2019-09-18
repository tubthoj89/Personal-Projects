using DVDLibraryCatelogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public class DvdRepositoryMock : IDvdRepository
    {
        private static List<Dvd> _dvds;

        static DvdRepositoryMock()
        {
            _dvds = new List<Dvd>()
            {
                new Dvd { dvdId = 0, title = "A Good Tale", realeaseYear = 2012, director = "Joe Smith", rating = "PG-13", notes = "This really is a great tale!" },

                new Dvd { dvdId = 1, title = "The Lion King", realeaseYear = 1994, director = "Jim", rating = "G", notes = "Great story!"}
            };
        }

        public void DvdCreate(Dvd dvd)
        {
            if (_dvds.Any())
            {
                dvd.dvdId = _dvds.Max(d => d.dvdId) + 1;
            }
            else
            {
                dvd.dvdId = 0;
            }
            _dvds.Add(dvd);
        }

        public void DvdDelete(int id)
        {
            _dvds.RemoveAll(d => d.dvdId == id);
        }

        public void DvdEdit(int id, Dvd dvd)
        {
            _dvds.RemoveAll(d => d.dvdId == dvd.dvdId);
            _dvds.Add(dvd);
        }

        public Dvd GetDvdById(int id)
        {
            return _dvds.FirstOrDefault(d => d.dvdId == id);
        }

        public IEnumerable<Dvd> GetAllDVDs()
        {
            return _dvds;
        }

        public IEnumerable<Dvd> SearchTerm(string category, string term)
        {
            IEnumerable<Dvd> found;

            switch (category)
            {
                case "title":
                    found = _dvds.Where(d => d.title == term);
                    return found;
                case "realeaseYear":
                    if (int.TryParse(term, out int year))
                    {
                        found = _dvds.Where(d => d.realeaseYear == year);
                        return found;
                    }
                    else
                    {
                        return null;
                    }
                case "director":
                    found = _dvds.Where(d => d.director == term);
                    return found;
                case "rating":
                    found = _dvds.Where(d => d.rating == term);
                    return found;
                default:
                    return null;
            }
        }
    }
}