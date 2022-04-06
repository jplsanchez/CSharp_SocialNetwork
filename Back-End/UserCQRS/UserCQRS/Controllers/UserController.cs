using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Domain.Commands.User;
using User.Domain.Interfaces.Repositories;
using User.Domain.Models;

namespace User.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _repository.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]  UpdateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var obj = new DeleteUserCommand { Id = id };
            var response = await _mediator.Send(obj);
            return Ok(response);
        }
    }
}