using Shoply.Infrastructure.Persistence.EFCore.Context;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Infrastructure.Persistence.EFCore.UnitOfWork;

public class ShoplyUnitOfWork(ShoplyDbContext context) : BaseUnitOfWork<ShoplyDbContext>(context), IShoplyUnitOfWork { }
