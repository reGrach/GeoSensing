using GeoService.BLL.DTO;
using GeoService.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoService.BLL
{
    public static class UserAction
    {
        // тестовые данные вместо использования базы данных
        private static List<UserDTO> users = new List<UserDTO>
        {
            new UserDTO { Login="Admin", Password="12345", Role = RoleType.Admin },
            new UserDTO { Login="Leader", Password="123", Role = RoleType.Leader },
            new UserDTO { Login="User1", Password="123", Role = RoleType.Participant },
            new UserDTO { Login="User2", Password="123", Role = RoleType.Participant }

        };

        public static UserDTO LoginUser(string lgn, string pwd)
        {
            if (users.FirstOrDefault(x => x.Login == lgn && x.Password == pwd) is UserDTO user)
                return user;
            else
                return null;
        }

        public static void TryRegisterUser(string lgn, string pwd)
        {
            if (users.Any(x => x.Login == lgn))
                throw new BusinessLogicException("Пользователь с таким логином уже существует");
            else
                users.Add(new UserDTO { Login = lgn, Password = pwd, Role = RoleType.Participant });
        }
    }
}
