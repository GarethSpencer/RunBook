using DAL.Entities;
using RepositoryLayer.Abstractions.Generic;

namespace RepositoryLayer.Abstractions;

public interface IWeightRepository : IEFRepository<Weight>, IRecordingRepository<decimal> { }
