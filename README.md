# validation-lite
A lightweight entity validation framework

# How to use
```C#
Person john = repo.GetPerson();
var v = new ValidateFor<Person>()
    .Field(p => p.Name).ShouldNotEmpty().Length(1, 10)
    .Field(p => p.Age).ShouldGreaterThan(0)
    .Field(p => p.Height).ShouldGreaterThanOrEqualTo(1.8m)
    .Field(p => p.Weight).ShouldLessThan(100m)
    .Field(p => p.Debt).ShouldLessThanOrEqualTo(0)
    .Field(p => p.Money).ShouldEqualTo(1000000)
    .Field(p => p.Company).ShouldNotNull().ValidateWith(
        new ValidateFor<Company>()
        .Field(c => c.Address).ShouldNotEmpty()
        .Field(c => c.EmployeeCount).ShouldGreaterThan(0)
    )
    .Field(p => p.FavoriteBooks).ShouldHaveData().ValidateWith(
        new ValidateFor<Book>()
        .Field(b => b.Name).ShouldNotEmpty()
        .Field(b => b.PageCount).ShouldGreaterThan(0)
    )
    .Entity().ShouldPassCustomCheck(CustomCheck);
var r = v.Validate(john);
if (!r.IsValid)
{
    //Show r.ErrorMessages;
}
```