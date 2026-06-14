using Microsoft.AspNetCore.Mvc;

namespace Renting.Microservice.Api.UseCases
{
    public interface IWebApiPresenter
    {
        IActionResult ActionResult { get; }
    }
}
