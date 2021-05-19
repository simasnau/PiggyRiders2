using Microsoft.EntityFrameworkCore;
using SmartSaver.Contexts;
using SmartSaver.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace SmartSaver.Service
{
    public class UserServices : IUserServices
    {
        public static UserInformation user;
        private readonly UserContext _context;

        private IConfiguration Configuration { get; }

        public UserServices(UserContext context, IConfiguration config)
        {
            _context = context;
            Configuration=config;
        }
        public async Task<ServiceResponse<List<UserInformation>>> AddUser(UserInformation newUser)
        {
            ServiceResponse<List<UserInformation>> serviceResponse = new ServiceResponse<List<UserInformation>>();

            if (UsernameExists(newUser.Username) || EmailExists(newUser.Email))
            {
                serviceResponse.Success = false;
                return serviceResponse;

            }
            else
            {
                UserBalance userBalance = new UserBalance();
                UserAchievement userAchievement = new UserAchievement();

                _context.UserInfo.Add(newUser);
                await _context.SaveChangesAsync();
                userBalance.user_id = (_context.UserInfo.Where(e => e.Email == newUser.Email && e.Password == newUser.Password).FirstOrDefault()).ID.ToString();
                _context.UserBalance.Add(userBalance);

                userAchievement.userID = (_context.UserInfo.Where(e => e.Email == newUser.Email && e.Password == newUser.Password).FirstOrDefault()).ID;
                _context.UserAchievement.Add(new UserAchievement { Nr = 1, Name = "Register", Description = "Create an account", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 2, Name = "Baby Steps", Description = "Create your first item to save for", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 3, Name = "Profiting", Description = "Have more income than expenses", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 4, Name = "Gains", Description = "Add some income", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 5, Name = "Losses", Description = "Add some expenses", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 6, Name = "Getting Started", Description = "Have three saving goals", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 7, Name = "Completed!", Description = "Complete one saving goal", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 8, Name = "Progress", Description = "Complete three saving goals", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 9, Name = "Keep going", Description = "Complete five saving goals", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 10, Name = "Nice job", Description = "Complete ten saving goals", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 11, Name = "Great job", Description = "Complete twenty five saving goals", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 12, Name = "Awesome", Description = "Complete fifty saving goals", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 13, Name = "Incredible", Description = "Complete a hundred saving goals", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 14, Name = "Profiting 2x", Description = "Have twice as much income than expenses", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 15, Name = "Making Cash", Description = "Have five times as much income than expenses", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 16, Name = "Big Dreams", Description = "Have ten saving goals submitted", Status = 0, Score = 10, userID = userAchievement.userID });
                _context.UserAchievement.Add(new UserAchievement { Nr = 17, Name = "Only Wishes", Description = "Have fifteen saving goals submitted", Status = 0, Score = 10, userID = userAchievement.userID });
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.UserInfo.ToListAsync();

                return serviceResponse;
            }

        }

        public async Task<UserInformation> CheckUser(UserInformation newUser)
        {
            return _context.UserInfo.Where(e => e.Email == newUser.Email && e.Password == newUser.Password).FirstOrDefault();

        }



        public bool UsernameExists(string username)
        {
            return _context.UserInfo.Any(e => e.Username == username);
        }

        private bool EmailExists(string email)
        {
            return _context.UserInfo.Any(e => e.Email == email);
        }

        //Barto
        public async Task<ServiceResponse<List<UserInformation>>> GetAllUsers()
        {
            ServiceResponse<List<UserInformation>> serviceResponse = new ServiceResponse<List<UserInformation>>();
            serviceResponse.Data = await _context.UserInfo.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserInformation>> DeleteUser(int ID)
        {
            ServiceResponse<UserInformation> serviceResponse = new ServiceResponse<UserInformation>();

            try
            {
                _context.EMInfo.RemoveRange(_context.EMInfo.Where(e => e.uID == ID));
                _context.SMInfo.RemoveRange(_context.SMInfo.Where(e => e.user_id == ID.ToString()));
                _context.UserAchievement.RemoveRange(_context.UserAchievement.Where(e => e.userID == ID));
                _context.UserBalance.RemoveRange(_context.UserBalance.Where(e => e.user_id == ID.ToString()));
                _context.UserBudget.RemoveRange(_context.UserBudget.Where(e => e.userID == ID));
                _context.UserInfo.RemoveRange(_context.UserInfo.Where(e => e.ID == ID));
                _context.SaveChanges();
                serviceResponse.Success = true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public UserInformation UpdateUsername(string oldUsername, string newUsername){
            var user=_context.UserInfo.SingleOrDefault(x => x.Username==oldUsername);
            if(user!=null){
                user.Username=newUsername;
                _context.SaveChanges();
                return user;
            }
            return null;
        }
        public UserInformation UpdatePassword(string username, string password){
            var user=_context.UserInfo.SingleOrDefault(x => x.Username==username);
            if(user!=null){
                user.Password=password;
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public ServiceResponse<UserInformation> ResetEmail(string email){

            ServiceResponse<UserInformation> serviceResponse = new ServiceResponse<UserInformation>();

            var user=_context.UserInfo.Where(user=> user.Email.Equals(email)).SingleOrDefault();

            if(user!=null){
                var fromAddress = new MailAddress(Configuration["EmailSettings:Address"], "PiggyRiders");
                var toAddress = new MailAddress(email, user.Username);
                string fromPassword = Configuration["EmailSettings:Password"];
                string subject = "Password Change";
                string body = "Hello, you are receiving this email, because you requested to reset password on account associated with this email. Below is your new generated password. Dont forget to change it immediately after logging in.\n";
                string newPass=RandomPassword();
                body=body+newPass;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                user.Password=newPass;
                _context.SaveChanges();
                serviceResponse.Success=true;
            }
            else {
                serviceResponse.Success=false;
            }
            return serviceResponse;
        }

        public string RandomPassword(int size = 0)  
        {  
            StringBuilder builder = new StringBuilder();  
            builder.Append(RandomString(4, true));  
            builder.Append(new Random().Next(1000, 9999));  
            builder.Append(RandomString(4, false));  
            return builder.ToString();  
        }  

        public string RandomString(int size, bool lowerCase)  
        {  
            StringBuilder builder = new StringBuilder();  
            Random random = new Random();  
            char ch;  
            for (int i = 0; i < size; i++)  
            {  
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));  
                builder.Append(ch);  
            }  
            if (lowerCase)  
                return builder.ToString().ToLower();  
            return builder.ToString();  
        }  

    }


}