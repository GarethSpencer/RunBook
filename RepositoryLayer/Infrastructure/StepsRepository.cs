using DAL.Data;
using DAL.Entities;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Infrastructure.Generic;

namespace RepositoryLayer.Infrastructure;

public sealed class StepsRepository(RunBookDbContext dbContext) : RecordingRepository<Steps, bool>(dbContext), IStepsRepository { }
