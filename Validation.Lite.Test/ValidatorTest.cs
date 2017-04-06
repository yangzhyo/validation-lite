using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.Lite.Test
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void Validate_Not_Null_Success()
        {
            Person john = new Person() { Name = "John" };
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).ShouldNotNull()
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Not_Null_Failed()
        {
            Person john = new Person();
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).ShouldNotNull()
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 1);
            Assert.AreEqual(r.ErrorMessages[0], "Person.Name should not be null.");
        }

        [TestMethod]
        public void Validate_Not_Empty_Success()
        {
            Person john = new Person() { Name = "John" };
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).ShouldNotEmpty()
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Not_Empty_Failed()
        {
            Person john = new Person() { Name = "   " };
            Person bob = new Person();
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).ShouldNotEmpty()
                .Build();
            var r = v.Validate(john);
            
            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 1);
            Assert.AreEqual(r.ErrorMessages[0], "Person.Name should not be empty.");

            r = v.Validate(bob);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 1);
            Assert.AreEqual(r.ErrorMessages[0], "Person.Name should not be empty.");
        }

        [TestMethod]
        public void Validate_Length_Success()
        {
            Person john = new Person() { Name = "John" };
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).Length(4).Length(0, 5)
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Length_Failed()
        {
            Person john = new Person() { Name = "John" };
            Person bob = new Person();
            var v = new ValidateFor<Person>()
                .Field(p => p.Name).Length(5).Length(3).Length(0, 3).Length(5, 6)
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 4);
            Assert.AreEqual(r.ErrorMessages[0], "Length of Person.Name should be 5.");
            Assert.AreEqual(r.ErrorMessages[1], "Length of Person.Name should be 3.");
            Assert.AreEqual(r.ErrorMessages[2], "Length of Person.Name should between 0 and 3.");
            Assert.AreEqual(r.ErrorMessages[3], "Length of Person.Name should between 5 and 6.");
        }
        
        [TestMethod]
        public void Validate_Greater_Than_Success()
        {
            Person john = new Person() { Age = 30, Height = 1.8m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldGreaterThan(0)
                .Field(p => p.Height).ShouldGreaterThan(0)
                .Build();
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
                .Field(p => p.Height).ShouldGreaterThan(0m)
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Person.Age should be greater than 0.");
            Assert.AreEqual(r.ErrorMessages[1], "Person.Height should be greater than 0.");
        }

        [TestMethod]
        public void Validate_Greater_Than_Or_Equal_To_Success()
        {
            Person john = new Person() { Age = 30, Height = 1.8m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldGreaterThanOrEqualTo(29)
                .Field(p => p.Height).ShouldGreaterThanOrEqualTo(1.8m)
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Greater_Than_Or_Equal_To_Failed()
        {
            Person john = new Person() { Age = 0, Height = -1m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldGreaterThanOrEqualTo(1)
                .Field(p => p.Height).ShouldGreaterThanOrEqualTo(0m)
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Person.Age should be greater than or equal to 1.");
            Assert.AreEqual(r.ErrorMessages[1], "Person.Height should be greater than or equal to 0.");
        }

        [TestMethod]
        public void Validate_Less_Than_Success()
        {
            Person john = new Person() { Age = 30, Height = 1.8m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldLessThan(31)
                .Field(p => p.Height).ShouldLessThan(2m)
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Less_Than_Failed()
        {
            Person john = new Person() { Age = 0, Height = 1m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldLessThan(0)
                .Field(p => p.Height).ShouldLessThan(0m)
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Person.Age should be less than 0.");
            Assert.AreEqual(r.ErrorMessages[1], "Person.Height should be less than 0.");
        }

        [TestMethod]
        public void Validate_Less_Than_Or_Equal_To_Success()
        {
            Person john = new Person() { Age = 30, Height = 1.8m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldLessThanOrEqualTo(31)
                .Field(p => p.Height).ShouldLessThanOrEqualTo(1.8m)
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Less_Than_Or_Equal_To_Failed()
        {
            Person john = new Person() { Age = 2, Height = 1m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldLessThanOrEqualTo(1)
                .Field(p => p.Height).ShouldLessThanOrEqualTo(0m)
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Person.Age should be less than or equal to 1.");
            Assert.AreEqual(r.ErrorMessages[1], "Person.Height should be less than or equal to 0.");
        }

        [TestMethod]
        public void Validate_Equal_To_Success()
        {
            Person john = new Person() { Age = 30, Height = 1.8m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldEqualTo(30)
                .Field(p => p.Height).ShouldEqualTo(1.8m)
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Equal_To_Failed()
        {
            Person john = new Person() { Age = 1, Height = 1m };
            var v = new ValidateFor<Person>()
                .Field(p => p.Age).ShouldEqualTo(0)
                .Field(p => p.Height).ShouldEqualTo(0m)
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Person.Age should be equal to 0.");
            Assert.AreEqual(r.ErrorMessages[1], "Person.Height should be equal to 0.");
        }

        //[TestMethod]
        //public void Validate_Have_Data_Wrong_Type()
        //{
        //    try
        //    {
        //        Person john = new Person();
        //        var v = new ValidateFor<Person>()
        //            .Field(p => p.Company).ShouldHaveData()
        //            .Build();
        //        var r = v.Validate(john);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual(ex.Message, "ShouldHaveData method only support ICollection type.");
        //        return;
        //    }

        //    Assert.Fail("Should throw exception: ShouldHaveData method only support ICollection type.");
        //}

        [TestMethod]
        public void Validate_Have_Data_Success()
        {
            Person john = new Person()
            {
                FavoriteBooks = new List<Book>() { new Book() }
            };
            var v = new ValidateFor<Person>()
                .EnumerableField<List<Book>, Book>(p => p.FavoriteBooks).ShouldHaveData()
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Have_Data_Failed()
        {
            Person john = new Person();
            var v = new ValidateFor<Person>()
                .EnumerableField<List<Book>, Book>(p => p.FavoriteBooks).ShouldHaveData()
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 1);
            Assert.AreEqual(r.ErrorMessages[0], "Person.FavoriteBooks should have data.");
        }

        [TestMethod]
        public void Validate_Sub_Entity_Success()
        {
            Person john = new Person()
            {
                Company = new Company()
                {
                    Address = "abc",
                    EmployeeCount = 1
                }
            };
            var v = new ValidateFor<Person>()
                .Field(p => p.Company).ValidateWith(
                    new ValidateFor<Company>()
                    .Field(c => c.Address).ShouldNotEmpty()
                    .Field(c => c.EmployeeCount).ShouldGreaterThan(0)
                    .Build()
                )
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Sub_Entity_Failed()
        {
            Person john = new Person();
            var v = new ValidateFor<Person>()
                .Field(p => p.Company).ValidateWith(
                    new ValidateFor<Company>()
                    .Field(c => c.Address).ShouldNotEmpty()
                    .Field(c => c.EmployeeCount).ShouldGreaterThan(0)
                    .Build()
                ).Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Company should not be null.");
            Assert.AreEqual(r.ErrorMessages[1], "Company should not be null.");

            john.Company = new Company();
            r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Company.Address should not be empty.");
            Assert.AreEqual(r.ErrorMessages[1], "Company.EmployeeCount should be greater than 0.");
        }

        [TestMethod]
        public void Validate_Sub_Collection_Entity_Success()
        {


            Person john = new Person()
            {
                FavoriteBooks = new List<Book>()
                {
                    new Book() {Name = "Hello world", PageCount =500 }
                }
            };
            var v = new ValidateFor<Person>()
                .EnumerableField<List<Book>, Book>(p => p.FavoriteBooks).ShouldHaveData().ValidateWith(
                    new ValidateFor<Book>()
                    .Field(b => b.Name).ShouldNotEmpty()
                    .Field(b => b.PageCount).ShouldGreaterThan(0)
                    .Build()
                ).Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Sub_Collection_Entity_Failed()
        {
            Person john = new Person()
            {
                FavoriteBooks = new List<Book>()
                {
                    new Book(),
                    new Book()
                }
            };
            var v = new ValidateFor<Person>()
                .EnumerableField<List<Book>, Book>(p => p.FavoriteBooks).ShouldHaveData().ValidateWith(
                    new ValidateFor<Book>()
                    .Field(b => b.Name).ShouldNotEmpty()
                    .Field(b => b.PageCount).ShouldGreaterThan(0)
                    .Build()
                ).Build();
            var r = v.Validate(john);
            
            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 2);
            Assert.AreEqual(r.ErrorMessages[0], "Person.FavoriteBooks Collection@1:Book.Name should not be empty.");
            Assert.AreEqual(r.ErrorMessages[1], "Person.FavoriteBooks Collection@1:Book.PageCount should be greater than 0.");
        }

        [TestMethod]
        public void Validate_Custom_Check_Success()
        {
            Person john = new Person();
            var v = new ValidateFor<Person>()
                .Entity().ShouldPassCustomCheck(CustomCheckOk)
                .Build();
            var r = v.Validate(john);

            Assert.IsTrue(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Validate_Custom_Check_Failed()
        {
            Person john = new Person();
            var v = new ValidateFor<Person>()
                .Entity().ShouldPassCustomCheck(CustomCheckFail)
                .Build();
            var r = v.Validate(john);

            Assert.IsFalse(r.IsValid);
            Assert.IsTrue(r.ErrorMessages.Count == 1);
            Assert.AreEqual(r.ErrorMessages[0], "Custom Check Failed.");
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
                ErrorMessages = new List<string> { "Custom Check Failed." }
            };
        }

        //[TestMethod]
        //public void Validate_List_Entity_Success()
        //{
        //    List<Person> persons = new List<Person>()
        //    {
        //        new Person() {Name = "John"}
        //    };
        //    var v = new ValidateFor<List<Person>>()
        //        .Field(s => s.Count).ShouldEqualTo(1)
        //        .Entity().ShouldHaveData().ValidateWith(
        //            new ValidateFor<Person>()
        //                .Field(p => p.Name).ShouldNotEmpty()
        //                .Build())
        //                .Build();
        //    var r = v.Validate(persons);
        //    Assert.IsTrue(r.IsValid);
        //}

        //[TestMethod]
        //public void Validate_List_Entity_Failed()
        //{
        //    List<Person> persons = new List<Person>();
        //    var v = new ValidateFor<List<Person>>()
        //        .Field(s => s.Count).ShouldEqualTo(1)
        //        .Entity().ShouldHaveData().ValidateWith(
        //            new ValidateFor<Person>()
        //                .Field(p => p.Name).ShouldNotEmpty()
        //                .Build()).Build();

        //    var r = v.Validate(persons);
        //    Assert.IsFalse(r.IsValid);
        //    Assert.AreEqual(r.ErrorMessages.Count, 2);
        //    Assert.AreEqual(r.ErrorMessages[0], "List`1.Count should be equal to 1.");
        //    Assert.AreEqual(r.ErrorMessages[1], "List`1 should have data.");

        //    persons.Add(new Person());
        //    r = v.Validate(persons);
        //    Assert.IsFalse(r.IsValid);
        //    Assert.AreEqual(r.ErrorMessages.Count, 1);
        //    Assert.AreEqual(r.ErrorMessages[0], "List`1 Collection@1:Person.Name should not be empty.");
        //}
    }
}
