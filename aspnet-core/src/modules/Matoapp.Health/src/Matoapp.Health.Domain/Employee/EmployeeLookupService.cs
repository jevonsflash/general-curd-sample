using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Matoapp.Health.Employee
{
    public class EmployeeLookupService : UserLookupService<Employee, IEmployeeRepository>, IEmployeeLookupService
    {
        public EmployeeLookupService(
            IEmployeeRepository userRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                unitOfWorkManager)
        {

        }

        protected override Employee CreateUser(IUserData externalUser)
        {
            return new Employee(externalUser);
        }
    }
}