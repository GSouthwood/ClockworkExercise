using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clockwork.Web.Models;

namespace Clockwork.Web.DAL
{
    public interface ICurrentTimeQueryDAL
    {
        List<CurrentTimeQuery> GetCurrentTimeQuery();
    }
}