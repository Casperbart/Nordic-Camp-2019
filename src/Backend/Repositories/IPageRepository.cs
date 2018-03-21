﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Repositories
{
    public interface IPageRepository
    {
        Task<IEnumerable<Page>> GetPages();
        Task<Page> GetPage(string url);
    }
}
