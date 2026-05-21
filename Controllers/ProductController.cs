using inventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;

namespace inventory.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private readonly IProductService _productService;
    public ProductController(IProductService productService) => _productService = productService;

    [HttpGet("GetAllProduct")]
    public IActionResult GetProduct()
    {
        DataTable categories = _productService.GetAllProductConsulta();
        string result = JsonConvert.SerializeObject(categories);
        return Ok(result);
    }


    [HttpGet("GetAllProductProcedure")]
    public IActionResult GetProductProcedure()
    {
        DataTable categories = _productService.GetAllProductProcedure();
        string result = JsonConvert.SerializeObject(categories);
        return Ok(result);
    }

    [HttpGet("GetProductByIdConsulta")]
    public IActionResult GetProductByID(int id)
    {
        try
        {
            DataTable categories = _productService.GetProductByIDConsulta(id);
            string result = JsonConvert.SerializeObject(categories);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpGet("GetProductByIdProcedure")]
    public IActionResult GetProductByIDProcedure(int Id)
    {
        try
        {
            DataTable categories = _productService.GetProductByIDProcedure(Id);
            string result = JsonConvert.SerializeObject(categories);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpPost("CrearProductProcedure")]

    public IActionResult InsertProduct([FromQuery] string name, [FromQuery] string productNumber, [FromQuery] string color, [FromQuery] int safetyStocklevel, [FromQuery] int reorderPoint, [FromQuery] decimal standardCost, [FromQuery] decimal listPrice, [FromQuery] decimal daysToManufacture, [FromQuery] DateTime sellStartDate)
    {
        try
        {
            DataTable category = _productService.CreateProductProcedure(name, productNumber, color, safetyStocklevel, reorderPoint, standardCost, listPrice, daysToManufacture, sellStartDate);
            string result = "Se creo correctamente el producto";
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpPost("CrearProductConsulta")]

    public IActionResult InsertProductConsulta([FromQuery] string name, [FromQuery] string productNumber, [FromQuery] string color, [FromQuery] int safetyStocklevel, [FromQuery] int reorderPoint, [FromQuery] decimal standardCost, [FromQuery] decimal listPrice, [FromQuery] decimal daysToManufacture, [FromQuery] DateTime sellStartDate)
    {
        try
        {
            DataTable category = _productService.CreateProductConsulta(name, productNumber, color, safetyStocklevel, reorderPoint, standardCost, listPrice, daysToManufacture, sellStartDate);
            string result = "Se creo correctamente el producto";
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }


    [HttpPatch("UpdateProductProcedure")]

    public IActionResult UpdateProduct([FromQuery] int id, [FromQuery] string name, [FromQuery] string productNumber, [FromQuery] string color, [FromQuery] int safetyStocklevel, [FromQuery] int reorderPoint, [FromQuery] decimal standardCost, [FromQuery] decimal listPrice, [FromQuery] decimal daysToManufacture, [FromQuery] DateTime sellStartDate)
    {
        try
        {
            _productService.UpdateProductProcedure(id, name, productNumber, color, safetyStocklevel, reorderPoint, standardCost, listPrice, daysToManufacture, sellStartDate);
            string result = "Se actualizo correctamente el producto";
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpDelete("Eliminar producto")]

    public IActionResult DeleteProduct([FromQuery] int id)
    {
        try
        {
            _productService.DeleeteProductProcedure(id);
            string result = "Se selimino correctamente el producto";
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }

    }



       [HttpGet("GetSellInterval")]
    public IActionResult GetSellInterval([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
    {
        try
        {
            DataTable sell = _productService.sellDays(fechaInicio, fechaFin);
            string result = JsonConvert.SerializeObject(sell);
            return Ok(result);
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpGet("GetSellWeek")]
    public IActionResult GetSellWeek([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
    {
        try
        {
            DataTable sell = _productService.sellWeenk(fechaInicio, fechaFin);
            string result = JsonConvert.SerializeObject(sell);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpGet("GetSellMonth")]
    public IActionResult GetSellMonth([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
    {
        try
        {
            DataTable sell = _productService.sellMonth(fechaInicio, fechaFin);
            string result = JsonConvert.SerializeObject(sell);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }


    }

    [HttpGet("TodosParaUno")]
    public IActionResult GetTodosParaUno([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin, [FromQuery] int opcion)
    {
        try
        {
            DataTable sell = _productService.todosParaUno(fechaInicio, fechaFin, opcion);
            string result = JsonConvert.SerializeObject(sell);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }

    }

    /**
    [HttpPost("InsertCategory")]
    public IActionResult InsertCategory([FromQuery] string name)
    {
        try
        {
            DataTable category = _productService.InsertCategory(name);
            string result = "Se creo correctamente la categoria";
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }

    }*/


    [HttpPost("InserCategoryConsulta")]
    public IActionResult InsertCategoryConsulta([FromQuery] string name)
    {
        try
        {
            DataTable category = _productService.InsertCategory(name);
            string result = "Se creo correctamente la categoria";
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpDelete("DeleteCategory")]
    public IActionResult DeleteCategory(int id)
    {
        try
        {
            DataTable category = _productService.DeleteCategory(id);
            string result = "Se elimino correctamente la categoria";
            return Ok(result);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpGet("GetCategory")]
    public IActionResult GetCategory(int id)
    {
        try
        {
            DataTable category = _productService.GetCategory(id);
            string result = JsonConvert.SerializeObject(category);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }

    [HttpPatch("UpdateCategory")]
    public IActionResult UpdateCategory([FromQuery] string name, [FromQuery] int id)
    {
        try
        {
            _productService.UpdateCategory(name, id);
            string result = "Se actualizo correctamente la categoria";
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }


    }
}