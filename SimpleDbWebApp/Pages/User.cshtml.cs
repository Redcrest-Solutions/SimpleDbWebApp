using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleDbWebApp.Models;

namespace SimpleDbWebApp.Pages
{
    public class UserModel : PageModel
    {
        public SimpleContext DataContext { get; }
        [BindProperty(SupportsGet = true)]
        public int EntryId { get; set; }
        [BindProperty]
        public User Entry { get; set; }

        public UserModel(SimpleContext dataContext)
        {
            DataContext = dataContext;
        }

        public void OnGet()
        {
            if (EntryId == 0)
                Entry = new User { };
            else
                Entry = DataContext.Users.First(r => r.Id == EntryId);
        }
        public IActionResult OnGetDelete()
        {
            DataContext.Users.Where(r => r.Id == EntryId).ExecuteDelete();
            DataContext.SaveChanges();
            return Redirect("/");
        }

        public IActionResult OnPostSave()
        {
            User entry;
            if (EntryId == 0)
            {
                entry = new User { };
                DataContext.Users.Add(entry);
            }
            else
                entry = DataContext.Users.First(r => r.Id == EntryId);
            entry.FirstName = Entry.FirstName;
            entry.LastName = Entry.LastName;
            DataContext.SaveChanges();

            return Redirect("/User/" + entry.Id);
        }
    }
}
