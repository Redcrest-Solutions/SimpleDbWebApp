using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleDbWebApp.Models;
using System.Collections;

namespace SimpleDbWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public SimpleContext DataContext { get; }
        public IEnumerable<User> Users { get; set; }

        public IndexModel(SimpleContext dataContext)
        {
            DataContext = dataContext;
        }

        public void OnGet()
        {
            Users = DataContext.Users.OrderBy(r => r.FirstName).ThenBy(r => r.LastName);
        }
    }
}
