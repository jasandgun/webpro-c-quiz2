using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JAKList.Models;
using JAKList.Data;

namespace JAKList.Controllers;

[Authorize(Roles = "Administrator")]
public class ManageUsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    

    public ManageUsersController(UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var admins = (await _userManager
            .GetUsersInRoleAsync("Administrator")).ToArray();

        var everyone = await _userManager.Users
            .Where(u => !admins.Contains(u))
            .ToArrayAsync();

        var model = new ManageUsersViewModel
        {
            Administrators = admins,
            Everyone = everyone
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string UserId)
    {
        var SelectedUser = await _context.Users.FindAsync(UserId);
        if (SelectedUser == null) {
            return BadRequest("No such user exists. The user ID you've requested: " + UserId);
        } else {
            _context.Users.Remove(SelectedUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}