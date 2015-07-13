using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Luna.Cloud.Data.CRM;
using Luna.Model.Cloud.CRM;

namespace Luna.Cloud.Controllers
{
    public class TagsController : BaseController
    {
        public CRMContext DataContext { get; set; }

        // GET: api/Repositories
        //[ResponseType(typeof(Tag[]))]
        //public async Task<IHttpActionResult> GetAll(Guid repositoryId)
        //{
        //    if (ValidateRepository(repositoryId))
        //    {
        //        return Ok(await DataContext.Tags.Where(x => x.RepositoryId == repositoryId).ToListAsync());
        //    }

        //    return NotFound();
        //}

        [ResponseType(typeof(DateTime))]
        public async Task<IHttpActionResult> GetLastModified(Guid repositoryId)
        {
            if (ValidateRepository(repositoryId))
            {
                if (await DataContext.Tags.Where(x => x.RepositoryId == repositoryId).AnyAsync())
                {
                    return this.Ok(await DataContext.Tags.Where(x => x.RepositoryId == repositoryId).MaxAsync(x => x.LastUpdate));
                }
                else
                {
                    return this.Ok(DateTime.MinValue);
                }
            }
            else
            {
                return this.NotFound();
            }
        }

        [ResponseType(typeof(Tag[]))]
        public async Task<IHttpActionResult> Get(Guid repositoryId, [FromUri] DateTime date)
        {
            if (ValidateRepository(repositoryId))
            {
                return Ok(await DataContext.Tags.Where(x => x.RepositoryId == repositoryId && x.LastUpdate > date).ToListAsync());
            }

            return NotFound();
        }

        [ResponseType(typeof(Tag))]
        public async Task<IHttpActionResult> Put(Guid repositoryId, Tag item)
        {
            if (!ValidateRepository(repositoryId))
            {
                return NotFound();
            }

            var repo = await DataContext.Tags.FindAsync(item.Id);
            if (repo == null || repo.RepositoryId != repositoryId)
            {
                return NotFound();
            }

            if (item.Version == repo.Version)
            {
                item.Version = Guid.NewGuid();
                DataContext.Tags.Attach(item);
                await DataContext.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return Conflict();
            }
        }

        public async Task<IHttpActionResult> Delete(Guid repositoryId, Guid id)
        {
            if (!ValidateRepository(repositoryId))
            {
                return NotFound();
            }

            var repo = await DataContext.Tags.FindAsync(id);
            if (repo == null || repo.RepositoryId != repositoryId)
            {
                return NotFound();
            }

            DataContext.Tags.Remove(repo);
            await DataContext.SaveChangesAsync();

            return Ok();
        }
    }
}