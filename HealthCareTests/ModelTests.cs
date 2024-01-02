using HealthCare.Core.Models;

namespace HealthCareTests
{
    public class ModelTests
    {

            [Fact]
            public void Patient_Properties_SetCorrectly()
            {
                // Arrange
                var patient = new Patient
                {
                    Id = Guid.NewGuid(),
                    Name = "Muppet Muppetsson",
                    Email = "denna@example.com"
                };

                // Act

                // Assert
                Assert.Equal(patient.Id, patient.Id);
                Assert.Equal("Muppet Muppetsson", patient.Name);
                Assert.Equal("denna@example.com", patient.Email);
            }
        
    }
}