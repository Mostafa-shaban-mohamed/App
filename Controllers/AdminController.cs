using System.Security.Cryptography;
using System.Text;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;

namespace App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private ApplicationDbContext _context;

    private IConfiguration _config;
    public AdminController(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    //Login Method 
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] LoginViewModel model)
    {
        //use salt key to hash password
        var user = _context.Admins.First(m => m.Email == model.Email);
        model.Password = Convert.ToBase64String(ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(model.Password), user.SaltKey));
        IActionResult response = Unauthorized();
        if (user != null && (user.Email == model.Email && user.Password == model.Password))
        {
            var tokenString = GenerateJSONWebToken(user);
            
            response = Ok(new { token = tokenString });
        }

        return response;
    }

    private string GenerateJSONWebToken(Admin userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email)
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          null,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // GET: api/<AdminController>
    [HttpGet]
    [Authorize]
    public IEnumerable<Admin> Get() //calling for all records
    {
        return _context.Admins;
    }


    // GET api/<AdminController>/{id}
    [HttpGet("{id}")]
    public Admin Get(int id) //calling for a record where ID = id
    {
        return _context.Admins.First(m => m.ID == id);
    }

    //method for hashing the password
    //Hashing methods ---------------------------------------------
    public static byte[] GenerateSalt()
    {
        byte[] salt = RandomNumberGenerator.GetBytes(32);
        return salt;
    }
    public static byte[] ComputeHMAC_SHA256(byte[] data, byte[] salt)
    {
        using (var hmac = new HMACSHA256(salt))
        {
            return hmac.ComputeHash(data);
        }
    }


    // POST api/<AdminController>
    [HttpPost]
    public void Post([FromBody] Admin model)
    {
        //check if model is valid
        if (model != null)
        {
            //create hashed password
            var salt = GenerateSalt();
            model.Password = Convert.ToBase64String(ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(model.Password), salt));
            model.SaltKey = salt;

            _context.Admins.Add(model);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("There is something missing in data, it'not completed");
        }

    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var admin = _context.Admins.FirstOrDefault(m => m.ID == id);
        if (admin != null)
        {
            _context.Admins.Remove(admin);
            _context.SaveChanges();
        }
        else
        {
            //print not found record message
            throw new Exception("This id is not found, please check Id first then try again");
        }
    }

}

