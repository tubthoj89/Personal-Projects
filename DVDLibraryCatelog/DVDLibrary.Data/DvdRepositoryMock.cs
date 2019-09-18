using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public class DvdRepositoryMock
    {
        private static List<DvdVM> _dvdvm;

        static DvdRepositoryMock()
        {
            new DvdVM
            {
                dvdId = 0, title = "A Good Tale", realeaseYear = 2012, director = "Joe Smith", rating = "PG-13", notes = "This really is a great tale!"
            },
            new DvdVM
            {
                dvdId = 1, title = "The Lion King", realeaseYear = 1994, director = "Jim", rating = "G", notes = "Great story!"
            }

        };
    }
}