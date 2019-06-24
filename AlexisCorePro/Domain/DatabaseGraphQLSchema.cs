using AlexisCorePro.Domain.Model;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain
{
    public class DatabaseGraphQLSchema : Schema
    {
        public DatabaseGraphQLSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CompanyType>();
        }
    }
}
