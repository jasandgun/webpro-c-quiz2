using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JAKList.Models;

namespace JAKList.Services;

public interface ITodoItemService
{
    Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);

    Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user);

    Task<bool> MarkDoneAsync(Guid id, ApplicationUser user);
}