﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Microsoft.EntityFrameworkCore; //!!!

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Роли сотрудников
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RolesController
    {
        //!!!comm
        /*        private readonly IRepository<Role> _rolesRepository;

                public RolesController(IRepository<Role> rolesRepository)
                {
                    _rolesRepository = rolesRepository;
                }*/
        //!!!comm
        //!!!
        private readonly DataContext _dataContext;

        public RolesController(DataContext dataContext)
        {
            //_employeeRepository = employeeRepository; //!!!comm
            _dataContext = dataContext;
        }
        //!!!

        /// <summary>
        /// Получить все доступные роли сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<RoleItemResponse>> GetRolesAsync()
        {
            //var roles = await _rolesRepository.GetAllAsync(); //!!!comm
            var roles = await _dataContext.Roles.ToListAsync(); //!!!

            var rolesModelList = roles.Select(x => 
                new RoleItemResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();

            return rolesModelList;
        }
    }
}