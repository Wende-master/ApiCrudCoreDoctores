using ApiCrudCoreDoctores.Models;
using ApiCrudCoreDoctores.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudCoreDoctores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private RepositoryDoctores repo;

        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctores()
        {
            return await this.repo.GetDoctorsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> FindDoctor(int id)
        {
            return await this.repo.FindDoctorAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostDoctor(Doctor doctor)
        {
            await this.repo.InsertDoctorAsync(doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario);
            return Ok();
        }

        [HttpPost]
        [Route("[action]/{idhospital}/{apellido}/{especialidad}/{salario}")]
        public async Task<ActionResult> PostDoctor(int idhospital, string apellido, string especialidad, int salario)
        {
            await this.repo.InsertDoctorAsync(idhospital, apellido, especialidad, salario);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutDoctor(Doctor doctor)
        {
            await this.repo.UpdateDoctorAsync(doctor.IdDoctor, doctor.IdHospital, doctor.Apellido,
                doctor.Especialidad,
                doctor.Salario
                );
            return Ok();
        }
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<ActionResult> PutDoctorWithParamas(int id, Doctor doctor)
        {
            await this.repo.UpdateDoctorAsync(id, doctor.IdHospital, doctor.Apellido,
                doctor.Especialidad, doctor.Salario
                );
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await this.repo.FindDoctorAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                await this.repo.DeleteDoctorAsync(id);
                return Ok();
            }
        }
    }
}
