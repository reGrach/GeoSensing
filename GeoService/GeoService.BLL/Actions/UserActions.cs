using GeoService.BLL.DTO;
using GeoService.BLL.Utils;
using GeoService.DAL;
using GeoService.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoService.BLL.Actions
{
    public static class UserActions
    {
        public static IdentityDTO TryRegisterUser(this GeoContext ctx, string login, string password)
        {
            if (ctx.Users.Any(x => x.Login == login))
                throw new ApiException("Пользователь с таким логином уже существует", nameof(TryRegisterUser), 400);

            var hasher = new PasswordHash();

            var dbUser = ctx.Users.Add(new User
            {
                Login = login,
                Salt = hasher.Salt,
                PasswordHash = hasher.Hash(password),
                Role = RoleEnum.NonDefined,
                RegistrationDate = DateTime.Now
            });
            ctx.SaveChanges();

            return new IdentityDTO
            {
                Id = dbUser.Entity.Id,
                Login = dbUser.Entity.Login,
                Role = dbUser.Entity.Role,
            };
        }

        public static IdentityDTO AuthenticationUser(this GeoContext ctx, string login, string password)
        {
            if (ctx.Users.FirstOrDefault(x => x.Login == login) is User userDb)
            {
                var hasher = new PasswordHash(userDb.Salt);
                if (!hasher.Verify(userDb.PasswordHash, password))
                    throw new ApiException("Введен неверный пароль", nameof(AuthenticationUser), 400);
                else
                    return new IdentityDTO
                    {
                        Id = userDb.Id,
                        Login = userDb.Login,
                        Role = userDb.Role,
                    };
            }
            else
                throw new ApiException("Пользователя с таким логином не существует", nameof(AuthenticationUser), 400);
        }
        
        public static RoleEnum GetUserRole(this GeoContext ctx, int id)
        {
            if (ctx.Users.Find(id) is User userDb)            
                return userDb.Role;
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(AuthenticationUser), 404);
        }

        public static UserWithImgDTO UpdateProfile(this GeoContext ctx, int id, UserWithImgDTO model)
        {
            if (ctx.Users.Find(id) is User dbUser)
            {
                dbUser.Name = model.Name;
                dbUser.Surname = model.SurName;
                ctx.SaveChanges();

                return GetProfile(ctx, id);
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(UpdateProfile), 404);
        }

        public static UserWithImgDTO GetProfile(this GeoContext ctx, int id)
        {
            if (ctx.Users.Find(id) is User dbUser)
                return dbUser.ToExtensionDTO();
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(GetProfile), 404);
        }

        public static IEnumerable<UserWithImgDTO> GetFilterUsers(this GeoContext ctx, string query, string role)
        {
            var dbUsers = ctx.Users.AsNoTracking()
                .Where(x => string.IsNullOrEmpty(role) || x.Role.ToString() == role)
                .AsEnumerable();
            if (string.IsNullOrEmpty(query))
                dbUsers = dbUsers
                    .Where(x => x.Login.Equals(query, StringComparison.InvariantCultureIgnoreCase)
                    || x.Name.Equals(query, StringComparison.InvariantCultureIgnoreCase)
                    || x.Surname.Equals(query, StringComparison.InvariantCultureIgnoreCase));

            return dbUsers.Select(x => x.ToImgDTO());
        }

        public static void CreateUpdateAvatar(this GeoContext ctx, int userId, byte[] content, string mime)
        {
            if (ctx.Users.Find(userId) is User dbUser)
            {
                var ava = dbUser.Avatar ?? ctx.Avatars.Add(new Avatar { User = dbUser }).Entity;
                ava.FileContent = content;
                ava.MimeType = mime;
                ctx.SaveChanges();
            }
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(CreateUpdateAvatar), 404);
        }

        public static string GetAvatar(this GeoContext ctx, int userId)
        {
            if (ctx.Users.Find(userId) is User dbUser)
                return dbUser.GetAvatarSrc();
            else
                throw new ApiException(ConstMsg.UserNotFound, nameof(GetAvatar), 404);
        }

        internal static UserDTO ToDTO(this User user) =>
            new UserDTO
            {
                Login = user.Login,
                Name = user.Name,
                SurName = user.Surname,
                IsLeader = user.Role == RoleEnum.Leader
            };

        internal static UserWithImgDTO ToImgDTO(this User user) =>
            new UserWithImgDTO
            {
                Login = user.Login,
                Name = user.Name,
                SurName = user.Surname,
                AvatarSrc = user.GetAvatarSrc(),
                IsLeader = user.Role == RoleEnum.Leader                
            };

        internal static UserExtensionDTO ToExtensionDTO(this User user) =>
            new UserExtensionDTO
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                SurName = user.Surname,
                AvatarSrc = user.GetAvatarSrc(),
                IsLeader = user.Role == RoleEnum.Leader,                
                Team = user.Team is Team team ? team.ToDTO() : null
            };

        internal static string GetAvatarSrc(this User user) =>
            user.Avatar is Avatar _ava ? $"data:{_ava.MimeType};base64,{Convert.ToBase64String(_ava.FileContent)}" : string.Empty;
    }
}
