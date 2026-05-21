using inventory.Services;
using Microsoft.AspNetCore.Mvc;
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




    [HttpDelete("Eliminar producto")]

    public IActionResult DeleteProduct([FromQuery] int id)
    {
        try
        {
            _productService.DeleteProductProcedure(id);
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

}