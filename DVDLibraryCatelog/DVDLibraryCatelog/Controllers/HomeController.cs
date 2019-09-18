using DVDLibraryCatelog.Factory;
using DVDLibraryCatelog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;


namespace DVDLibraryCatelog.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {
        IDvdRepository _dvdRepo = DVDLibraryFactory.Create();

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetById(int id)
        {
            Dvd found = _dvdRepo.GetDvdById(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            return Ok(_dvdRepo.GetAllDVDs());
        }
    }
}