using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace AstralTest.Extensions
{
    /// <summary>
    /// Статический класс, предоставляющий методы расширения
    /// </summary>
    public static class ExtensionView
    {
        /// <summary>
        /// Метод расширения для отображения пользоваетеля
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static object UserView(this User user)
        {
            if (user != null)
            {
                return new
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Id = user.UserId,
                    Notes = user.Notes.Select(n =>
                        new
                        {
                            Text = n.Text,
                            Id = n.NoteId
                        }),
                    NameContainers = user.TasksContainers
                        .Select(x => new
                        {
                            Name = x.Name
                        })
                };
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения пользователей, только аднминам
        /// </summary>
        /// <param name="users"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static object UsersForAdminView(this IEnumerable<User> users, HttpContext httpContext)
        {
            if (httpContext.User.IsInRole(RolesOption.Admin.ToString()))
            {
                return users.Select(x =>
                    new
                    {
                        Name = x.UserName,
                        Email = x.Email,
                        Id = x.UserId,
                        Role = x.Role.RoleName,
                        Notes = x.Notes.Select(n => n.NoteView()),
                        NameContainers = x.TasksContainers
                            .Select(a => new
                            {
                                Name = a.Name
                            })
                    });
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения заметки
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static object NoteView(this Note note)
        {
            if (note == null) return null;
            return new
            {
                Id = note.NoteId,
                Text = note.Text,
                User = note.Master.UserName
            };
        }

        /// <summary>
        /// Метод расширения для отображение задачи пользователя 
        /// </summary>
        /// <param name="userTask"></param>
        /// <returns></returns>
        public static object UserTaskView(this UserTask userTask)
        {
            if (userTask != null)
                return new
                {
                    idContainer=userTask.ListId,
                    IdTask = userTask.TaskId,
                    Text = userTask.TextTask,
                    EndTime = userTask.EndTime,
                };
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения контейрена задач(отображает так же задачи)
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static object TasksContainerView(this TasksContainer container)
        {
            if (container != null)
                return new
                {
                    IdContainer = container.ListId,
                    NameContainer = container.Name,
                    UserName = container.Master.UserName,
                    Tasks = container.Tasks
                        .Select(t => t?.UserTaskView())
                };
            return null;
        }
        
        /// <summary>
        /// Метод расширения для отображения информации о файле
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static object FileView(this File file)
        {
            if (file != null)
            {
                return new
                {
                    idFile = file.FileId,
                    NameFile = file.NameFile,
                    TypeFile = file.TypeFile,
                    CreatedTime = file.CreatedTime
                };
            }
            return null;
        }

        /// <summary>
        /// Метод расширения для отображения информации о прикреплении
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static object AttachmentView(this Attachment attachment)
        {
            if (attachment != null)
            {
                return new
                {
                    IdAttachment = attachment.AttachmentId,
                    IdTask = attachment.TaskId,
                    IdFile = attachment.FileId
                };
            }
            return null;
        }
    }
}
