using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsersDTO
    {
        private string phoneNumber;
        private string pass;
        private string userFullName;
        private DateTime userDateOfBirth;
        private int userGender = 0;
        private string userEmail;
        private string userAddress;
        private int userRole = 1;

        public UsersDTO() {
            this.phoneNumber = "";
            this.pass = "";
            this.userFullName = "";
            this.userDateOfBirth = DateTime.Now;
            this.userGender = 0;
            this.userEmail = "";
            this.userAddress = "";
            this.userRole = 1;
        }

        public UsersDTO(string phoneNumber, string pass
            , string userFullName, DateTime userDateOfBirth, int userGender
            , string userEmail, string userAddress, int userRole)
        {
            this.phoneNumber = phoneNumber;
            this.pass = pass;
            this.userFullName = userFullName;
            this.userDateOfBirth = userDateOfBirth;
            this.userGender = userGender;
            this.userEmail = userEmail;
            this.userAddress = userAddress;
            this.userRole = userRole;
        }

        public UsersDTO(UsersDTO usersDTO)
        {
            this.phoneNumber = usersDTO.PhoneNumber;
            this.pass = usersDTO.Pass;
            this.userFullName = usersDTO.userFullName;
            this.userDateOfBirth = usersDTO.userDateOfBirth;
            this.userGender = usersDTO.userGender;
            this.userEmail = usersDTO.userEmail;
            this.userAddress = usersDTO.userAddress;
            this.userRole = usersDTO.userRole;
        }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Pass { get => pass; set => pass = value; }
        public string UserFullName { get => userFullName; set => userFullName = value; }
        public DateTime UserDateOfBirth { get => userDateOfBirth; set => userDateOfBirth = value; }
        public int UserGender { get => userGender; set => userGender = value; }
        public string UserEmail { get => userEmail; set => userEmail = value; }
        public string UserAddress { get => userAddress; set => userAddress = value; }
        public int UserRole { get => userRole; set => userRole = value; }
    }
}
