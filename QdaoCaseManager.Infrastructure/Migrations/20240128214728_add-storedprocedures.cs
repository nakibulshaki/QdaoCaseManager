using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QdaoCaseManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addstoredprocedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"CREATE PROCEDURE [CreateNote]
                                    @Content nvarchar(max),
                                    @CaseId int
                                AS
                                BEGIN
                                    INSERT INTO [Notes] (Content, CaseId)
                                    VALUES (@Content, @CaseId)
                                END");   
            
            migrationBuilder.Sql($@"CREATE PROCEDURE [DeleteNote]
                                (
                                    @Id INT
                                )
                                AS
                                BEGIN
                                    SET NOCOUNT ON;

                                    DELETE FROM Notes
                                    WHERE Id = @Id;
                                END");

            migrationBuilder.Sql($@"CREATE PROCEDURE [dbo].[GetNoteById]
                                (
                                    @Id INT
                                )
                                AS
                                BEGIN
                                    SET NOCOUNT ON;

                                    SELECT
                                        Id,
                                        Content,
                                        CaseId
                                    FROM Notes
                                    WHERE Id = @Id;
                                END;"); 
            migrationBuilder.Sql($@"CREATE PROCEDURE [GetNotesWithPagination]
                                    (
                                      @CurrentPage INT,
                                      @PageSize INT,
                                      @SearchString NVARCHAR(MAX),
                                      @TotalCount INT OUTPUT
                                    )
                                    AS
                                    BEGIN
                                      SET NOCOUNT ON;

                                      DECLARE @Offset INT = (@CurrentPage - 1) * @PageSize;
                                      DECLARE @Extra INT;
                                      SELECT
                                        n.Id,
                                        n.Content,
                                        c.Tittle AS CaseTittle,
                                        n.Created,
                                        ROW_NUMBER() OVER (ORDER BY n.Id) AS RowNum
                                      INTO #TempResult
                                      FROM Notes n
                                      INNER JOIN Cases c ON n.CaseId = c.Id
                                      WHERE (@SearchString IS NULL OR n.Content LIKE '%' + @SearchString + '%');


                                      SELECT
                                        Id,
                                        Content,
                                        CaseTittle,
                                        Created
                                      FROM #TempResult
                                      WHERE RowNum > @Offset AND RowNum <= (@Offset + @PageSize);
  
                                      SELECT @Extra = COUNT(Id) FROM #TempResult;
                                      Set @TotalCount = @Extra;

                                      DROP TABLE #TempResult;
                                    END");

            migrationBuilder.Sql($@"CREATE PROCEDURE [UpdateNote]
                                (
                                    @Id INT,
                                    @Content NVARCHAR(MAX),
                                    @CaseId INT
                                )
                                AS
                                BEGIN
                                    SET NOCOUNT ON;

                                    UPDATE Notes
                                    SET
                                        Content = @Content,
                                        CaseId = @CaseId
                                    WHERE Id = @Id;
                                END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [CreateNote]");
            migrationBuilder.Sql("DROP PROCEDURE [DeleteNote]");
            migrationBuilder.Sql("DROP PROCEDURE [GetNoteById]");
            migrationBuilder.Sql("DROP PROCEDURE [GetNotesWithPagination]");
            migrationBuilder.Sql("DROP PROCEDURE [UpdateNote]");

        }
    }
}
