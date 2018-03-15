using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoMvc.Models;
using TodoMvc.Data;


namespace TodoMvc.Services
{
    public class ToDoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;
        
        public ToDoItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<ToDoItem>> 
        GetIncompleteItemsAsync()
        {
           var items = await _context.Items
            .Where(x => x.IsDone == false)
            .ToArrayAsync();


            return items;

        }
        public async Task<bool> AddItemAsync(NewToDoItem newToDoItem)

        {
            var entity = new ToDoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newToDoItem.Title,
                DueAt = DateTimeOffset.Now.AddDays(3)
                
            };
            _context.Items.Add(entity);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

    }
}