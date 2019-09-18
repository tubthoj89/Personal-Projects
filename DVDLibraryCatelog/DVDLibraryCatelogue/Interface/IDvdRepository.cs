using DVDLibraryCatelogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public interface IDvdRepository
    {
        IEnumerable<Dvd> GetAllDVDs();
        Dvd GetDvdById(int dvdid);
        void DvdDelete(int dvdid);
        void DvdCreate(Dvd dvd);
        void DvdEdit(int id, Dvd dvd);
        IEnumerable<Dvd> SearchTerm(string category, string term);
    }
}