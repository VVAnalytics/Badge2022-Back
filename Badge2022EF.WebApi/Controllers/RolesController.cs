﻿using Badge2022EF.DAL.Repositories;
using Badge2022EF.Models.Concretes;
using Badge2022EF.WebApi.Filters;
using Badge2022EF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace Badge2022EF.WebApi.Controllers
{
    [Authorization]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public RolesController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        // GET: api/<RolesController>
        [HttpGet]
        [Authorization("Admin")]
        public IEnumerable<Roles> GetAll()
        {
            return new ObservableCollection<Roles>(_roleRepository.GetAll()).ToList();
        }
        // GET: api/<MedecinsController>
        [HttpGet]
        [Authorization("Admin")]
        public IEnumerable<Roles> GetPage([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            return new ObservableCollection<Roles>(_roleRepository.GetAll(limit, offset)).ToList();
        }
        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Roles> GetOne(int id)
        {
            IEnumerable<Roles> aa = _roleRepository.GetOne2(id);
            foreach (var item in aa) { _ = item; };
            return aa.AsEnumerable();
        }

        // POST api/<RolesController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public void Post([FromBody] J_Roles newRole)
        {
            Roles role = new(
                        newRole.Name
                        )
            {
                Id = 0
            };
            _roleRepository.Add(role);
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Put(int id, [FromBody] J_Roles majRole)
        {
            Roles role = new(
                        majRole.Name
                        )
            {
                Id = id
            };
            _roleRepository.Update(role);
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Delete(int id)
        {
            _roleRepository.Delete(id);
        }
    }
}
