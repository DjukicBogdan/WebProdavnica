using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebProdavnica.Data;
using WebProdavnica.Models;

namespace WebProdavnica.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ProdavnicaContext db;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, ProdavnicaContext _db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            db = _db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="Unesite email")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage ="Unesi ime")]
            [StringLength(30)]
            public string Ime { get; set; }

            [Required(ErrorMessage = "Unesi prezime")]
            [StringLength(30)]
            public string Prezime { get; set; }

            [Required(ErrorMessage = "Unesi Adresu")]
            [StringLength(100)]
            public string Adresa { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Najmanje 3 karaktera", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Lozinka")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potvrdi lozinku")]
            [Compare("Password", ErrorMessage = "Lozinka ne odgovara potvrdi.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Ime=Input.Ime, Prezime=Input.Prezime, Adresa=Input.Adresa };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                 
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    Kupac k1 = new Kupac {
                        KupacId = user.Id,
                        Ime = user.Ime,
                        Prezime = user.Prezime,
                        Adresa = user.Adresa
                    };

                    try
                    {
                        db.Kupaci.Add(k1);
                        db.SaveChanges();
                        return LocalRedirect(returnUrl);
                    }
                    catch (Exception)
                    {

                        return Page();
                    }
                  

                   
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
