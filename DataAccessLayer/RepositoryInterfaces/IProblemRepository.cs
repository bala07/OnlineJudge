﻿using System.Collections.Generic;

using Domain.Models;

namespace DataAccessLayer.RepositoryInterfaces
{
    public interface IProblemRepository
    {
        Problem Get(string problemCode);

        IList<Problem> GetAll();
    }
}
