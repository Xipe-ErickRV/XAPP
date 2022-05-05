using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xapp.API.Data;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Xapp.Domain.DTOs;
using Xapp.Domain.Entities;
using Xapp.Domain.DTOs.Perfil;
using Xapp.API.Hash;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Xapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private IConfiguration _config { get; }

        private readonly DbService _db;

        public PerfilController(IConfiguration config, DbService db)
        {
            _config = config;
            _db = db;
        }

        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return BadRequest();

            return Ok(user);
        }
        [HttpGet("getPerfil")]
        public async Task<IActionResult> GetPerfil(string email)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .ThenInclude(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Email == email);

            var perfil = user.PerfilUser;

            if (user != null)
            {
                var output = new ApiResponse<Perfil>
                {
                    StatusCode = 200,
                    Message = "Perfil de usuario",
                    Result = perfil
                };
                return Ok(output);
            }
            else
            {
                var output = new ApiResponse<Perfil>
                {
                    StatusCode = 400,
                    Message = "Usuario no encontrado.",
                    Result = null
                };
                return BadRequest(output);
            };
        }
        [HttpGet("getSkills")]
        public async Task<IActionResult> GetSkills(string email)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .ThenInclude(x => x.Skills)
                .FirstOrDefaultAsync(m => m.Email == email);

            if (user != null)
            {
                var output = new ApiResponse<List<Skill>>
                {
                    StatusCode = 200,
                    Message = "Lista de skills",
                    Result = user.PerfilUser.Skills.ToList()
                };
                return Ok(output);
            } 
            else
            {
                var output = new ApiResponse<List<Skill>>
                {
                    StatusCode = 400,
                    Message = "Usuario no encontrado.",
                    Result = null
                };
                return BadRequest(output);
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInput dto)
        {
            Encrypt hash = new Encrypt();
            var user = await _db.Users
                .Include(m => m.PerfilUser)
                .FirstOrDefaultAsync(m => m.Email == dto.Email && m.Password == hash.EncryptPwd(dto.Password));
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings")["Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Email)

                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);
                user.Token = jwt;

                var output = new ApiResponse<User>
                {
                    StatusCode = 200,
                    Message = "Bienvenido a XAPP",
                    Result = user
                };
                return Ok(output);
            }
            else
            {
                var output = new ApiResponse<User>
                {
                     StatusCode = 400,
                     Message ="Verifica tus campos."
                };
                return BadRequest(output);
            } 
        }


        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser(UserInput dto)
        {
            Encrypt hash = new Encrypt();

            var user = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = hash.EncryptPwd(dto.Password),
                PerfilUser = new Perfil
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Area = dto.Area,
                    Bio = dto.Bio,
                    FechaCumple = dto.FechaCumple
                },
                WalletlUser = new Wallet(),
                Comments = new List<Comment>(),
                Posts = new List<Post>(),
                PTOs = new List<PTO>(),
                Roles = new List<Rol>()
            };
            user.CreateEntity();
            user.PerfilUser.CreateEntity();
            user.WalletlUser.CreateEntity();

            await _db.Users.AddAsync(user);
            await _db.Perfiles.AddAsync(user.PerfilUser);
            await _db.Wallets.AddAsync(user.WalletlUser);
            await _db.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("newSkill")]
        public async Task<IActionResult> NewSkill(string email, SkillInput dto)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .ThenInclude(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return BadRequest();

            var skill = new Skill()
            {
                User = user.UserId,
                Nombre = dto.Nombre,
                Nivel = dto.Nivel
            };
            skill.CreateEntity();
            user.PerfilUser.Skills.Add(skill);

            await _db.Skills.AddAsync(skill);
            await _db.SaveChangesAsync();

            if (user.PerfilUser.Skills != null)
            {
                var output = new ApiResponse<Perfil>
                {
                    StatusCode = 200,
                    Message = "Añadiste una nueva skill.",
                    Result = user.PerfilUser
                };
                return Ok(output);
            }
            else
            {
                var output = new ApiResponse<Perfil>
                {
                    StatusCode = 400,
                    Message = "Verifica tus campos."
                };
                return BadRequest(output);
            }

           

        }

        [HttpPatch("passwordChange")]
        public async Task<IActionResult> PasswordChange(string email, string old, string newP, string confirmP)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return NotFound();

            if (user.Password != old) return BadRequest();

            if (confirmP != newP) return BadRequest();

            user.Password = newP;
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("patchUsuario")]
        public async Task<IActionResult> PatchUsuario(string email, PatchUser dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return BadRequest();

            user.Username = dto.Username;
            //user.Password = dto.Password;
            user.Email = dto.Email;
           

            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("patchPerfil")]
        public async Task<IActionResult> PatchPerfil(string email, ProfileOutput dto)
        {
            var user = await _db.Users
                .Include(x=> x.PerfilUser)
                .ThenInclude(x => x.Skills)
                .FirstOrDefaultAsync(m => m.Email == email);

            if (user != null)
            {
                if(user.PerfilUser.UrlCv != null)
                {
                    dto.UrlCv = user.PerfilUser.UrlCv;
                }
                if (user.PerfilUser.UrlFoto != null)
                {
                    dto.UrlImage = user.PerfilUser.UrlFoto;
                }
                user.PerfilUser.MetodoEdit(dto);
               
                await _db.SaveChangesAsync();

                var output = new ApiResponse<Perfil>
                {
                    StatusCode = 200,
                    Message = "Editaste el perfil.",
                    Result = user.PerfilUser
                };
                return Ok(output);
            }
            else
            {
                var output = new ApiResponse<Perfil>
                {
                    StatusCode = 400,
                    Message = "Usuario no encontrado.",
                    Result = user.PerfilUser
                };
                return BadRequest(output);
            }
        }
        

        [HttpDelete("userDelete")]
        public async Task<IActionResult> UserDelete(string email)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .Include(x=>x.WalletlUser)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return BadRequest();

            user.Delete();
            user.PerfilUser.Delete();
            user.WalletlUser.Delete();
            //Execute every Delete() in child entities like PTO?

            await _db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpDelete("skillDelete")]
        public async Task<IActionResult> SkillDelete(int ID, string email)
        {
            var user = await _db.Users
                .Include(x=> x.PerfilUser)
                .ThenInclude(x=> x.Skills)
                .FirstOrDefaultAsync(x => x.Email == email);

            //validación bla bla
            // validación ...

            var skill = user.PerfilUser.Skills
                .Find(x => x.User == user.UserId && x.Id == ID);
            if (skill == null) return BadRequest();

            skill.Delete();
            await _db.SaveChangesAsync();

            if (user.PerfilUser.Skills != null)
            {
                var output = new ApiResponse<Skill>
                {
                    StatusCode = 200,
                    Message = "Eliminaste una skill.",
                    Result = skill
                };
                return Ok(output);
            }
            else
            {
                var output = new ApiResponse<Skill>
                {
                    StatusCode = 400,
                    Message = "Verifica tus campos."
                };
                return BadRequest(output);
            }
        }

        [HttpDelete("CvDelete")]
        public async Task<IActionResult> CvDelete(string email)
        {
            var user = await _db.Users
                .Include(x => x.PerfilUser)
                .FirstOrDefaultAsync(x => x.Email == email);


            user.PerfilUser.UrlCv = null;
            await _db.SaveChangesAsync();

            if (user.PerfilUser.Skills != null)
            {
                var output = new ApiResponse<Perfil>
                {
                    StatusCode = 200,
                    Message = "Eliminaste una skill.",
                    Result = user.PerfilUser
                };
                return Ok(output);
            }
            else
            {
                var output = new ApiResponse<Perfil>
                {
                    StatusCode = 400,
                    Message = "Verifica tus campos."
                };
                return BadRequest(output);
            }
        }

        [HttpPost("UploadResume")]
        public async Task<IActionResult> UploadResume()
        {
            var resume = Request.Form.Files["resume"];

            IFormFile file = resume;
            if (file != null)
            {
                var blobSection = _config.GetSection("BlobSettings");
                var connectionString = blobSection.GetSection("ConnectionString").Value;
                var sourceContainerName = blobSection.GetSection("Container").Value;
                var fileName = $"{file.FileName}";
                BlobServiceClient blobServiceClient = new(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(sourceContainerName);
                BlobHttpHeaders blobHttpHeader = new()
                {
                    ContentType = file.ContentType
                };
                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(file.OpenReadStream(), blobHttpHeader);
                string url = $"https://xipeappstorgae.blob.core.windows.net/xapp/{fileName}";
                var output = new ApiResponse<string>
                {
                    StatusCode = 200,
                    Message = "Resume uploaded",
                    Result = url
                };
                return Ok(output);
            }

            return Ok();
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage()
        {
            var photo = Request.Form.Files["photo"];

            IFormFile file = photo;
            if (file != null)
            {
                var blobSection = _config.GetSection("BlobSettings");
                var connectionString = blobSection.GetSection("ConnectionString").Value;
                var sourceContainerName = blobSection.GetSection("Container").Value;
                var fileName = $"{file.FileName}";
                BlobServiceClient blobServiceClient = new(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(sourceContainerName);
                BlobHttpHeaders blobHttpHeader = new()
                {
                    ContentType = file.ContentType
                };
                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(file.OpenReadStream(), blobHttpHeader);
                string url = $"https://xipeappstorgae.blob.core.windows.net/xapp/{fileName}";
                var output = new ApiResponse<string>
                {
                    StatusCode = 200,
                    Message = "Profile photo uploaded",
                    Result = url
                };
                return Ok(output);
            }

            return Ok();
        }
    }
}


