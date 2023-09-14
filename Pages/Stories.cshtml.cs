using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Collabra_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Collabra_Test.Pages
{
	public class StoriesModel : PageModel
    {
        [BindProperty]
        public int NumberInput { get; set; }

        public List<HackerNewsStory> hackerNewsStories { get; set; }

        public StoriesModel()
        {
            hackerNewsStories = new List<HackerNewsStory>();
        }

        public void OnGet()
        {           

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Access the entered number from the NumberInput property
                int enteredNumber = NumberInput;

                hackerNewsStories = await new Services.HackerNewsApi().GetBestStories(enteredNumber);
            }

            // Your code here
            return Page();
        }
    }
}
