using Api_Creation_LearningMigration.Context;
using Api_Creation_LearningMigration.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Creation_LearningMigration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet("GetRecords")]
        public async Task<ActionResult<IEnumerable<Product>>> GetRecords()
        {
            var result = await _context.Products.ToListAsync();
            return Ok(result); // Return records as JSON
        }
        [HttpPost]
        [Route("createRecord")]
        public async Task<ActionResult<Product>> CreateRecords([FromBody] Product Product)
        {
            try
            {
                if (Product == null)
                {
                    return BadRequest("Product is null");
                }

                _context.Products.Add(Product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRecords), new { id = Product.Id }, Product);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // [HttpDelete("deleteRecords")]
        [HttpDelete("deleteRecord/{id}")]
        public async Task<ActionResult> DeleteRecords(int id)
        {
            var result = await _context.Products.FindAsync(id);
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();
            
            return Ok("Deleted Succesfully");
        }

        [HttpPut]

        [Route("updateRecord/{id}")]
        public async Task<ActionResult> UpdateRecord(int id, [FromBody] Product record)
        {
            var existingRecord = await _context.Products.FindAsync(id);
            if (existingRecord == null)
            {
                return NotFound("No record found");
            }

            try
            {
                // Update properties
                existingRecord.ProductName = record.ProductName;
                existingRecord.ProductDispatchAddress = record.ProductDispatchAddress;
                existingRecord.Cost = record.Cost;
                existingRecord.Warranty = record.Warranty;
                

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }

            return NoContent(); // Return 204 No Content on successful update
        }
        
    }
}
