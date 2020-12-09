using FluentMigrator;
namespace ExampleWithNuGetPackage.MigrationScript
{
    [Migration(1001)]
    public class V1001 : Migration
    {

        public override void Down()
        {
            if (Schema.Table("Member1").Exists())
            {
                Delete.Table("Member1");
            }
        }

        public override void Up()
        {
            if (!Schema.Table("Member1").Exists())
            {
               Create.Table("Member1")
              .WithColumn("MemberId").AsInt32().PrimaryKey().Identity()
              .WithColumn("Name").AsString(50)
              .WithColumn("Address").AsString()
              .WithColumn("MobileNo").AsString(10);
            }            
        }

    }
}