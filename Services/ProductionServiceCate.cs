using inventory.Context;
using System.Data;
using System.Data.SqlClient;


namespace inventory.Services;

public class ProductionServiceCate : IProductionServiceCate
{
    private readonly IDatabaseContext _context;
    public ProductionServiceCate(IDatabaseContext context) => _context = context;


      public DataTable GetCategory(int Id)
    {
        DataTable dt = new DataTable();
        _context.Execute(connection =>
        {
            String sql = @"SELECT * FROM [Production].[ProductCategory] WHERE ProductCategoryID = @ProductCategoryID;";
            using (SqlCommand command = new SqlCommand(sql, connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@ProductCategoryID", Id);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        });
        return dt;
    }


    public DataTable GetAllCategory()
    {
        DataTable dt = new DataTable();
        _context.Execute(connection =>
        {
            String sql = @"SELECT * FROM [Production].[ProductCategory];";
            using (SqlCommand command = new SqlCommand(sql, connection as SqlConnection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        });
        return dt;
    }

    public DataTable InsertCategory(string name)
    {
        DataTable dt = new DataTable();
        _context.Execute(connection =>
        {
            string sql = @"
            INSERT INTO [Production].[ProductCategory] (Name) 
            VALUES (@Name);
            ";

            using (SqlCommand command = new SqlCommand(sql, connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.ExecuteNonQuery();
            }
            return dt;
        });

        return dt;
    }

      public DataTable DeleteCategory(int Id)
    {
        DataTable dt = new DataTable();
        _context.Execute(connection =>
        {
            string sql = @"DELETE FROM [Production].[ProductCategory] WHERE ProductCategoryID = @ProductCategoryID;";
            using (SqlCommand command = new SqlCommand(sql, connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@ProductCategoryID", Id);
                command.ExecuteNonQuery();
            }
            return dt;
        });
        return dt;
    }

     public bool UpdateCategory(string name, int Id)
    {
        bool isUpdated = false;
        DataTable dt = new DataTable();
        _context.Execute(connection =>
        {
            string sql = @"UPDATE [Production].[ProductCategory] SET Name = @Name WHERE ProductCategoryID = @ProductCategoryID";
            using (SqlCommand command = new SqlCommand(sql, connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@ProductCategoryID", Id);
                int rowsAffected = command.ExecuteNonQuery();
                isUpdated = rowsAffected > 0;

            }
            return isUpdated;

        });
        return isUpdated;
    }



}



public interface IProductionServiceCate
{
    public DataTable GetAllCategory();
    public DataTable GetCategory(int Id);
    public DataTable InsertCategory(string name);
    public DataTable DeleteCategory(int Id);
    public bool UpdateCategory(string name, int Id);
}