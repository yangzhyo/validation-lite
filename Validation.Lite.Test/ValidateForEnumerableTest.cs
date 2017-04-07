using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.Lite.Test
{
    [TestClass]
    public class ValidateForEnumerableTest
    {
        [TestMethod]
        public void Validate_Enumerable_Entity_Should_Have_Data_Success()
        {
            List<Person> persons = new List<Person>
            {
                new Person {Name = "John"}
            };

            var v = new ValidateForEnumerable<List<Person>, Person>()
                .ShouldHaveData();
            var r = v.Validate(persons);
            Assert.IsTrue(r.IsValid);
        }

        [TestMethod]
        public void Validate_Enumerable_Entity_Sub_Validation_Success()
        {
            List<Person> persons = new List<Person>
            {
                new Person {Name = "John"}
            };

            var v = new ValidateForEnumerable<List<Person>, Person>()
                .ValidateElementWith(
                    new ValidateFor<Person>()
                        .Field(p => p.Name).ShouldNotEmpty()
                        .Build());
            var r = v.Validate(persons);
            Assert.IsTrue(r.IsValid);
        }

        [TestMethod]
        public void Validate_Enumerable_Entity_Custom_Check_Success()
        {
            List<Person> persons = new List<Person>
            {
                new Person {Name = "John"}
            };

            var v = new ValidateForEnumerable<List<Person>, Person>()
                .ShouldPassCustomCheck(ps => ValidationResult.Valid);
            var r = v.Validate(persons);
            Assert.IsTrue(r.IsValid);
        }

        [TestMethod]
        public void Validate_Enumerable_Entity_Custom_Check_Failed()
        {
            List<Person> persons = new List<Person>();
            var v = new ValidateForEnumerable<List<Person>, Person>()
                .ShouldPassCustomCheck(
                    ps =>
                    {
                        if (ps == null || ps.Count == 0)
                        {
                            return new ValidationResult(false, "Person collection no data.");
                        }
                        return ValidationResult.Valid;
                    }
                );

            var r = v.Validate(persons);
            Assert.IsFalse(r.IsValid);
            Assert.AreEqual(r.ErrorMessages.Count, 1);
            Assert.AreEqual(r.ErrorMessages[0], "Person collection no data.");
        }

        [TestMethod]
        public void Validate_Enumerable_Entity_Should_Have_Data_Failed()
        {
            List<Person> persons = new List<Person>();
            var v = new ValidateForEnumerable<List<Person>, Person>()
                .ShouldHaveData();

            var r = v.Validate(persons);
            Assert.IsFalse(r.IsValid);
            Assert.AreEqual(r.ErrorMessages.Count, 1);
            Assert.AreEqual(r.ErrorMessages[0], "List`1 should have data.");
        }

        [TestMethod]
        public void Validate_Enumerable_Entity_Sub_Validation_Failed()
        {
            List<Person> persons = new List<Person> {new Person()};
            var v = new ValidateForEnumerable<List<Person>, Person>()
                .ValidateElementWith(
                    new ValidateFor<Person>()
                        .Field(p => p.Name).ShouldNotEmpty()
                        .Build()
                );

            var r = v.Validate(persons);
            Assert.IsFalse(r.IsValid);
            Assert.AreEqual(r.ErrorMessages.Count, 1);
            Assert.AreEqual(r.ErrorMessages[0], "List`1 Collection@1:Person.Name should not be empty.");
        }
    }
}
