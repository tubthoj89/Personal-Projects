using DVDLibraryCatelog.Controllers;
using DVDLibraryCatelog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
namespace DVDLibraryCatelog.Factory
{
    public class DVDLibraryFactory
    {
        public static IDvdRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            IDvdRepository dvdRepository;

            switch (mode)
            {
                case "EF":
                    return dvdRepository = new DvdRepositoryEF();
                case "ADO":
                    return dvdRepository = new DvdRepositoryADO();
                case "Mock":
                    return dvdRepository = new DvdRepositoryMock();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}