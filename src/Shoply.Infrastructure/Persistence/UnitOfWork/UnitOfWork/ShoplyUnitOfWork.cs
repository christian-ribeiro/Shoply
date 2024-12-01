using Shoply.Infrastructure.Persistence.Context;
using Shoply.Infrastructure.Persistence.UnitOfWork.Interface;

namespace Shoply.Infrastructure.Persistence.UnitOfWork;

public class ShoplyUnitOfWork(ShoplyDbContext context) : BaseUnitOfWork<ShoplyDbContext>(context), IShoplyUnitOfWork { }
