using GatherApp.Contracts.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GatherApp.DataContext
{
    public static class SeedEmailData
    {
        public static void Initialize(IServiceProvider serviceProvider, IWebHostEnvironment webHostEnvironment)
        {
            var context = new GatherAppContext(serviceProvider.GetRequiredService<DbContextOptions<GatherAppContext>>());

            var templatePath = Path.Combine(webHostEnvironment.ContentRootPath, "Templates/EmailTemplates.json");
            var templates = JsonConvert.DeserializeObject<List<Email>>(File.ReadAllText(templatePath));

            if (templates == null)
            {
                throw new FileNotFoundException("No template found.");
            }

            ProcessTemplates(context, templates);
        }

        private static void ProcessTemplates(GatherAppContext context, List<Email> templates)
        {
            foreach (var template in templates)
            {
                ProcessTemplate(context, template);
            }

            context.SaveChanges();
        }

        private static void ProcessTemplate(GatherAppContext context, Email template)
        {
            var existingEmailTemplate = context.EmailConstants.Where(e => e.Id == template.Id).SingleOrDefault();

            if (existingEmailTemplate != null)
            {
                var email = UpdateExistingEmailFromTemplate(template, existingEmailTemplate);
                context.EmailConstants.Update(email);
            }
            else
            {
                var email = CreateEmailFromTemplate(template);
                context.EmailConstants.Add(email);
            }
        }

        private static Email CreateEmailFromTemplate(Email template)
        {
            return new Email
            {
                Id = template.Id,
                Type = template.Type,
                Subject = template.Subject,
                Body = template.Body,
            };
        }

        private static Email UpdateExistingEmailFromTemplate(Email template, Email existingEmailTemplate)
        {
            existingEmailTemplate.Id = template.Id;
            existingEmailTemplate.Type = template.Type;
            existingEmailTemplate.Subject = template.Subject;
            existingEmailTemplate.Body = template.Body;

            return existingEmailTemplate;
        }
    }
}
