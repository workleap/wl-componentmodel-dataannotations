namespace GSoft.ComponentModel.DataAnnotations.Tests;

public class MaxValueAttributeTests
{
    [Theory]
    [InlineData(50, 49)]
    [InlineData(int.MaxValue, 49)]
    [InlineData(int.MinValue, int.MinValue)]
    [InlineData(-50, -51)]
    public void GivenIntValue_ShouldPassValidation_WhenValueIsLessThanOrEqualToMax(int maxValue, int valueBeingValidated)
    {
        var maxValueAttribute = new MaxValue(maxValue);
         
        Assert.True(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(49, 50)]
    [InlineData(49, int.MaxValue)]
    [InlineData(-51, -50)]
    public void GivenIntValue_ShouldFailValidation_WhenValueIsMoreThanMax(int maxValue, int valueBeingValidated)
    {
        var maxValueAttribute = new MaxValue(maxValue);
         
        Assert.False(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(50.0, 49.0)]
    [InlineData(double.MaxValue, 49.0)]
    [InlineData(double.MinValue, double.MinValue)]
    [InlineData(-50.0, -51.0)]
    public void GivenDoubleValue_ShouldPassValidation_WhenValueIsLessThanOrEqualToMax(double maxValue, double valueBeingValidated)
    {
        var maxValueAttribute = new MaxValue(maxValue);
         
        Assert.True(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(49.0, 50.0)]
    [InlineData(49.0, double.MaxValue)]
    [InlineData(-51.0, -50.0)]
    public void GivenDoubleValue_ShouldFailValidation_WhenValueIsMoreThanToMax(double maxValue, double valueBeingValidated)
    {
        var maxValueAttribute = new MaxValue(maxValue);
         
        Assert.False(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(50.0, 49.0)]
    [InlineData(long.MaxValue, 49.0)]
    [InlineData(long.MinValue, long.MinValue)]
    [InlineData(-50.0, -51.0)]
    public void GivenLongValue_ShouldPassValidation_WhenValueIsLessThanOrEqualToMax(long maxValue, long valueBeingValidated)
    {
        var maxValueAttribute = new MaxValue(maxValue);
         
        Assert.True(maxValueAttribute.IsValid(valueBeingValidated));
    }
    
    [Theory]
    [InlineData(49, 50)]
    [InlineData(49.0, long.MaxValue)]
    [InlineData(-51, -50)]
    public void GivenLongValue_ShouldFailValidation_WhenValueIsMoreThanMax(long maxValue, long valueBeingValidated)
    {
        var maxValueAttribute = new MaxValue(maxValue);
         
        Assert.False(maxValueAttribute.IsValid(valueBeingValidated));
    }
}
