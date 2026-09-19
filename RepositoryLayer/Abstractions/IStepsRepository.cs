using DAL.Entities;
using RepositoryLayer.Abstractions.Generic;

namespace RepositoryLayer.Abstractions;

public interface IStepsRepository : IEFRepository<Steps>, IRecordingRepository<bool> { }
