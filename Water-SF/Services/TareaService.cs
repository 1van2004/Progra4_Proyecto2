using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Water_SF.Data;

namespace Water_SF.Services
{
    public class TareaService : ITareasService
    {
        private readonly WaterSFContext _tareaContext;

        public TareaService(WaterSFContext tareaContext)
        {
            _tareaContext = tareaContext;
        }

        public async Task<IEnumerable<Tarea>> Get(string[] ids)
        {
            var projects = _tareaContext.Tareas.AsQueryable();

            if (ids != null && ids.Any())
                projects = projects.Where(x => ids.Contains(x.Id));

            return await projects.ToListAsync();
        }

        public async Task<Tarea> Add(Tarea tarea)
        {
            await _tareaContext.Tareas.AddAsync(tarea);

            await _tareaContext.SaveChangesAsync();
            return tarea;
        }

        public async Task<IEnumerable<Tarea>> AddRange(IEnumerable<Tarea> projects)
        {
            await _tareaContext.Tareas.AddRangeAsync(projects);
            await _tareaContext.SaveChangesAsync();
            return projects;
        }

        public async Task<Tarea> Update(Tarea tarea)
        {
            var projectForChanges = await _tareaContext.Tareas.SingleAsync(x => x.Id == tarea.Id);
            projectForChanges.StartDate = tarea.StartDate;
            projectForChanges.EndDate = tarea.EndDate;
            projectForChanges.PerInCharge = tarea.PerInCharge;
            projectForChanges.Description = tarea.Description;
            projectForChanges.Priority = tarea.Priority;

            _tareaContext.Tareas.Update(projectForChanges);
            await _tareaContext.SaveChangesAsync();
            return tarea;
        }

        public async Task<bool> Delete(Tarea tarea)
        {
            try
            {
                var tareaExistente = await _tareaContext.Tareas.FindAsync(tarea.Id);
                if (tareaExistente == null)
                    return false;

                _tareaContext.Tareas.Remove(tareaExistente);
                await _tareaContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface ITareasService
    {
        Task<IEnumerable<Tarea>> Get(string[] ids);

        Task<Tarea> Add(Tarea tarea);

        Task<Tarea> Update(Tarea tarea);

        Task<bool> Delete(Tarea tarea);
    }
}
