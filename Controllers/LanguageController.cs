using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LanguageController : ControllerBase
{
    private ApplicationDbContext _context;
    public LanguageController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Language
    [HttpGet]
    public IEnumerable<Language> Get() //calling for all records
    {
        return _context.Languages;
    }


    // GET api/Language/{name}
    [HttpGet("{name}")]
    public Language Get(string name) //calling for a record where Language Name = name
    {
        return _context.Languages.First(m => m.Language_Name == name);
    }

    // POST api/Language
    [HttpPost]
    public void Post([FromBody] LanguageRequestModel langModel)
    {
        //check if model is valid
        if (langModel != null)
        {
            //auto generate ID
            var ID = "L" + (_context.Languages.Count() + 1).ToString("0000");
            var model = new Language()
            {
                ID = ID,
                Language_Name = langModel.Language,
                Level = langModel.Level
            };
            _context.Languages.Add(model);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("There is something missing in data, it'not completed");
        }

    }

    //Delete api/Language/{id}
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        var lang = _context.Languages.FirstOrDefault(m => m.ID == id);
        if (lang != null)
        {
            _context.Languages.Remove(lang);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("This id is not found, please check Id first then try again");
        }
    }

}

