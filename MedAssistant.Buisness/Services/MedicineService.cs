using AutoMapper;
using MedAssistant.Data.Repositories;
using MedAssistant.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Net;
using HtmlAgilityPack;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedAssistant.Buisness.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }

        
        public async Task GetAllMedecinesFromTabletkaAsync()
        {
            try { 
                List<MedicineDTO> dtos = new();
                var web = new HtmlWeb(); 

                string[] leters = new string[] { "А","Б","В","Г","Д","Е","Ё","Ж","З","И","Й","К","Л","М","Н","О","П","Р","С","Т","У","Ф","Х","Ц","Ч","Ш","Ц","Щ","Э","Ю","Я","AZ","1" };
                 
                for (int i = 0; i < leters.Length; i++)
                {
                    int k = 1;
                    while (true)
                    { 
                        string url = "https://tabletka.by/drugs?search="+leters[i]+"&page="+k;
                        var HtmlDoc = web.Load(url);
                        var nodes = HtmlDoc.DocumentNode.SelectNodes("//li").Where(x => x.HasClass("search-result__item")).ToList();

                        if (nodes.Any())
                        {
                            foreach (var node in nodes)
                            { 
                                var hrefStart = node.InnerHtml.IndexOf("href="); 
                                var link = node.InnerHtml[(hrefStart + 5)..];
                                link = link.Split('>').First().Trim('"');
                                var Fulllink = "https://tabletka.by" + link;

                                dtos.Add(
                                    new MedicineDTO {
                                        Name = node.InnerText, 
                                        MedecineUrl = Fulllink 
                                    });
                            }
                            k++;
                        }
                        else 
                        {
                            break; 
                        } 
                    } 
                }

                var oldMedecines = unitOfWork.Medicines.Get().Select(x => x.Name).Distinct().ToArray(); 
                var entities = dtos.Where(x => !oldMedecines.Contains(x.Name)).Select(dto => mapper.Map<Medicine>(dto)).ToArray();

                await unitOfWork.Medicines.AddRangeAsync(entities);
                await unitOfWork.Commit();
            }
            catch
            {
                throw;
            }
        }


        public async Task<MedicineDTO> GetMedecineByIdAsync(int id)
        {
            try
            {
                var entity = mapper.Map<MedicineDTO>(await unitOfWork.Medicines.GetByIdAsync(id));
                 
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<MedicineDTO>> GetAllMedecinesFromDataBaseAsync()
        {
            try
            {
                var Dtos = await unitOfWork.Medicines.GetAllAsync();
                return Dtos.Select(x => mapper.Map<MedicineDTO>(x)).ToList();
                 
            }
            catch (Exception)
            { 
                throw;
            }
            
        }
        public async Task<int> AddMedecineAsync(MedicineDTO dto)
        {
            try
            {
                await unitOfWork.Medicines.AddAsync(mapper.Map<Medicine>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
         
        public async Task<int> UpdateMedecineAsync(MedicineDTO dto)
        {
            try
            {
                unitOfWork.Medicines.Update(mapper.Map<Medicine>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> RemoveMedecineAsync(MedicineDTO dto)
        {
            try
            {
                unitOfWork.Medicines.Remove(mapper.Map<Medicine>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        //Вытягивание производителя из tabletka.by плохо сделано, вдруг прийдет что-нибудь в голову
        //public async Task GetMonufactureToMedecinesAsync()
        //{
        //    int k = 0;
        //    var web = new HtmlWeb();
        //    List<string> list= new();

        //    foreach (var med in unitOfWork.Medicines.Get())
        //    {
        //        if (!string.IsNullOrEmpty(med.MedecineUrl))
        //        {
        //            var HtmlDoc = web.Load(med.MedecineUrl);

        //            var nodes = HtmlDoc.DocumentNode.SelectNodes("//td")
        //            .Where(x => x.HasClass("produce"))
        //            .ToList();
        //            k++;
        //            Console.WriteLine(k+"         -----//////////////////////////////////");



        //            if (nodes.Any())
        //            { 
        //                var monufacturer = nodes[0]?.InnerText.Trim().Split('\n').First().Trim();
        //                if (monufacturer != null)
        //                {
        //                    //med.Monufacturer = monufacturer;
        //                    list.Add(monufacturer);

        //                }
        //            }

        //        }
        //    }

        //    await unitOfWork.Commit();
        //}

    }
}
