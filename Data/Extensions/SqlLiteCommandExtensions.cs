namespace Data.Extensions;

public static class SqlLiteCommandExtensions
{
    public static DataTable SelectQuery<T>(this T dbContext, string query) where T : ModelContext
    {
        var connection = (SqliteConnection)dbContext.Database.GetDbConnection();
        
        try
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            
            using var selectCommand = new SqliteCommand(query, connection);
            using var reader = selectCommand.ExecuteReader();
            
            var entries = new DataTable();
            if (reader.HasRows)
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var columnType = reader.GetFieldType(i);
                    var columnName = reader.GetName(i);
                    entries.Columns.Add(new DataColumn(columnName, columnType));
                }
            
            var rowIndex = 0;
            while (reader.Read())
            {
                var row = entries.NewRow();
                entries.Rows.Add(row);

                for (var columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++)
                    entries.Rows[rowIndex][columnIndex] = reader.GetValue(columnIndex);

                rowIndex++;
            }

            return entries;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            connection.Close();
        }
    }
}
