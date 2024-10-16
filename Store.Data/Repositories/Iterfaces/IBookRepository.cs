﻿using Store.Data.Entities;

namespace Store.Data.Repositories.Iterfaces
{
    public interface IBookRepository
    {
        Task<Book> Get(int id);
        Task<IEnumerable<Book>> Get();
        Task<int> Create(Book book);
        Task<Book> Update(Book book);
        Task<int> Delete(int id);
    }
}
