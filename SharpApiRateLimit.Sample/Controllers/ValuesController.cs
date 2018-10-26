using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SharpApiRateLimit.Sample.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [RateLimitByHeader("X-UserId", "1s", calls: 1)]

    public class ValuesController : ControllerBase {

        [HttpGet]
        [RateLimitByHeader("X-UserId", "15s", calls: 2)]
        public ActionResult<IEnumerable<string>> Get() {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        [RateLimitByHeader("X-UserId", "10m", calls: 100)]
        public ActionResult<string> Get(int id) {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value) {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
