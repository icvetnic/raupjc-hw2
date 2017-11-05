using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment2
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDatabase = null)
        {
            if (initialDatabase != null)
            {
                _inMemoryTodoDatabase = initialDatabase;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
            //_inMemoryTodoDatabase = initialDatabase ?? new GenericList<TodoItem>();
        }


        public TodoItem Get(Guid todoId)
        {
            IEnumerable<TodoItem> todoItems = _inMemoryTodoDatabase.Where(item => item.Id == todoId);
            TodoItem todoItem = todoItems.FirstOrDefault();
            return todoItem;

        }

        public TodoItem Add(TodoItem todoItem)
        {
            if (_inMemoryTodoDatabase.Contains(todoItem))
            {
                throw new DuplicateTodoItemException("dupicate id: " + todoItem.Id);
            }
            _inMemoryTodoDatabase.Add(todoItem);
            return Get(todoItem.Id);
        }

        public bool Remove(Guid todoId)
        {
            IEnumerable<TodoItem> todoItems = _inMemoryTodoDatabase.Where(item => item.Id == todoId);
            TodoItem todoItem = todoItems.FirstOrDefault();
            if (todoItem == null)
            {
                return false;
            }
            return _inMemoryTodoDatabase.Remove(todoItem);
        }

        public TodoItem Update(TodoItem todoItem)
        {
            if (!_inMemoryTodoDatabase.Contains(todoItem))
            {
                _inMemoryTodoDatabase.Add(todoItem);
                return Get(todoItem.Id);
            }
            TodoItem curentTodoItem = Get(todoItem.Id);
            curentTodoItem.ReplaceAllData(todoItem);
            return Get(todoItem.Id);
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem item = Get(todoId);
            if (item == null)
            {
                throw new ArgumentException("TodoItem with id: " + todoId + " does not exist.");
            }
            return item.MarkAsCompleted();
        }

        public List<TodoItem> GetAll()
        {
            List<TodoItem> list = _inMemoryTodoDatabase
                .OrderByDescending(item => item.DateCreated)
                .ToList();
            return list;
        }

        public List<TodoItem> GetActive()
        {
            List<TodoItem> list = _inMemoryTodoDatabase
                .Where(item => !item.IsCompleted)
                .ToList();
            return list;
        }

        public List<TodoItem> GetCompleted()
        {
            List<TodoItem> list = _inMemoryTodoDatabase
                .Where(item => item.IsCompleted)
                .ToList();
            return list;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        { 
        List<TodoItem> list = _inMemoryTodoDatabase
                .Where(filterFunction)
                .ToList();
            return list;
        }
    }

    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException()
        {
        }

        public DuplicateTodoItemException(string message) : base(message)
        {
        }

        public DuplicateTodoItemException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}