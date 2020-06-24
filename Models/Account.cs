using GitBrainsBlogApi.DTO;
using GitBrainsBlogApi.Entities;
using GitBrainsBlogApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Security.Claims;

namespace GitBrainsBlogApi.Models
{
    public class Account : UserRepository
    {
        readonly Token _token = new Token();

        private IActionContextAccessor actionContextAccessor;
        private RoleRepository roleRepository;

        public Account(IActionContextAccessor actionContextAccessor, RoleRepository _roleRepository)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.roleRepository = _roleRepository;
        }

        public IEnumerable<Claim> Claims()
        {
            return actionContextAccessor.ActionContext.HttpContext.User.Claims;
        }

        public AccountDTO Authenticate(string _userName, string _password)
        {
            UserEntity findUser = base.FindByUsername(_userName);
            if (findUser is null) throw new HumanException("Пользователь не найден в системе.");
            if (findUser.password != _password) throw new HumanException("Пароль не корректный.");
            

            return new AccountDTO(findUser, _token.Generate(findUser.id), roleRepository.FindByUserId(findUser.id));
        }
    }
}
