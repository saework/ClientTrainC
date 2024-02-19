using System;
using System.Collections.Generic;
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
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController
        : ControllerBase
    {
        //private readonly IRepository<Employee> _employeeRepository; //!!!comm
        private readonly DataContext _dataContext;

        //!!!comm
        /*        public EmployeesController(IRepository<Employee> employeeRepository)
                {
                    _employeeRepository = employeeRepository;
                }*/
        //!!!comm
        //!!!
        //public EmployeesController(IRepository<Employee> employeeRepository, DataContext dataContext)
        public EmployeesController(DataContext dataContext)
        {
            //_employeeRepository = employeeRepository; //!!!comm
            _dataContext = dataContext;
        }
        //!!!

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            //!!!comm
            /*            var employees = await _employeeRepository.GetAllAsync();
                        var employeesModelList = employees.Select(x =>
                            new EmployeeShortResponse()
                            {
                                Id = x.Id,
                                Email = x.Email,
                                FullName = x.FullName,
                            }).ToList();

                        return employeesModelList;*/
            //!!!comm
            //!!!

            var employeesModelList = await _dataContext.Employees.Select(x =>
                                        new EmployeeShortResponse()
                                        {
                                            Id = x.Id,
                                            Email = x.Email,
                                            FullName = x.FullName,
                                        }).ToListAsync();

            return employeesModelList;

            //!!!
        }
        
        /// <summary>
        /// Получить данные сотрудника по id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            //var employee = await _employeeRepository.GetByIdAsync(id); //!!!comm
            var employee = await _dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id); //!!!

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                /*                Role = new RoleItemResponse()
                                {
                                    Name = employee.Role.Name,
                                    Description = employee.Role.Description
                                },*/
                Role = null,
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }
    }
}