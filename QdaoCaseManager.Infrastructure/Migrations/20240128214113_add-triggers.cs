using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Diagnostics.Metrics;

#nullable disable

namespace QdaoCaseManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"CREATE TRIGGER [CaseDeleteTrigger]
            ON[Cases]
            AFTER DELETE
            AS
            BEGIN
            INSERT INTO CaseHistories(CaseId, ActionType, ActionTime)
            SELECT Id, 'DELETE', GETDATE()
            FROM deleted;
            END;");

            migrationBuilder.Sql($@"CREATE TRIGGER [CaseUpdateTrigger]
            ON[Cases]
            AFTER UPDATE
            AS
            BEGIN
            INSERT INTO CaseHistories(CaseId, ActionType, ActionTime)
            SELECT Id, 'UPDATE', GETDATE()
            FROM deleted;
            END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER [CaseDeleteTrigger]");
            migrationBuilder.Sql("DROP TRIGGER [CaseUpdateTrigger]");
        }
    }
}
