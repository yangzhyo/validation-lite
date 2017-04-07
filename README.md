# validation-lite
- A lightweight and high performance entity validation framework.
- Based on generic type and expression tree, it's very efficient.
- Flexible and reusable.

# How to use
## For Single Object
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
    .EnumerableField<List<Book>, Book>(p => p.FavoriteBooks).ShouldHaveData().ValidateWith(
        new ValidateFor<Book>()
        .Field(b => b.Name).ShouldNotEmpty()
        .Field(b => b.PageCount).ShouldGreaterThan(0)
    )
    .Entity().ShouldPassCustomCheck(CustomCheck)
	.Build();
	
var r = v.Validate(john);
if (!r.IsValid)
{
    //Show r.ErrorMessages;
}
```

## For Enumerable Object
```C#
List<Person> persons = new List<Person>()
{
    new Person() {Name = "John"}
};

var v = new ValidateForEnumerable<List<Person>, Person>()
    .ShouldHaveData()
	.ShouldPassCustomCheck(ps => ValidationResult.Valid);
	.ValidateElementWith(
        new ValidateFor<Person>()
            .Field(p => p.Name).ShouldNotEmpty());

var r = v.Validate(persons);
```

## Reusable Validator
```C#
public class PersonValidator : ValidateFor<Person>
{
    public PersonValidator()
    {
        this.Field(p => p.Age).ShouldGreaterThan(0)
            .Field(p => p.Company).ShouldNotNull().ValidateWith(new CompanyValidator());
    }
}

public class CompanyValidator : ValidateFor<Company>
{
    public CompanyValidator()
    {
        this.Field(c => c.Address).ShouldNotEmpty().ShouldPassCustomCheck(CheckAddress);
    }

    public ValidationResult CheckAddress(string address)
    {
        return ValidationResult.Valid;
    }
}

Person john = new Person();
PersonValidator validator = new PersonValidator();
ValidationResult result = validator.Validate(john);
```