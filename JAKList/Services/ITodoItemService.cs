using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JAKList.Models;

namespace JAKList.Services;

public interface ITodoItemService
{
    Task<TodoItem[]> GetIncompleteItemsAsync();

    Task<bool> AddItemAsync(TodoItem newItem);
}