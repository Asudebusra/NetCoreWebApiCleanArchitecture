﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(); // geriye etkilenen satır sayısını döncek
    }
}
