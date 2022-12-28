using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Http.Headers;

namespace App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private ApplicationDbContext _context;
    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet]
    public IEnumerable<User> Get() //calling for all records
    {
        return _context.Users;
    }


    // GET api/User/{id}
    [HttpGet("{id}")]
    public User Get(int id) //calling for a record where ID = id
    {
        return _context.Users.First(m => m.ID == id);
    }

    //Pie Chart
    [HttpGet("PieChart")]
    public IEnumerable<PieChart> PieChartMethod()
    {
        var users = _context.Users.ToList();
        var datapoints = new List<PieChart>();
        foreach (var lang in _context.Languages.ToList())
        {
            var data = new PieChart()
            {
                y = users.Where(x => x.Language == lang.Language_Name).Count(),
                label = lang.Language_Name
            };
            datapoints.Add(data);
        }
        return datapoints;
    }

    //Line Chart
    [HttpGet("LineChart")]
    public IEnumerable<LineChart> LineChartMethod()
    {
        var users = _context.Users.ToList();
        var datapoints = new List<LineChart>();
        var max = _context.Users.Max(x => x.Age);
        var min = _context.Users.Min(x => x.Age);
        for (int i = min; i <= max; i++)
        {
            datapoints.Add(new LineChart()
            {
                x = i,
                y = _context.Users.Where(x => x.Age == i).Count()
            });
        }
        return datapoints;
    }

    //csv converter
    [HttpGet("export")]
    public ActionResult export()
    {
        string constr = "Server=DESKTOP-MAT9FS5;Database=ApplicationDB;Trusted_Connection=True;MultipleActiveResultSets=True;";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    //DataTable dt = new DataTable();
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        //Build the CSV file data as a Comma separated string.
                        string csv = string.Empty;

                        foreach (DataColumn column in dt.Columns)
                        {
                            //Add the Header row for CSV file.
                            csv += column.ColumnName + ',';
                        }

                        //Add new line.
                        csv += "\r\n";

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Data rows.
                                csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                            }

                            //Add new line.
                            csv += "\r\n";
                        }

                        //Download the CSV file.

                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        writer.Write(csv);
                        writer.Flush();
                        stream.Position = 0;

                        return File(new System.Text.UTF8Encoding().GetBytes(csv), "APPLICATION/octet-stream", "Report123.csv");

                    }
                }
            }
        }
    }

    // POST api/User
    [HttpPost]
    [Authorize]
    public void Post([FromBody] UserViewModel model)
    {
        var dob = Convert.ToDateTime(model.DoB);
        //check if model is valid
        if (model != null)
        {
            var age = DateTime.Today.Year - dob.Year;
            if (age < 18)
            {
                throw new Exception("age must be equal or greater than 18");
            }
            var user = new User()
            {
                Name = model.Name,
                National_ID = model.National_ID,
                DoB = dob,
                Age = age,
                Account = model.Account,
                LoB = model.LoB,
                Language = model.Language,
                Level = model.Level
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("There is something missing in data, it not completed");
        }

    }

    //PUT api/User/{id}
    [HttpPut("{id}")]
    [Authorize]
    public void Put(int id, [FromBody] UserViewModel model)
    {
        var user = _context.Users.FirstOrDefault(s => s.ID == id);
        if (user != null && model != null)
        {
            var dob = Convert.ToDateTime(model.DoB);
            //age checking (equal or older than 18)
            var age = DateTime.Today.Year - dob.Year;
            if (age < 18)
            {
                throw new Exception("age must be equal or greater than 18");
            }
            //get throw properties and apply the change
            user.Name = model.Name;
            user.National_ID = model.National_ID;
            user.DoB = dob;
            user.Account = model.Account;
            user.LoB = model.LoB;
            user.Language = model.Language;
            user.Level = model.Level;
            //compare data in user reqiured to modify (user with this id) 
            _context.Entry<User>(user).CurrentValues.SetValues(user);
            _context.SaveChanges();
        }
    }

    //DELETE api/User/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public void Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(m => m.ID == id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("This id is not found, please check Id first then try again");
        }
    }

}

