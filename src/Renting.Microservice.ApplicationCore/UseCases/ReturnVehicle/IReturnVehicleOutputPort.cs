namespace Renting.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output port for the Return Vehicle use case.
    /// </summary>
    public interface IReturnVehicleOutputPort : IOutputPortStandard<ReturnVehicleOutput>, IOutputPortNotFound
    {
    }
}
