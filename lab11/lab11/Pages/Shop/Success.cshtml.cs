using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab11.Pages.Shop
{
    public class SuccessModel : PageModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Method { get; set; }

        public void OnGet(string name, string address, string method)
        {
            Name = name;
            Address = address;
            Method = method;
        }
    }
}