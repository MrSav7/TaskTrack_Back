using KyrsachAPI.Context;
using KyrsachAPI.Entities.User;
using KyrsachAPI.Models.User;
using Microsoft.EntityFrameworkCore;
using StoredProcedureEFCore;
using System.Security.Cryptography;
using System.Text;


namespace KyrsachAPI.Service
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User Authenticate(string UserLogin, string UserPassword);
        List<AmdGetUsers> AmdGetUsers(int? userId);
        List<UserActiveStatus> getStatusTipes();
        bool ChangeUserStatus(int userId, int status);
        void CreateNewUser(NewUserForm form);
        List<UserRole> GetRoles();
    }

    public class UserService: IUserService
    {
        private readonly ILogger _logger;
        private readonly TaskTrackContext _context;

        public UserService(TaskTrackContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        

        public List<User> GetAllUsers() {
            return _context.Users
                .ToList();
        }

        public User Authenticate(string UserLogin, string UserPassword) {
            var existUser = _context.Users.SingleOrDefault(l => l.UserLogin == UserLogin);

            if(existUser == null)
            {
                return null;
            }

            /*byte[] hash = Encoding.ASCII.GetBytes(UserPassword.GetHashCode() + UserPassword.Length + UserPassword[2] + "numero_uno_-_meow(woah)");
            MD5 md5 = MD5.Create();
            byte[] byteHashPass = md5.ComputeHash(hash);
            string HashPass = Convert.ToBase64String(byteHashPass);*/

            string HashPass = GetSHA256Hash(UserPassword);

            //UserPassword = HashPass;
            //_logger.LogInformation(existUser.UserPassword);
            //_logger.LogInformation(HashPass);
            if (existUser.UserPassword == HashPass)
            {
                _logger.LogInformation("true");
            }
            else
            {
                _logger.LogInformation("false");
            }

            return existUser.UserPassword == HashPass ? existUser : null;
        }

        public static string GetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Преобразуем входную строку в массив байт и вычисляем хэш
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Преобразуем массив байт в строку в формате "x2" (двузначное шестнадцатеричное число)
                StringBuilder sb = new StringBuilder();
                foreach (byte hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public List<AmdGetUsers> AmdGetUsers(int? userId)
        {
            List<AmdGetUsers> res = new List<AmdGetUsers>();
            if(userId == null)
            {
                _context.LoadStoredProc("[dbo].[AdmPanGetUsers]")
                .Exec(result => res = result.ToList<AmdGetUsers>());
                return res;
            }

            _context.LoadStoredProc("[dbo].[AdmPanGetUsers]")
                .AddParam("userId", userId.Value)
                .Exec(result => res = result.ToList<AmdGetUsers>());
            return res;
        }

        public List<UserActiveStatus> getStatusTipes()
        {
            return _context.UserActiveStatuses.ToList();
        }

        public bool ChangeUserStatus(int userId, int status)
        {
            var u = _context.Users.Where(u => u.UserId == userId).First();
            if(u == null)
            {
                return false;
            }

            using (var tr = _context.Database.BeginTransaction())
            {
                u.UserActiveStatus = Convert.ToBoolean(status);
                _context.SaveChanges();
                tr.Commit();
            }
            return true;
        }

        public void CreateNewUser(NewUserForm form)
        {
            User user = new User()
            {
                UserLogin = form.UserLogin,
                UserPassword = GetSHA256Hash(form.UserPassword),
                UserEmail = form.UserEmail,
                UserFirstName = form.UserFirstName,
                UserLastName = form.UserLastName,
                UserMiddleName = form.UserMiddleName,
                UserRoleId = form.UserRoleId,
                UserActiveStatus = true
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<UserRole> GetRoles()
        {
            return _context.UserRoles.AsNoTracking()
                .OrderBy(o => o.RoleId)
                .ToList();
        }
    }
}
