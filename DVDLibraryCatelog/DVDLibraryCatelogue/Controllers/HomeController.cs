using DVDLibraryCatelog.Factory;
using DVDLibraryCatelog.Models;
using DVDLibraryCatelogue.Models;
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

        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddDVD(Dvd dvd)
        {
            _dvdRepo.DvdCreate(dvd);

            return Created($"dvd/{dvd.dvdId}", dvd);
        }

        [Route("dvds/{category}/{term}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByCategory(string category, string term)
        {
            IEnumerable<Dvd> found =_dvdRepo.SearchTerm(category, term);

            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void UpdateDvd(int id, Dvd dvd)
        {
            _dvdRepo.DvdEdit(id, dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void DeleteDvd(int id)
        {
            _dvdRepo.DvdDelete(id);
        }
    }
}