using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation.Lite.Test
{
    [TestClass]
    public class ValidationResultTest
    {
        [TestMethod]
        public void Validation_Result_To_String()
        {
            var valid = ValidationResult.Valid;
            Assert.AreEqual(valid.ToString(), "Validate successfully.");

            var invalid = new ValidationResult(false);
            invalid.ErrorMessages.Add("Error1");
            invalid.ErrorMessages.Add("Error2");
            Assert.AreEqual(invalid.ToString(), @"Error1
Error2");
        }
    }
}
