using BlazorDemo.Server.Domain;
using System.Collections.Generic;
using System;

namespace BlazorDemo.Server.Persistence
{
    public interface ITodoStore
        {
        public List<TodoEntity> Todos { get; set; }
        public void Upsert(TodoEntity entity);
        public TodoEntity? GetById(int id);
        public List<TodoEntity> GetAll();
        public void Delete(int id);
    }

    public class TodoStore : ITodoStore
    {
        public List<TodoEntity> Todos { get; set; } = new List<TodoEntity>();
        public int NextIndex { get; set; } = 1;

        public void Upsert(TodoEntity entity)
        {
            if (entity.Id.HasValue)
            {
                var todo = GetById(entity.Id.Value);
                todo = entity;
            } else
            {
                entity.Id = NextIndex++;
                Todos.Add(entity);
            }
        }

        public TodoEntity? GetById(int id)
        {
            return Todos.Find(todo => todo.Id == id);
        }

        public List<TodoEntity> GetAll()
        {
            return Todos;
        }

        public void Delete(int id)
        {
            Todos.RemoveAll(item => item.Id == id);
        }

    }
}

