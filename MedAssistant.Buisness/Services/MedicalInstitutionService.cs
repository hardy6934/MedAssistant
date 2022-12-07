using AutoMapper;
using HtmlAgilityPack;
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
    public class MedicalInstitutionService : IMedicalInstitutionService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public MedicalInstitutionService(IMapper mapper, 
            IUnitOfWork unitOfWork) 
        { 

            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

      

        // ссылки городов для распаршивания данных о клиниках рб из сайта  https://med.beg.by/spisok-lpu/
        //public List<string> FindAllCityLinksForMedicalInstitutions()
        //{
        //    List<string> links = new();
        //    var web = new HtmlWeb();

        //    string url = "https://med.beg.by/spisok-lpu/";
        //    var HtmlDoc = web.Load(url);
        //    var nodes = HtmlDoc.DocumentNode.SelectNodes("//a").Where(x => x.HasClass("tab-head")).ToList();

        //    links.AddRange(nodes.Select(x => "https://med.beg.by/spisok-lpu/" + x.Attributes["href"].Value).ToList()); 

        //    return links;
        //}


        public async Task AddMedicalInstitutionsAsync()
        { 
            //https://med.beg.by/spisok-lpu/ только там по городам и метод дрлжен принимать ссылки городов

            //var web = new HtmlWeb();

            //List<MedicalInstitutionDTO> dtos = new();

            //foreach (var n in links)
            //{
            //    var HtmlDoc = web.Load(n);
            //    var nodes = HtmlDoc.DocumentNode.SelectNodes("//td").ToList();

            //    foreach (var node in nodes)
            //    { 
            //        dtos.Add(
            //            new MedicalInstitutionDTO
            //            {
            //                Name = node.InnerText
            //            });
            //    }
            //} 
            try
            {
                List<MedicalInstitutionDTO> dtos = new();
                var web = new HtmlWeb();
                int k = 1;
                while (true)
                {
                   
                    string url = "https://clinics.medsovet.info/belarus/bolnicy?cat=0&page="+k.ToString();
                    var HtmlDoc = web.Load(url);
                    var nodes = HtmlDoc.DocumentNode.SelectNodes("//div").Where(x => x.HasClass("clinic-item_info")).ToList();

                    if (nodes.Any())
                    {
                        foreach (var node in nodes)
                        { 
                            var Infolink = node.ChildNodes.Where(x=>x.HasClass("clinic-item_name")).FirstOrDefault()?.Attributes["href"].Value;
                            var Location = node.ChildNodes.Where(x => x.HasClass("clinic-item_address")).FirstOrDefault()?.InnerText;
                            var name = node.ChildNodes.Where(x => x.HasClass("clinic-item_name")).FirstOrDefault()?.InnerText;

                            if (name != null)
                            { 
                                dtos.Add(
                                    new MedicalInstitutionDTO
                                    {
                                        Name = name,
                                        Location = Location,
                                        InfoUrl = Infolink,

                                    });
                            }
                        }
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }

                var oldMedInstitutions = unitOfWork.MedicalInstitution.Get().Select(x => x.Name).Distinct().ToArray();
                var entities = dtos.Where(x => !oldMedInstitutions.Contains(x.Name)).Select(dto => mapper.Map<MedicalInstitution>(dto)).ToArray();

                await unitOfWork.MedicalInstitution.AddRangeAsync(entities);
                await unitOfWork.Commit();

            }
            catch (Exception)
            { 
                throw;
            }

           



        }

        public async Task<int> AddMedicalInstitutionAsync(MedicalInstitutionDTO dto)
        {
            try
            {
                await unitOfWork.MedicalInstitution.AddAsync(mapper.Map<MedicalInstitution>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<List<MedicalInstitutionDTO>> GetAllMedicalInstitutionsFromDataBaseAsync()
        {
            try
            {
                var Dtos = await unitOfWork.MedicalInstitution.GetAllAsync();
                return Dtos.Select(x => mapper.Map<MedicalInstitutionDTO>(x)).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MedicalInstitutionDTO> GetMedicalInstitutionByIdAsync(int id)
        {
            try
            {
                var entity = mapper.Map<MedicalInstitutionDTO>(await unitOfWork.MedicalInstitution.GetByIdAsync(id));

                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> RemoveMedicalInstitutionAsync(MedicalInstitutionDTO dto)
        {
            try
            {
                unitOfWork.MedicalInstitution.Remove(mapper.Map<MedicalInstitution>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateMedicalInstitutionAsync(MedicalInstitutionDTO dto)
        {
            try
            {
                unitOfWork.MedicalInstitution.Update(mapper.Map<MedicalInstitution>(dto));
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

