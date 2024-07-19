using DemoBTL.Application.Handle.HandleEmail;
using DemoBTL.Application.InterfaceService;
using DemoBTL.Application.Payload.Mapper;
using DemoBTL.Application.Payload.RequestModels.UserResquest;
using DemoBTL.Application.Payload.ResponeModels.DataUser;
using DemoBTL.Application.Payload.Respones;
using DemoBTL.Domain.Entity;
using DemoBTL.Domain.Entity.Enumerates;
using DemoBTL.Domain.Entity.Users.Function;
using DemoBTL.Domain.InterfacReponsitories;
using DemoBTL.Domain.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Bcrynet = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoBTL.Application.Handle.HandleFile;
using MimeKit;
using DemoBTL.Domain.Entity.Cerificate.Detail;
using DemoBTL.Domain.Entity.StutyStudent.Course.Detail;
using System.ComponentModel.DataAnnotations;


namespace DemoBTL.Application.ImplemenService
{
    public class AuServices : IAuService
    {
        private readonly IBaseRepository<User> _BaseUserRepository;
        private readonly UserConverter _userConverter;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IEmailServices _emailServices;
        private readonly IBaseRepository<ConfirmEmail> _confirmEmailRepository;
        private readonly IBaseRepository<Permission> _BasePermissionRepository;
        private readonly IBaseRepository<Role> _BaseRoleRepositoty;
        private readonly IBaseRepository<RefreshToken> _BaseRefreshTokeRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<Certificate> _BaseCertificateRepository;
        private readonly IBaseRepository<CerificateType> _BaseCertificateTypeRepository;
        private readonly IBaseRepository<Course> _BaseCouserRepository;
        private readonly IBaseRepository<CourseSubject> _BaseCSRepository;
        private readonly IBaseRepository<Subject> _BaseSubjectRepository;
        public AuServices() { }
        public AuServices(IBaseRepository<User> baseUserRepository, UserConverter userConverter, IConfiguration configuration, 
            IUserRepository userRepository, IEmailServices emailServices, IBaseRepository<ConfirmEmail> confirmEmailRepository, 
            IBaseRepository<Permission> basepermissionrepository, IBaseRepository<Role> baseRoleRepository, 
            IBaseRepository<RefreshToken> baseRefreshTokeRepository, IHttpContextAccessor ContextAccessor, IBaseRepository<CerificateType> baseCertificateTypeRepository,
            IBaseRepository<Certificate> baseCertificateRepository, IBaseRepository<Course> basecouserepository, IBaseRepository<CourseSubject> baseCSRepository,
            IBaseRepository<Subject> baseSubjectRepository) 
        {
            _BaseUserRepository = baseUserRepository;
            _userConverter = userConverter;
            _configuration = configuration;
            _userRepository = userRepository;
            _emailServices = emailServices;
            _confirmEmailRepository = confirmEmailRepository;
            _BasePermissionRepository = basepermissionrepository;
            _BaseRoleRepositoty = baseRoleRepository;
            _BaseRefreshTokeRepository = baseRefreshTokeRepository;
            _contextAccessor = ContextAccessor;
            _BaseCertificateRepository = baseCertificateRepository;
            _BaseCertificateTypeRepository = baseCertificateTypeRepository;
            _BaseCouserRepository = basecouserepository;
            _BaseCSRepository = baseCSRepository;
            _BaseSubjectRepository = baseSubjectRepository;
        }
        public async Task<ResponseOject<DataResponeUser>> Register(Request_Register request_Register)
        {
            try
            {
                if ((!VAlidateinput.isValiEmail(request_Register.Email)))
                {
                    return new ResponseOject<DataResponeUser>
                    {
                        Data = null,
                        Message = "Định dạng Email sai",
                        Status = StatusCodes.Status410Gone,
                    };
                }
                if (await _userRepository.GetUserByEmail(request_Register.Email) != null)
                {
                    return new ResponseOject<DataResponeUser>
                    {
                        Data = null,
                        Message = "Email đã có người đăng ký",
                        Status = StatusCodes.Status410Gone,
                    };
                }
                if (await _userRepository.GetUserByUsername(request_Register.Username) != null)
                {
                    return new ResponseOject<DataResponeUser>
                    {
                        Data = null,
                        Message = "Username đã có người đăng ký",
                        Status = StatusCodes.Status410Gone,
                    };
                }
                var user = new User
                {
                    Avatar = null,
                    IsActive = true,
                    CreateTime = DateTime.Now,
                    DateofBirth = null,
                    Fullname = request_Register.Fullname,
                    Username = request_Register.Username,
                    Password = Bcrynet.HashPassword(request_Register.Password),
                    Email = request_Register.Email,
                    UserStatus = ConstanEnum.EnumStatusUser.Unactive,
                };
                user = await _BaseUserRepository.CreateAsync(user);
                await _userRepository.AddrollToUserAsync(user, new List<string> { "Student" });
                ConfirmEmail confirm = new ConfirmEmail
                {
                    IsConfirm = false,
                    ConfirmCode = CrateCodeActive(),
                    Expirytime = DateTime.Now.AddMinutes(10),
                    UserId = user.Id,
                };
                confirm = await _confirmEmailRepository.CreateAsync(confirm);
                var message = new EmailMessage(new string[] { request_Register.Email }, "Nhan ma xac nhan", $"ma xac nhan: {confirm.ConfirmCode}");
                var responmessage = _emailServices.SendEmail(message);
                return new ResponseOject<DataResponeUser>
                {
                    Data = _userConverter.EntityToDTO(user),
                    Message = "Đăng ký tài khoản thành công vui lòng nhận mã và xác thực tài khoản tại email + thêm quyền mặc định",
                    Status = StatusCodes.Status200OK,
                };
            }
            catch (Exception ex)
            {
                return new ResponseOject<DataResponeUser>
                {
                    Data = null,
                    Message = "Erro" + ex.Message,
                    Status = StatusCodes.Status500InternalServerError,
                };
            }

        }
        #region 
        private string CrateCodeActive()
        {
            string str = "AA-Pc" + DateTime.Now.ToString();
            return str;
        }
        #endregion
        public async Task<string> ConfirmRegister(string confirmcod)
        {
            try
            {
                var code = await _confirmEmailRepository.GetAsync(x => x.ConfirmCode.ToLower().Equals(confirmcod.ToLower()));
                if (code == null)
                {
                    return $"Mã xác nhận không hợp lệ";
                }
                var user = await _BaseUserRepository.GetAsync(x => x.Id == code.UserId);
                if (code.Expirytime < DateTime.Now)
                {
                    return $"Mã xác nhận đã hết hạn";
                }
                user.UserStatus = ConstanEnum.EnumStatusUser.Active;
                code.IsConfirm = true;
                await _BaseUserRepository.UpdateAsync(user);
                await _confirmEmailRepository.UpdateAsync(code);
                return $"Xác nhận tài khoản thành công";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public async Task<ResponseOject<DataResponeLogin>> GetJwtTokenAsync(User user)
        {
            var permissions = await _BasePermissionRepository.GetAllAsync(x => x.UserId == user.Id);
            var roles = await _BaseRoleRepositoty.GetAllAsync();
            var authClaims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.Username.ToString()),
                new Claim("Email", user.Email.ToString()),
            };
            foreach (var permission in permissions)
            {
                foreach (var role in roles)
                {
                    if (role.Id == permission.RoleId)
                    {
                        authClaims.Add(new Claim("Permission", role.RoleName));
                    }
                }
            }
            var userRole = await _userRepository.GetRoleOfUserAsync(user);
            foreach (var item in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            var jwtToken = GetToken(authClaims);
            var refreshToken = GenerationRefeshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refeshTokenValidity);
            RefreshToken rf = new RefreshToken
            {
                ExpiryTime = DateTime.UtcNow.AddHours(refeshTokenValidity),
                UserId = user.Id,
                Token = refreshToken,
            };
            rf = await _BaseRefreshTokeRepository.CreateAsync(rf);
            return new ResponseOject<DataResponeLogin>
            {
                Status = StatusCodes.Status200OK,
                Message = "Tạo token thành công",
                Data = new DataResponeLogin
                {
                    AccessToke = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    RefeshToken = refreshToken,
                }
            };
        }

