using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public interface IDvdRepository
    {
        IEnumerable<Dvd> GetAllDVDs();
        Dvd GetDvdById(int id);
        void DvdDelete(int id);
        void DvdCreate(Dvd dvd);
        void DvdEdit(Dvd dvd);
        void SearchTerm(string category, string term);
    }
}