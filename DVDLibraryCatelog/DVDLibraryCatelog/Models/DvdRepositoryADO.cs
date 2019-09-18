using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public class DvdRepositoryADO : IDvdRepository
    {
        public void DvdCreate(Dvd dvd)
        {
            throw new NotImplementedException();
        }

        public void DvdDelete(int id)
        {
            throw new NotImplementedException();
        }

        public void DvdEdit(Dvd dvd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dvd> GetAllDVDs()
        {
            throw new NotImplementedException();
        }

        public Dvd GetDvdById(int id)
        {
            throw new NotImplementedException();
        }

        public void SearchTerm(string category, string term)
        {
            throw new NotImplementedException();
        }
    }
}