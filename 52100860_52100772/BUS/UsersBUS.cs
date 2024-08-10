using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class UsersBUS
    {
        private UsersDAL usersDAL;

        public UsersBUS()
        {
            usersDAL = new UsersDAL();
        }

        public bool InsertUser(UsersDTO usersDTO)
        {
            return usersDAL.InsertUser(usersDTO);
        }

        public bool DeleteUser(UsersDTO usersDTO)
        {
            return usersDAL.DeleteUser(usersDTO);
        }

        public bool DeleteUser(String userID)
        {
            return usersDAL.DeleteUser(userID);
        }

        public bool UpdateUser(UsersDTO usersDTO)
        {
            return usersDAL.UpdateUser(usersDTO);
        }

        public List<UsersDTO> GetAllUsers(int userRole)
        {
            return usersDAL.GetAllUsers(userRole);
        }

        public UsersDTO getUserByPhoneNumber(string phoneNumber)
        {
            return usersDAL.getUserByPhoneNumber(phoneNumber);
        }

        public UsersDTO Login(string phonenumber, string password)
        {
            return usersDAL.Login(phonenumber, password);
        }
    }
}
