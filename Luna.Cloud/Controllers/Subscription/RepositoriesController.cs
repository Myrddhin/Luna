using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Luna.Model.Cloud.Meta;

namespace Luna.Cloud.Controllers
{
    public class RepositoriesController : BaseController
    {
        // GET: api/Repositories
        [ResponseType(typeof(IEnumerable<Repository>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await AvailableRepositories.ToListAsync();
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Repositories/5
        [ResponseType(typeof(Repository))]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var repository = await AvailableRepositories.FirstAsync(x => x.Id == id);
            if (repository == null)
            {
                return NotFound();
            }

            return Ok(repository);
        }

        // GET: api/Repositories/5
        [ResponseType(typeof(IEnumerable<Repository>))]
        public async Task<IHttpActionResult> GetLast(DateTime date)
        {
            return Ok(await AvailableRepositories.Where(x => x.LastUpdate > date).ToListAsync());
        }   /*

        /*

        // PUT: api/Repositories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRepository(Guid id, Model.Storage.Repository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != repository.Id)
            {
                return BadRequest();
            }

            db.Entry(repository).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepositoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Repositories
        [ResponseType(typeof(Model.Storage.Repository))]
        public async Task<IHttpActionResult> PostRepository(Model.Storage.Repository repository)
        {
            db.Repositories.Add(repository);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RepositoryExists(repository.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = repository.Id }, repository);
        }

        // DELETE: api/Repositories/5
        [ResponseType(typeof(Repository))]
        public async Task<IHttpActionResult> DeleteRepository(Guid id)
        {
            Repository repository = await db.Repositories.FindAsync(id);
            if (repository == null)
            {
                return NotFound();
            }

            db.Repositories.Remove(repository);
            await db.SaveChangesAsync();

            return Ok(repository);
        }
         *
         * */
    }
}