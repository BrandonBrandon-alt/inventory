
using inventory.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace inventory.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductControllerCate : ControllerBase
{

    private readonly IProductionServiceCate _productServiceCate;
    public ProductControllerCate(IProductionServiceCate productServiceCate) => _productServiceCate = productServiceCate;


    [HttpPost("InserCategoryConsulta")]
    public IActionResult InsertCategoryConsulta([FromQuery] string name)
    {
        try
        {
            DataTable category = _productServiceCate.InsertCategory(name);
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
            DataTable category = _productServiceCate.DeleteCategory(id);
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
            DataTable category = _productServiceCate.GetCategory(id);
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
            _productServiceCate.UpdateCategory(name, id);
            string result = "Se actualizo correctamente la categoria";
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }


    }

    [HttpGet("GetAllCategory")]
    public IActionResult GetAllCategory()
    {
        try
        {
            DataTable categories = _productServiceCate.GetAllCategory();
            string result = JsonConvert.SerializeObject(categories);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }
}