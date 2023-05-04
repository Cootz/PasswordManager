using PasswordManager.Model.Converter;
using PasswordManager.Validation;

namespace PasswordManager.Tests.Converter;

[TestFixture]
public class FirstValidationConverterTest
{
    [Test]
    public void ConverterWithValidationObjectHavingErrorMessagesTest()
    {
        ValidatableObject<string> validatableObject = new();

        validatableObject.Errors = new List<string> { "First error message", "Second error message" };

        IValueConverter converter = new FirstValidationConverter();

        string? firstMessage = (string)converter.Convert(validatableObject.Errors, typeof(string), null, null);

        Assert.That(firstMessage, Is.EqualTo(validatableObject.Errors.First()));
    }

    [Test]
    public void ConverterWithValidationObjectNotHavingAnyMessagesTest()
    {
        ValidatableObject<string> validatableObject = new();

        IValueConverter converter = new FirstValidationConverter();

        string? firstMessage = (string)converter.Convert(validatableObject.Errors, typeof(string), null, null);

        Assert.That(firstMessage, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ConvertBackThrowsNotSupportedExceptionTest()
    {
        IValueConverter converter = new FirstValidationConverter();

        Assert.Throws<NotSupportedException>(() => converter.ConvertBack(null, null, null, null));
    }
}