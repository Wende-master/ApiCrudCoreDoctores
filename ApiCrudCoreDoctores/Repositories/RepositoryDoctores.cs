using ApiCrudCoreDoctores.Data;
using ApiCrudCoreDoctores.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrudCoreDoctores.Repositories
{
    public class RepositoryDoctores
    {
        private DoctoresContext context;
        public RepositoryDoctores(DoctoresContext context)
        {
            this.context = context;
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await this.context.Doctores.ToListAsync();
        }

        public async Task<Doctor> FindDoctorAsync(int idDoctor)
        {
            return await this.context.Doctores.FirstOrDefaultAsync(z => z.IdDoctor == idDoctor);
        }

        private async Task<int> GetMaxIdDoctor()
        {
            if (this.context.Doctores.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Doctores.MaxAsync(z => z.IdDoctor) + 1;
            }
        }

        public async Task InsertDoctorAsync(int idHospital, string apellido, string especialidad, int salario)
        {
            Doctor doctor = new Doctor
            {
                IdDoctor = await this.GetMaxIdDoctor(),
                IdHospital = idHospital,
                Apellido = apellido,
                Especialidad = especialidad,
                Salario = salario
            };
            this.context.Doctores.Add(doctor);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(int idDoctor, int idHospital, string apellido, string especialidad, int salario)
        {
            Doctor doctor = await this.FindDoctorAsync(idDoctor);
            doctor.IdHospital = idHospital;
            doctor.Apellido = apellido;
            doctor.Especialidad = especialidad;
            doctor.Salario = salario;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int idDoctor)
        {
            Doctor doctor = await this.FindDoctorAsync(idDoctor);
            this.context.Doctores.Remove(doctor);
            await this.context.SaveChangesAsync();
        }
    }
}
