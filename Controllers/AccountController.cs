using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private ApplicationDbContext _context;
    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Account
    [HttpGet]
    public IEnumerable<Account> Get() //calling for all records
    {
        return _context.Accounts;
    }


    // GET api/Account/{id}
    [HttpGet("{name}")]
    public IEnumerable<Account> Get(string name) //calling for a record where Account Name = name
    {
        return _context.Accounts.Where(m => m.Account_Name == name).ToList();
    }

    // POST api/Account
    [HttpPost]
    public void Post([FromBody] AccountRequestModel accountModel)
    {
        //check if model is valid
        if (accountModel != null)
        {
            //auto generate ID
            var ID = "A" + (_context.Accounts.Count() + 1).ToString("0000");
            var model = new Account()
            {
                ID = ID,
                Account_Name = accountModel.Account,
                LoB = accountModel.Line_Of_Business
            };
            _context.Accounts.Add(model);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("There is something missing in data, it not completed");
        }

    }

    //Delete api/Account/{id}
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        var account = _context.Accounts.FirstOrDefault(m => m.ID == id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("This id is not found, please check Id first then try again");
        }
    }

}

