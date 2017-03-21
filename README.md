# validation-lite
A lightweight entity validation framework

# How to use
```C#
Person john = repo.GetPerson();
var v = new ValidateFor<Person>()
	.Field(p => p.Name).ShouldNotEmpty().Length(1, 10)
	.Field(p => p.Age).ShouldGreaterThan(0)
	.Field(p => p.Height).ShouldGreaterThanOrEqualTo(1.0m)
	.Field(p => p.Company).ShouldNotNull()
	.Field(p => p.Friends).ShouldHaveData()
	.Entity().ShouldPassCustomCheck(CustomCheck);
var r = v.Validate(john);
if (!r.IsValid)
{
	//Show r.ErrorMessages;
}
```