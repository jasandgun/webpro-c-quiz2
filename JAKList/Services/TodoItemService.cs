using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAKList.Data;
using JAKList.Models;
using Microsoft.EntityFrameworkCore;

namespace JAKList.Services;

public class TodoItemService : ITodoItemService
{
    private readonly ApplicationDbContext _context;

    public TodoItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TodoItem[]> GetIncompleteItemsAsync(
        ApplicationUser user)
    {
        var items = await _context.Items
            .Where(x => x.IsDone == false && x.OwnerId == user.Id)
            .ToArrayAsync();
        return items;
    }
    public async Task<bool> AddItemAsync(
        TodoItem newItem, ApplicationUser user)
    {
        newItem.Id = Guid.NewGuid();
        newItem.IsDone = false;
        if (newItem.DueAt==null) {
            newItem.DueAt = DateTimeOffset.Now.AddDays(2);            
        }
        newItem.OwnerId = user.Id;

        _context.Items.Add(newItem);

        var saveResult = await _context.SaveChangesAsync();
        return saveResult == 1;
    }
    public async Task<bool> MarkDoneAsync(
        Guid id, ApplicationUser user)
    {
        var item = await _context.Items.
            Where(x=>x.Id==id && x.OwnerId == user.Id).
            SingleOrDefaultAsync();
        
        if(item == null)
            return false;

        item.IsDone = true;

        var saveResult = await _context.SaveChangesAsync();
        return saveResult == 1;
    }
}