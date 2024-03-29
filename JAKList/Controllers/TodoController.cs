using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAKList.Services;
using JAKList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JAKList.Controllers;
[Authorize]
public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService;
    private readonly UserManager<ApplicationUser> _userManager;

    public TodoController(ITodoItemService todoItemService,
        UserManager<ApplicationUser> userManager)
    {
        _todoItemService = todoItemService;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index() 
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) 
            return Challenge();

        // Get to-do items from database
        var items = await _todoItemService
            .GetIncompleteItemsAsync(currentUser);

        // Put items into a model
        var model = new TodoViewModel() {
            Items = items
        };

        // Render view using the model
        return View(model);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(TodoItem newItem)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) 
            return Challenge();

        var successful = await _todoItemService
            .AddItemAsync(newItem, currentUser);
        if (!successful)
        {
            return BadRequest("Could not add item.");
        }

        return RedirectToAction("Index");
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkDone(Guid id) 
    {
        if(id==Guid.Empty) 
        {
            return RedirectToAction("Index");
        }
        
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) 
            return Challenge();

        var successful = await _todoItemService
            .MarkDoneAsync(id, currentUser);
        if (!successful)
        {
            return BadRequest("Could not mark item as done.");
        }
        return RedirectToAction("Index");
    }
}
