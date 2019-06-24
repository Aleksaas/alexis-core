using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain.Model
{
    public class Company : BaseModel
    {
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }

    public class CompanyType : ObjectGraphType<Company>
    {
        public CompanyType()
        {
            Name = "Company";
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }

    public class CompanyQuery : ObjectGraphType
    {
        public CompanyQuery(DatabaseContext db)
        {
            Field<CompanyType>(
              "Company",
              arguments: new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id", Description = "The ID of the Author." }),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");
                  var company = db
              .Companies
              .FirstOrDefault(i => i.Id == id);
                  return company;
              });

            Field<ListGraphType<CompanyType>>(
              "Companies",
              resolve: context =>
              {
                  var companies = db.Companies;
                  return companies;
              });
        }
    }
}
