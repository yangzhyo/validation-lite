# validation-lite
A lightweight entity validation framework

# How to use
```C#
Person john = repo.GetPerson();
var v = new ValidateFor<Person>()
	.Field(p => p.Name).ShouldNotNull().ShouldNotEmpty()
	.Field(p => p.Age).ShouldGreaterThan(0)
	.Entity().ShouldPassCustomCheck(CustomCheck);
var r = v.Validate(john);
if (!r.IsValid)
{
	//Show r.ErrorMessages;
}
```