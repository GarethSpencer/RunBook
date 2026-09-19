using DAL.Data;
using DAL.Entities;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Infrastructure.Generic;

namespace RepositoryLayer.Infrastructure;

public sealed class WeightRepository(RunBookDbContext dbContext) : RecordingRepository<Weight, decimal>(dbContext), IWeightRepository { }
