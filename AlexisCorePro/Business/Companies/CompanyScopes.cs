namespace AlexisCorePro.Business.Companies
{
    public static class CompanyScopes
    {
        public class CompanyBasic
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class CompanyDto
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
