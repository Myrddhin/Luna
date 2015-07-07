using System;
using System.Linq;
using System.Web.Http;
using Luna.Cloud.Data.Meta;
using Luna.Model.Cloud.Meta;

namespace Luna.Cloud.Controllers
{
    public class BaseController : ApiController
    {
        public MetaDataContext MetaContext { get; set; }

        protected string UserToken
        {
            // get { return ClaimsPrincipal.Current.Identity.Name; }
            get { return "carpentier_yann@hotmail.fr"; }
        }

        protected bool ValidateRepository(Guid repositoryId)
        {
            return AvailableRepositories.Any(x => x.Id == repositoryId);
        }

        protected IQueryable<Repository> AvailableRepositories
        {
            get
            {
                var q = from r in MetaContext.Repositories.Include("Subscriptions.Users")
                        join s in MetaContext.Subscriptions on r.SubscriptionId equals s.Id
                        join u in MetaContext.Users on s.Id equals u.SubscriptionId
                        where u.AzureLogin.EndsWith(UserToken) && s.Closing == null
                        select r;

                return q;
            }
        }
    }
}