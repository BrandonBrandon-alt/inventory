
using inventory.Context;
using System.Data;
using System.Data.SqlClient;

namespace inventory.Services;

public class ProductService : IProductService
{
    private readonly IDatabaseContext _context;
    public ProductService(IDatabaseContext context) => _context = context;

    public DataTable GetAllProductConsulta()
    {
        DataTable products = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("SELECT * FROM [Production].[Product]", connection as SqlConnection);
            query.SelectCommand.CommandTimeout = 90;
            DataSet dt = new();
            query.Fill(dt);
            return dt.Tables[0];
        });
        return products;
    }

    public DataTable GetAllProductProcedure()
    {
        DataTable products = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("dbo.SP_GetAllProduct", connection as SqlConnection);
            query.SelectCommand.CommandType = CommandType.StoredProcedure;
            query.SelectCommand.CommandTimeout = 90;
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;
        });
        return products;
    }

    public DataTable GetProductByIDConsulta(int Id)
    {
        DataTable products = _context.Execute(connection =>
        {
            SqlDataAdapter query = new($"SELECT * FROM [Production].[Product] WHERE ProductID = {Id}", connection as SqlConnection);
            query.SelectCommand.CommandTimeout = 90;
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;
        });

        return products;
    }

    public DataTable GetProductByIDProcedure(int Id)
    {
        DataTable products = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("dbo.SP_GetProductByID", connection as SqlConnection);
            query.SelectCommand.CommandType = CommandType.StoredProcedure;
            query.SelectCommand.Parameters.AddWithValue("@ProductID", Id);
            query.SelectCommand.CommandTimeout = 90;
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;
        });
        return products;
    }


    public DataTable CreateProductConsulta(string name, string productNumber, string color, int safetyStocklevel, int reorderPoint, decimal standardCost, decimal listPrice, decimal daysToManufacture, DateTime sellStartDate)
    {
        DataTable products = _context.Execute(connection =>
        {
            string sql = @"
            INSERT INTO [Production].[Product] (Name, ProductNumber, Color, SafetyStockLevel, ReorderPoint, StandardCost, ListPrice, DaysToManufacture, SellStartDate) 
            VALUES (@Name, @ProductNumber, @Color, @SafetyStockLevel, @ReorderPoint, @StandardCost, @ListPrice, @DaysToManufacture, @SellStartDate);
            SELECT SCOPE_IDENTITY() AS NewProductID;
            ";
            using (SqlCommand command = new SqlCommand(sql, connection as SqlConnection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@ProductNumber", productNumber);
                command.Parameters.AddWithValue("@Color", color);
                command.Parameters.AddWithValue("@SafetyStockLevel", safetyStocklevel);
                command.Parameters.AddWithValue("@ReorderPoint", reorderPoint);
                command.Parameters.AddWithValue("@StandardCost", standardCost);
                command.Parameters.AddWithValue("@ListPrice", listPrice);
                command.Parameters.AddWithValue("@DaysToManufacture", daysToManufacture);
                command.Parameters.AddWithValue("@SellStartDate", sellStartDate);
                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
        });
        return products;
    }


    public DataTable CreateProductProcedure(string name, string productNumber, string color, int safetyStocklevel, int reorderPoint, decimal standardCost, decimal listPrice, decimal daysToManufacture, DateTime sellStartDate)
    {
        DataTable products = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("dbo.usp_InsertProduct", connection as SqlConnection);
            query.SelectCommand.CommandType = CommandType.StoredProcedure;
            query.SelectCommand.Parameters.AddWithValue("@Name", name);
            query.SelectCommand.Parameters.AddWithValue("@ProductNumber", productNumber);
            query.SelectCommand.Parameters.AddWithValue("@Color", color);
            query.SelectCommand.Parameters.AddWithValue("@SafetyStockLevel", safetyStocklevel);
            query.SelectCommand.Parameters.AddWithValue("@ReorderPoint", reorderPoint);
            query.SelectCommand.Parameters.AddWithValue("@StandardCost", standardCost);
            query.SelectCommand.Parameters.AddWithValue("@ListPrice", listPrice);
            query.SelectCommand.Parameters.AddWithValue("@DaysToManufacture", daysToManufacture);
            query.SelectCommand.Parameters.AddWithValue("@SellStartDate", sellStartDate);
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;
        });
        return products;
    }

    public DataTable DeleteProductProcedure(int id)
    {
        DataTable product = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("dbo.SP_DeleteProduct", connection as SqlConnection);
            query.SelectCommand.CommandType = CommandType.StoredProcedure;
            query.SelectCommand.Parameters.AddWithValue("@ProductID", id);
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;
        });
        return product;
    }


    public DataTable sellDays(DateTime fechaInicio, DateTime fechaFinal)
    {
        DataTable sell = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("dbo.SP_IntervalDays", connection as SqlConnection);
            query.SelectCommand.CommandType = CommandType.StoredProcedure;
            query.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            query.SelectCommand.Parameters.AddWithValue("@FechaFinal", fechaFinal);
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;


        });
        return sell;
    }

    public DataTable sellWeenk(DateTime fechaInicio, DateTime fechaFinal)
    {
        DataTable sell = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("dbo.SP_IntervalWeek", connection as SqlConnection);
            query.SelectCommand.CommandType = CommandType.StoredProcedure;
            query.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            query.SelectCommand.Parameters.AddWithValue("@fechaFinal", fechaFinal);
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;

        });
        return sell;
    }
    public DataTable sellMonth(DateTime fechaInicio, DateTime fechaFinal)
    {
        DataTable sell = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("dbo.SP_IntervalMonth", connection as SqlConnection);
            query.SelectCommand.CommandType = CommandType.StoredProcedure;
            query.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            query.SelectCommand.Parameters.AddWithValue("@FechaFinal", fechaFinal);
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;

        });
        return sell;
    }

    public DataTable todosParaUno(DateTime fechaInicio, DateTime fechaFinal, int opcion)
    {
        DataTable sell = _context.Execute(connection =>
        {
            SqlDataAdapter query = new("dbo.SP_TodosParaUno", connection as SqlConnection);
            query.SelectCommand.CommandType = CommandType.StoredProcedure;
            query.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            query.SelectCommand.Parameters.AddWithValue("@FechaFinal", fechaFinal);
            query.SelectCommand.Parameters.AddWithValue("@Opcion", opcion);
            DataTable dt = new DataTable();
            query.Fill(dt);
            return dt;

        });
        return sell;
    }


}

public interface IProductService
{
    /* TRAER PRODUCTOS */
    public DataTable GetAllProductConsulta();

    public DataTable GetAllProductProcedure();

    public DataTable GetProductByIDConsulta(int Id);

    public DataTable GetProductByIDProcedure(int Id);

    /* CREAR PRODUCTOS */
    public DataTable CreateProductProcedure(string name, string productNumber, string color, int safetyStocklevel, int reorderPoint, decimal standardCost, decimal listPrice, decimal daysToManufacture, DateTime sellStartDate);

    public DataTable CreateProductConsulta(string name, string productNumber, string color, int safetyStocklevel, int reorderPoint, decimal standardCost, decimal listPrice, decimal daysToManufacture, DateTime sellStartDate);

    /* ACTUALIZAR PRODUCTOS */
    /* Eliminar producto*/
    public DataTable DeleteProductProcedure(int id);
    public DataTable sellDays(DateTime fechaInicio, DateTime fechaFinal);

    public DataTable sellWeenk(DateTime fechaInicio, DateTime fechaFinal);

    public DataTable sellMonth(DateTime fechaInicio, DateTime fechaFinal);

    public DataTable todosParaUno(DateTime fechaInicio, DateTime fechaFinal, int opcion);
}
