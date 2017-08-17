using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AstralTest.Database
{
    public static class DataInitializer
    {
        public static async Task Initialize(this DatabaseContext context, IServiceProvider serviceProvider)
        {
            //Тут создаются 2 роли(admin и user), если таковых нет
            {
                var roleUser = await context.Roles.SingleOrDefaultAsync(x => x.RoleName == RolesOption.User.ToString());
                if (roleUser == null)
                {
                    context.Roles.Add(new Role(RolesOption.User, nameof(RolesOption.User)));
                }

                var roleAdmin =
                    await context.Roles.SingleOrDefaultAsync(x => x.RoleName == RolesOption.Admin.ToString());
                if (roleAdmin == null)
                {
                    context.Roles.Add(new Role(RolesOption.Admin, nameof(RolesOption.Admin)));
                }

                if (roleUser == null || roleAdmin == null)
                {
                    await context.SaveChangesAsync();
                }
            }
            //Тут создаётся пользователь admin, если такового нет
            {
                if (await context.Users.SingleOrDefaultAsync(x => x.UserName == "admin") == null)
                {
                    var resRole = await context.Roles.SingleAsync(x => x.RoleName == RolesOption.Admin.ToString());
                    var resultUser = new User
                    {
                        Email = "admin@mail.com",
                        UserName = "admin",
                        PhoneNumber = "99999999999",
                        RoleId = resRole.RoleId,
                        PasswordSalt = "qwer98kj"
                    };
                    var hashProvider = serviceProvider.GetRequiredService<IPasswordHasher<User>>();
                    var resultHash = hashProvider.HashPassword(resultUser, "admin");
                    resultUser.PasswordHash = resultHash;
                    context.Users.Add(resultUser);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}