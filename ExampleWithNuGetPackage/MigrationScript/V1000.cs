using FluentMigrator;
namespace ExampleWithNuGetPackage.MigrationScript
{
    [Migration(1000)]
    public class V1000 : Migration
    {

        public override void Down()
        {
            if (Schema.Table("Member").Exists())
            {
                Delete.Table("Member");
            }
        }

        public override void Up()
        {
            if (!Schema.Table("Member").Exists())
            {
                Create.Table("Member")
              .WithColumn("MemberId").AsInt32().PrimaryKey().Identity()
              .WithColumn("Name").AsString(50)
              .WithColumn("Address").AsString()
              .WithColumn("MobileNo").AsString(10);
            }

            Execute.EmbeddedScript("INSERTCOURSE.sql");
        }


    }
}