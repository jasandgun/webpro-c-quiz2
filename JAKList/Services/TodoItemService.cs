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

    public async Task<TodoItem[]> GetIncompleteItemsAsync()
    {
        var items = await _context.Items
            .Where(x => x.IsDone == false)
            .ToArrayAsync();
        return items;
    }
}