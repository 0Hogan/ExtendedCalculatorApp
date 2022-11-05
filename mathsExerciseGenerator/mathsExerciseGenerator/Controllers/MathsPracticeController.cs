using Microsoft.AspNetCore.Mvc;
using mathsExerciseGenerator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mathsExerciseGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathsPracticeController : ControllerBase
    {
        /*
        // GET: api/<MathsPracticeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        // GET api/<MathsPracticeController>
        [HttpGet]
        public string Get()
        {
            MathsExerciseGenerator exerciseGenerator = new();
            return exerciseGenerator.GetMathsExercisesJsonString();
        }

        /*
        // POST api/<MathsPracticeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MathsPracticeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MathsPracticeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
