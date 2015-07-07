using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Luna.Model.Cloud.CRM;

namespace Luna.Cloud.Controllers
{
    public class TagsController : BaseController
    {
        // GET: api/Repositories
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> Get(Guid repositoryId)
        {
            return Ok(repositoryId.ToString() + " OK");
        }

        public async Task<IHttpActionResult> Get(Guid repositoryId, DateTime lastModified)
        {
            throw new Exception();
        }

        public async Task<IHttpActionResult> Put(Guid repositoryId, Tag item)
        {
            throw new Exception();
        }

        public async Task<IHttpActionResult> Delete(Guid repositoryId, Guid id)
        {
            throw new Exception();
        }
    }
}