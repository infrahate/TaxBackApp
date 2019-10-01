using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxBackApp.Models;

namespace TaxBackApp.Data
{
    public class InitData
    {
        public static void InitDatabseData(IApplicationBuilder app) {
            using ( var s = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope()) {
                var context = s.ServiceProvider.GetService<TaxBackContext>();

                if (!context.Users.Any()) {
                    context.Database.OpenConnection();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Users] ON");
                    context.Database.ExecuteSqlCommand("INSERT INTO[dbo].[Users](UserId, UserName) VALUES(1, 'John Doe'), (2, 'Jaine Doe'), (3, 'Jaina Pudding')");
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Users] OFF");
                    context.Database.CloseConnection();
                }

                

                if (!context.TaskStatuses.Any())
                {
                    context.Database.OpenConnection();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TaskStatuses] ON");
                    context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[TaskStatuses](TaskStatusId,TaskStatusDescription) VALUES (1,'Open'),(2,'Closed'),(3,'Pending')");
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TaskStatuses] OFF");
                    context.Database.CloseConnection();
                }
                

                if (!context.TaskTypes.Any())
                {
                    context.Database.OpenConnection();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TaskTypes] ON");
                    context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[TaskTypes](TaskTypeId,TaskTypeDescription) VALUES (1,'Programming'),(2,'PM'),(3,'HR'),(4,'PR')");
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TaskTypes] OFF");
                    context.Database.CloseConnection();
                }

                if (!context.TaskCommentTypes.Any())
                {
                    context.Database.OpenConnection();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TaskTypes] ON");
                    context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[TaskCommentTypes](TaskCommentTypeId,TaskTypeDescription) VALUES (1,'Comment'),(2,'Action')");
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TaskTypes] OFF");
                    context.Database.CloseConnection();
                }

            }
        }
    }
}
