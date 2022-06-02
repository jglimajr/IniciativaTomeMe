using InteliSystem.Cores.CustomersRules.Repositories;
using InteliSystem.Entities.Customers;
using InteliSystem.Utils.Enumerators;
using InteliSystem.Utils.GlobalClasses;
using InteliSystem.Utils.Interfaces;

namespace InteliSystem.Cores.CustomersRules;

public class CustomersRules : RulesBase
{
    private readonly ICustomersRepository _repository;
    public CustomersRules(ICustomersRepository repository) : base(repository) => this._repository = repository;

    public Task<ClassReturn> Add(CustomerAdd add)
    {
        return Task.Run<ClassReturn>(async () =>
        {
            var customer = new Customer(firstName: add.FirstName, lastName: add.LastName, gender: add.Gender, birthDate: add.BirthDate, eMail: add.EMail, passWord: add.PassWord);

            if (customer.ExistNotifications)
            {
                this.AddNotifications(customer.GetAllNotifications);
                return new ClassReturn(ReturnValues.Failure, null);
            }

            var retaux = await this._repository.AddAsync<Customer>(customer);

            return new ClassReturn(ReturnValues.Success, null);
        });
    }
}