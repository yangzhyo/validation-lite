using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.Lite.Test
{
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

    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void Test_Instant_Validator()
        {
            Person john = new Person();
            PersonValidator validator = new PersonValidator();
            ValidationResult result = validator.Validate(john);
            Assert.IsFalse(result.IsValid);
        }
    }
}
