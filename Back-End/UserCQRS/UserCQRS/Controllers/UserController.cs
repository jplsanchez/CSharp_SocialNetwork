using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Domain.Commands.User;
using User.Domain.Interfaces.Repositories;
using User.Domain.Models;

namespace User.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Default")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IReadRepository<UserModel> _repository;

        public UserController(IMediator mediator, IReadRepository<UserModel> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancelToken)
        {
            return Ok(await _repository.GetAll(cancelToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancelToken)
        {
            return Ok(await _repository.Get(id, cancelToken));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] RegisterUserCommand command, CancellationToken cancelToken)
        {
            var response = await _mediator.Send(command, cancelToken);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand command, CancellationToken cancelToken)
        {
            var response = await _mediator.Send(command, cancelToken);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancelToken)
        {
            var obj = new DeleteUserCommand { Id = id };
            var response = await _mediator.Send(obj, cancelToken);
            return Ok(response);
        }
    }
}