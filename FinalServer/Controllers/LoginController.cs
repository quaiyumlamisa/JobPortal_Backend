
using FinalServer.Data;
using FinalServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalServer.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        readonly ApplicationDbContext _applicationDbContext;
        private readonly ApplicationSettings _appSettings;
        public LoginController(ApplicationDbContext context, IOptions<ApplicationSettings> appSettings)
        {

            _applicationDbContext = context;
            _appSettings = appSettings.Value;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login employee)
        {
            if (employee.role == "Employer")
            {
                Employer emp = _applicationDbContext.employers.FirstOrDefault(e => e.Email == employee.email && e.Password == employee.password);
                if (emp != null)
                {

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserID",emp.EmployersId.ToString()),
                            new Claim("Name",emp. CompanyName.ToString()),
                            new Claim(ClaimTypes.Role,employee.role)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new { message = "Username or password is incorrent" });
                }


            }


            if (employee.role == "Jobseeker")
            {
                Jobseeker emp = _applicationDbContext.jobseekers.FirstOrDefault(e => e.Email == employee.email && e.Password == employee.password);
                if (emp != null)
                {

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserID",emp.JobSeekerId.ToString()),
                            new Claim("Name",emp. FullName.ToString()),
                            new Claim(ClaimTypes.Role,employee.role)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new { message = "Username or password is incorrent" });
                }


            }


            else if (employee.role == "Admin")
            {
                Admin job = _applicationDbContext.admin.FirstOrDefault(e => e.Email == employee.email && e.Password == employee.password);
                if (job != null)
                {

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserID",job.AdminId .ToString()),
                             
                          //  new Claim("Role",sign.role.ToString()),
                            new Claim(ClaimTypes.Role,employee.role)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new { message = "Username or password is incorrent" });
                }


            }

            else
            {
                return BadRequest("this is pure shit");
            }


        }
    }
}
