using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SVGsWebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Svg;

namespace SVGsWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SVGsController : Controller
    {
        private readonly SVGsContext _dbContext;
        private readonly IHostingEnvironment _environment;

        public SVGsController(SVGsContext dbContext, IHostingEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;

            // sample data
            if (_dbContext.SVGs.Count() == 0)
            {
                string svg;
                svg = System.IO.File.ReadAllText(System.IO.Path.Combine(environment.ContentRootPath, @"App_Data\Farm_fresh_milk_6072.svg"));
                _dbContext.SVGs.Add(new SVG() { Title = "Farm Fresh Milk", Specification = svg, PNG = SVG2Binary(svg) });

                svg = System.IO.File.ReadAllText(System.IO.Path.Combine(environment.ContentRootPath, @"App_Data\Dont_wait_the_time_will_never_be_just_right.svg"));
                _dbContext.SVGs.Add(new SVG() { Title = "Dont Wait, The Time Will Never Be Just Right", Specification = svg, PNG = SVG2Binary(svg) });

                _dbContext.SaveChanges();
            }
        }

        // GET api/svgs
        [HttpGet]
        public JsonResult Get()
        {
            var data = (from s in _dbContext.SVGs
                        select new
                        {
                            id = s.Id,
                            title = s.Title,
                            pngBase64 = Convert.ToBase64String(s.PNG)
                        }).ToList();

            return Json(data);
        }

        // GET api/svgs/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = (from s in _dbContext.SVGs
                        where s.Id == id
                        select new
                        {
                            id = s.Id,
                            title = s.Title,
                            specification = s.Specification,
                            pngBase64 = Convert.ToBase64String(s.PNG)
                        }).SingleOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Json(data);
        }

        // POST api/svgs
        [HttpPost]
        public IActionResult Post([FromBody]SVG svg)
        {
            svg.PNG = SVG2Binary(svg.Specification);
            _dbContext.SVGs.Add(svg);
            _dbContext.SaveChanges();

            return Content(svg.Id.ToString());
        }

        // PUT api/svgs
        [HttpPut]
        public IActionResult Put([FromBody]SVG svg)
        {
            SVG s = _dbContext.SVGs.Find(svg.Id);
            if (s == null)
            {
                return NotFound();
            }

            s.Title = svg.Title;
            s.Specification = svg.Specification;
            _dbContext.SaveChanges();

            return new NoContentResult();
        }

        // DELETE api/svgs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SVG s = _dbContext.SVGs.Find(id);
            if (s == null)
            {
                return NotFound();
            }

            _dbContext.SVGs.Remove(s);
            _dbContext.SaveChanges();

            return new NoContentResult();
        }

        private byte[] SVG2Binary(string svg)
        {
            System.IO.MemoryStream png = new System.IO.MemoryStream();
            var svgDocument = SvgDocument.FromSvg<SvgDocument>(svg);
            using (var smallBitmap = svgDocument.Draw())
            {
                using (System.Drawing.Bitmap bitmap = svgDocument.Draw())
                {
                    bitmap.Save(png, System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            return png.ToArray();
        }
    }
}
