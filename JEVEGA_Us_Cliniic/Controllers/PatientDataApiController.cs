using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JEVEGA_Us_Cliniic.Controllers
{
    public class PatientDataApiController : ApiController
    {
        JEVEGA_UsDbEntities dbEnt = new JEVEGA_UsDbEntities();

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, dbEnt.PatientDatas.OrderBy(p => p.Lastname).ToList());
        } //-- return all ... 

        public HttpResponseMessage Get(string indexchar)
        {
            return Request.CreateResponse(HttpStatusCode.OK, dbEnt.PatientDatas.Where(p => p.Lastname.StartsWith(indexchar)).OrderBy(l => l.Lastname).ToList());

        } //-- return list using query string ... indexchar?X

        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, dbEnt.PatientDatas.Find(id));

        } //-- return single item by id ... 
    }
}
