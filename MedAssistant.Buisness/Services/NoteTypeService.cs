using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Data.Repositories;
using MedAssistant.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Buisness.Services
{
    public class NoteTypeService : INoteTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public NoteTypeService(IUnitOfWork unitOfWork, IMapper mapper) { 
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> AddNoteTypeAsync(NoteTypeDTO dto)
        {
            try
            {
                await unitOfWork.NoteType.AddAsync(mapper.Map<NoteType>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<List<NoteTypeDTO>> GetAllNoteTypes()
        {
            try
            {
                var Dtos = await unitOfWork.NoteType.GetAllAsync();
                return Dtos.Select(x => mapper.Map<NoteTypeDTO>(x)).ToList(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<NoteTypeDTO> GetNoteTypeByIdAsync(int id)
        {
            try
            {
                var entity = mapper.Map<NoteTypeDTO>(await unitOfWork.NoteType.GetByIdAsync(id)); 
                return entity;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<int> RemoveNoteTypeAsync(NoteTypeDTO dto)
        {
            try
            {
                unitOfWork.NoteType.Remove(mapper.Map<NoteType>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateNoteTypeAsync(NoteTypeDTO dto)
        {
            try
            {
                unitOfWork.NoteType.Update(mapper.Map<NoteType>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
