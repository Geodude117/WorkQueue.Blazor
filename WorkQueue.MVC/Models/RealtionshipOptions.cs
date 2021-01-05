using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkQueue.MVC.Models
{
    public static class RealtionshipOptions
    {
        public static IEnumerable<SelectListItem> CSURelationshipOptions()
        {
            yield return new SelectListItem { Text = "Customer", Value = "Customer" };
            yield return new SelectListItem { Text = "Authorised Third Party", Value = "Authorised Third Party" };
            yield return new SelectListItem { Text = "Unauthorised Third Party", Value = "Unauthorised Third Party" };
        }
    }
}
