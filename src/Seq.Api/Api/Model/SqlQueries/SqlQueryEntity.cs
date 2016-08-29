namespace Seq.Api.Model.SqlQueries
{
    public class SqlQueryEntity : Entity
    {
        public SqlQueryEntity()
        {
            Title = "New SQL Query";
            Sql = "";
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Sql { get; set; }

        public bool Show { get; set; }
    }
}