        public async Task<ResponseOject<DataResponeLogin>> Login(Resquest_Login request)
        {
            var user = await _BaseUserRepository.GetAsync(x => x.Username.Equals(request.Username));
            if (user == null)
            {
                return new ResponseOject<DataResponeLogin>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Username không tồn tại",
                    Data = null,
                };
            }
            if (user.UserStatus.ToString().Equals(ConstanEnum.EnumStatusUser.Unactive.ToString()))
            {
                return new ResponseOject<DataResponeLogin>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tài khoản chưa được xác thực",
                    Data = null,
                };
            }
            bool checkPass = Bcrynet.Verify(request.Password, user.Password);
            if (!checkPass)
            {
                return new ResponseOject<DataResponeLogin>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "mật khẩu không chính xác",
                    Data = null,
                };
            }
            return new ResponseOject<DataResponeLogin>
            {
                Status = StatusCodes.Status200OK,
                Message = "Đăng nhập thành công",
                Data = new DataResponeLogin
                {
                    AccessToke = GetJwtTokenAsync(user).Result.Data.AccessToke,
                    RefeshToken = GetJwtTokenAsync(user).Result.Data.RefeshToken
                },
            };
        }

        #region
        private JwtSecurityToken GetToken(List<Claim> authCalin)
        {
            var authSiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SercetKey"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int tokenValidityInHours);
            var expiritionUTC = DateTime.Now.AddHours(tokenValidityInHours);
            var token = new JwtSecurityToken
                (
                issuer: _configuration["JWT:ValiIssuer"],
                audience: _configuration["JWT:ValiAudience"],
                expires: expiritionUTC,
                claims: authCalin,
                signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        private string GenerationRefeshToken()
        {
            var ramdomnurber = new byte[64];
            var rage = RandomNumberGenerator.Create();
            rage.GetBytes(ramdomnurber);
            return Convert.ToBase64String(ramdomnurber);
        }
        #endregion
        public async Task<ResponseOject<DataResponeUser>> ChangePassword(int userId, Request_ChangePassword request_ChangePassword)
        {
            try
            {
                var user = await _BaseUserRepository.GetbyIdAsync(userId);
                bool checkpass = Bcrynet.Verify(request_ChangePassword.OladPassword, user.Password);
                if (!checkpass)
                {
                    return new ResponseOject<DataResponeUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không chính xác",
                        Data = null,
                    };
                }
                if (!request_ChangePassword.NewPassword.Equals(request_ChangePassword.ConfirmPassword))
                {
                    return new ResponseOject<DataResponeUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không trùng khớp",
                        Data = null,
                    };
                }

                user.Password = Bcrynet.HashPassword(request_ChangePassword.NewPassword);
                user.UpdateTime = DateTime.UtcNow;
                await _BaseUserRepository.UpdateAsync(user);
                return new ResponseOject<DataResponeUser>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật mật khẩu thành công",
                    Data = _userConverter.EntityToDTO(user),
                };
            }
            catch (Exception ex)
            {
                return new ResponseOject<DataResponeUser>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "không thể cập nhật mật khẩu",
                    Data = null,
                };
            }
        }


        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(email);
                if (user == null)
                {
                    return "Người dùng không tồn tại trong hệ thống";
                }
                //var listConfirmCode = await _confirmEmailRepository.GetAllAsync(x=>x.UserId == user.Id);
                //if (listConfirmCode.ToList().Count > 0)
                //{
                //    foreach (var abc in listConfirmCode)
                //    {
                //        await _confirmEmailRepository.DeleteByIdAsync(abc.Id);
                //    }
                //};
                ConfirmEmail confirm = new ConfirmEmail
                {
                    ConfirmCode = CrateCodeActive(),
                    Expirytime = DateTime.Now.AddMinutes(1),
                    UserId = user.Id,
                    IsConfirm = false,
                };
                confirm = await _confirmEmailRepository.CreateAsync(confirm);
                var sen1 = _emailServices.SendEmail(new EmailMessage
                {
                    Subject = "Nhan ma xac nhan",
                    Content = $"ma xac nhan: {confirm.ConfirmCode}",
                    To = new List<string> { user.Email}.Select(x=> new MailboxAddress("Email",x )).ToList()
                });
                return "Gửi mã xác nhận thành công vui lòng kiểm tra Email";
            }
            catch (Exception ex)
            {
                return "không thể thực hiện ForgotPassword" + ex.Message;
            }
        }

        public async Task<string> ConfirmCreatePassword(Request_CreateNewPassword request_CreateNewPassword)
        {
            try
            {
                var confirmemail = await _confirmEmailRepository.GetAsync(x => x.ConfirmCode.Equals(request_CreateNewPassword.ConfirmCode));
                if (confirmemail == null)
                {
                    return "Mã xác nhận không hợp lệ";
                }
                if (confirmemail.Expirytime < DateTime.Now)
                {
                    return "Mã xác nhận đã hết hạn";
                }
                if (!request_CreateNewPassword.NewPassword.Equals(request_CreateNewPassword.ConfirmPassword))
                {
                    return "Mật khẩu không trùng khớp";
                }
                var user = await _BaseUserRepository.GetAsync(x => x.Id == confirmemail.UserId);
                user.Password = Bcrynet.HashPassword(request_CreateNewPassword.NewPassword);
                user.UpdateTime = DateTime.Now;
                await _BaseUserRepository.UpdateAsync(user);
                return "Cập nhật Password thành công";
            }
            catch (Exception ex)
            {
                return "Không thể thực hiện tạo mật khẩu mới" + ex.Message;
            }
        }

        public async Task<string> Addrole(int userId, List<string> roles)
        {
            var curendUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!curendUser.Identity.IsAuthenticated)
                {
                    return "người dùng chưa được xác thực (Token của người dùng bị lỗi)";
                }
                if (!curendUser.IsInRole("Admin"))
                {
                    return "bạn phải là Admin mới có quyền thực hiện chức năng này";
                }
                var user = await _BaseUserRepository.GetbyIdAsync(userId);
                if (user == null)
                {
                    return "không tìm thấy người dùng";
                }
                await _userRepository.AddrollToUserAsync(user, roles);
                return "Thêm quyền cho người dùng thành công";

            }
            catch (Exception ex)
            {
                return " không thể thực hiện chức năng thêm role cho người dùng" + ex.Message;
            }
        }

        public async Task<string> Deleterole(int userId, List<string> roles)
        {
            var curendUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!curendUser.Identity.IsAuthenticated)
                {
                    return "người dùng chưa được xác thực (Token của người dùng bị lỗi)";
                }
                if (!curendUser.IsInRole("Admin"))
                {
                    return "bạn phải là Admin mới có quyền thực hiện chức năng này";
                }
                var user = await _BaseUserRepository.GetbyIdAsync(userId);
                if (user == null)
                {
                    return "người dùng không tồn tại";
                }
                await _userRepository.DeleteRoleAsync(user, roles);
                return "Xoá quyền thành công";

            }
            catch (Exception ex)
            {
                return " không thể thực hiện chức năng xoá role cho người dùng" + ex.Message;
            }
        }




        public async Task<ResponseOject<DataResponeUser>> updateuser(ResquestUpdateUser updateUser, int userid)
        {
            var curreenUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!curreenUser.Identity.IsAuthenticated)
                {
                    return new ResponseOject<DataResponeUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "người dùng chưa được xác thực",
                        Data = null
                    };
                }
                var Id = curreenUser.FindFirst("Id").Value;
                var userItem = await _BaseUserRepository.GetbyIdAsync(userid);
                if (int.Parse(Id) != userid && int.Parse(Id) != userItem.Id)
                {
                    return new ResponseOject<DataResponeUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }
               
                userItem.Avatar = await HandleeUploadFile.WriteFile(updateUser.Avatar);
                userItem.DateofBirth = updateUser.DateofBirth;
                userItem.Email = updateUser.Email;
                userItem.Address = updateUser.Address;
                userItem.Fullname = userItem.Fullname;
                await _BaseUserRepository.UpdateAsync(userItem);
                return new ResponseOject<DataResponeUser>
                {
                    Message = "cập nhật thông tin thành công",
                    Status = StatusCodes.Status200OK,
                    Data = _userConverter.EntityToDTO(userItem)
                };
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<string> AddCertificate(int userId, Request_Certificate request_Certificate)
        {
            var currenUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currenUser.Identity.IsAuthenticated)
                {
                    return "Người dùng chưa xác thực";
                }
                var id = currenUser.FindFirst("Id").Value;
                var userfind = await _BaseUserRepository.GetbyIdAsync(userId);
                if (userfind.Id != int.Parse(id) && int.Parse(id) != userId) 
                {
                    return "Id bạn nhập vào hoặc id người dùng không trùng nhau";
                }
                Certificate certificate = new Certificate 
                {
                    Name = request_Certificate.Name,
                    Image = await HandleeUploadFile.WriteFile(request_Certificate.Image),
                    Description = request_Certificate.Description,
                    userId = userfind.Id,
                };
                certificate = await _BaseCertificateRepository.CreateAsync(certificate);
                await _userRepository.AddrollToUserAsync(userfind, new List<string> {"Teacher"});
                return "Cập nhật chứng chỉ thành công cần cập nhật thêm loại chứng chỉ";
            }
            catch(Exception ex)
            {
                return "Thêm chứng chỉ không thành công";
            }
        }

        public async Task<string> AddCertificateOfType(int Userid, Resquest_CertificateOfType resquest_CertificateOfType)
        {
            var currenuser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currenuser.Identity.IsAuthenticated)
                {
                    return "Người dùng chưa xác thực";
                }
                var Id = currenuser.FindFirst("Id").Value;
                var Userfind = await _BaseUserRepository.GetbyIdAsync(Userid);
                if (Userfind.Id != int.Parse(Id) && Userid != int.Parse(Id))
                {
                    return "Id bạn nhập vào hoặc id người dùng không trùng nhau";
                }
                var CertificateBase = await _BaseCertificateRepository.GetbyIdAsync(resquest_CertificateOfType.CertificateId);
                if (CertificateBase == null)
                {
                    return "Cập nhật Certificate rồi mới cập nhật Type";
                }
                CerificateType certificatetype = new CerificateType
                {
                    CertificateId = resquest_CertificateOfType.CertificateId,
                    Name = resquest_CertificateOfType.Name
                };
                certificatetype = await _BaseCertificateTypeRepository.CreateAsync(certificatetype);
                return "Cập nhật CertificateType thành công";
            }
            catch(Exception ex)
            {
                return "Thêm chứng chỉ không thành công";
            }
        }

        public async Task<string> Addcourse(int userId, Request_Couse request_Couse)
        {
            var currenuser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currenuser.Identity.IsAuthenticated)
                {
                    return "Người dùng chưa xác thực";
                }
                var Id = currenuser.FindFirst("Id").Value;
                var userfind = await _BaseUserRepository.GetbyIdAsync(userId);
                if (userfind.Id != int.Parse(Id) && userId != int.Parse(Id))
                {
                    return "Id bạn nhập vào hoặc id người dùng không trùng nhau";
                }
                var CertificateOfUser = await _BaseCertificateRepository.GetbyIdAsync(userId);
                if (CertificateOfUser == null)
                {
                    return "Người dùng chưa có chứng chỉ";
                }
                Course course = new Course
                {
                    Name = request_Couse.Name,
                    Introduce = request_Couse.Introduce,
                    ImageCouse = request_Couse.ImageCouse,
                    Code = request_Couse.Code,
                    Price = request_Couse.Price,
                    Creatorld = userId.ToString(),
                    TotalCourseDuration = request_Couse.TotalCourseDuration,
                };
                course = await _BaseCouserRepository.CreateAsync(course);
                return "Cập nhật khoá học thành công. chưa làm chức năng gửi gmail cho các sinh viên đã đăng ký khoá trước";
            }
            catch(Exception ex)
            {
                return "Không thể thêm khoá học";
            }

        }

        public async Task<string> AddSubject(int userId , Request_Subject request_Subject)
        {
            var Curren = _contextAccessor.HttpContext.User;
            try
            {
                if (!Curren.Identity.IsAuthenticated)
                {
                    return "Người dùng chưa xác thực ";
                }
                var Id = Curren.FindFirst("Id").Value;
                var userfind = await _BaseUserRepository.GetbyIdAsync(userId);
                if (userfind.Id != int.Parse(Id) && userId != int.Parse(Id))
                {
                    return " Id trong token và id của người dùng không trùng khớp";
                }
                var couserofuser = await _BaseCouserRepository.GetbyIdAsync(userId);
                if (userId != couserofuser.Id)
                {
                    return "Phải là người khởi tạo khoá học mới có quyền thêm chủ đề";
                }
                Subject subject = new Subject
                {
                    Name = request_Subject.Name,
                    Symbol = request_Subject.Symbol,
                    IsActivce = false,
                };
                subject = await _BaseSubjectRepository.CreateAsync(subject);
                var subjectcurren = await _BaseSubjectRepository.GetAsync(x => x.Name == subject.Name);
                CourseSubject courseSubject = new CourseSubject
                {
                    CourseId = couserofuser.Id,
                    SubjectId = subjectcurren.Id,
                };
                courseSubject = await _BaseCSRepository.CreateAsync(courseSubject);
                return "Thêm chứng chỉ thành công";
            }
            catch
            {
                return "Không thêm được subject";
            }
        }
    }
}
