using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.Lite.Test
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void Validate_No_Rule()
        {
            try
            {
                var v = new ValidateFor<Person>().ShouldNotNull();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "No validation rule was set.");
                return;
            }

            Assert.Fail("Should throw exception: No validation rule was set.");
        }

        [TestMethod]
        public void Validate_Not_Null_Success()
        {
            Person john = new Person() { Name = "John" };
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).ShouldNotNull();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Not_Null_Failed()
        {
            Person john = new Person();
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).ShouldNotNull();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 1);
            Assert.AreEqual(r.ErrorMessages[0], "Name should not be null.");
        }

        [TestMethod]
        public void Validate_Not_Empty_Wrong_Type()
        {
            try
            {
                Person john = new Person();
                var v = new ValidateFor<Person>()
                    .Field(p => p.Age).ShouldNotEmpty();
                var r = v.Validate(john);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "ShouldNotEmpty method only support string type.");
                return;
            }

            Assert.Fail("Should throw exception: ShouldNotEmpty method only support string type.");
        }

        [TestMethod]
        public void Validate_Not_Empty_Success()
        {
            Person john = new Person() { Name = "John" };
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).ShouldNotEmpty();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Not_Empty_Failed()
        {
            Person john = new Person() { Name = "   " };
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).ShouldNotEmpty();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 1);
            Assert.AreEqual(r.ErrorMessages[0], "Name should not be empty.");
        }

        [TestMethod]
        public void Validate_Greater_Than_Wrong_Type()
        {
            try
            {
                Person john = new Person();
                var v = new ValidateFor<Person>()
                    .Field(p => p.Company).ShouldGreaterThan(0);
                var r = v.Validate(john);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "ShouldGreaterThan method only support IComparable type.");
                return;
            }

            Assert.Fail("Should throw exception: ShouldGreaterThan method only support IComparable type.");
        }

        [TestMethod]
        public void Validate_Greater_Than_Success()
        {
            Person john = new Person() { Age = 30, Height = 1.8m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldGreaterThan(0)
                .Field(p => p.Height).ShouldGreaterThan(0m);
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Greater_Than_Failed()
        {
            Person john = new Person() { Age = 0, Height = -1m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldGreaterThan(0)
                .Field(p => p.Height).ShouldGreaterThan(0m);
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Age should be greater than 0.");
            Assert.AreEqual(r.ErrorMessages[1], "Height should be greater than 0.");
        }

        [TestMethod]
        public void Validate_Custom_Check_Success()
        {
            Person john = new Person();
            var v = new ValidateFor<Person>()
                .Entity().ShouldPassCustomCheck(CustomCheckOk);
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Custom_Check_Failed()
        {
            Person john = new Person();
            var v = new ValidateFor<Person>()
                .Entity().ShouldPassCustomCheck(CustomCheckFail);
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 1);
            Assert.AreEqual(r.ErrorMessages[0], "Custom Check Failed.");
        }

        [TestMethod]
        public void Validate_Custom_Check_Wrong_Type()
        {
            try
            {
                Person john = new Person();
                var v = new ValidateFor<Person>()
                    .Field(p => p.Age).ShouldPassCustomCheck(CustomCheckFail);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "ShouldPassCustomCheck method only support entity validation rule.");
                return;
            }
            
            Assert.Fail("Should throw exception: ShouldPassCustomCheck method only support entity validation rule.");
        }

        ValidationResult CustomCheckOk(Person p)
        {
            return new ValidationResult { IsValid = true };
        }

        ValidationResult CustomCheckFail(Person p)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessages = new System.Collections.Generic.List<string> { "Custom Check Failed." }
            };
        }
    }
}
