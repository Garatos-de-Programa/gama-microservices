﻿using Gama.Application.DataContracts.Queries.Common;
using Gama.Domain.Models.Occurrences;

namespace Gama.Application.Contracts.Repositories
{
    public interface IOccurrenceRepository : IRepository<Occurrence>
    {
        Task<IEnumerable<Occurrence>> GetAsync(DateSearchQuery search, int offset, int size);
    }
}
