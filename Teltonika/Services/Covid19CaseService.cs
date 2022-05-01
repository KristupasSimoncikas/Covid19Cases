using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teltonika.DataModels;
using Teltonika.DBContext;
using Teltonika.Interfaces;

namespace Teltonika.Services
{
    public class Covid19CaseService : ICovid19CaseService
    {
        readonly Context _context;

        public Covid19CaseService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Covid19Case>> GetCovid19Case()
        {
            return await _context.Covid19Cases.Take(2000).OrderByDescending(q => q.Id).ToListAsync();
        }

        public async Task AddCovid19Case(Covid19Case cc)
        {
            await _context.Covid19Cases.AddAsync(cc);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCovid19Case(Covid19Case cc)
        {
            var ccToUpdate = await _context.Covid19Cases.SingleOrDefaultAsync(r => r.Id == cc.Id);
            if (ccToUpdate != null)
            {
                ccToUpdate.Gender = cc.Gender;
                ccToUpdate.AgeBracket = cc.AgeBracket;
                ccToUpdate.MunicipalityName = cc.MunicipalityName;
                ccToUpdate.MunicipalityCode = cc.MunicipalityCode;
                ccToUpdate.ConfirmationDate = cc.ConfirmationDate;
                ccToUpdate.CaseCode = cc.CaseCode;
                ccToUpdate.CoordinateX = cc.CoordinateX;
                ccToUpdate.CoordinateY = cc.CoordinateY;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCovid19Case(int id)
        {
            var ccToRemove= await _context.Covid19Cases.SingleOrDefaultAsync(r => r.Id == id);
            if (ccToRemove != null)
            {
                _context.Covid19Cases.Remove(ccToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Covid19Case> GetCovid19CaseById(int id)
        {
            return await _context.Covid19Cases
                .Where(r => r.Id == id)
                .SingleOrDefaultAsync();
        }

        public class ChartData
        {
            public string Group { get; set; }
            public int Count { get; set; }
        }

        // Chart data
        public async Task<IEnumerable<ChartData>> GetAgeBracketData()
        {
            const int sizeToDisplay = 100;
            int count = _context.Covid19Cases.OrderByDescending(q => q.Id).Select(q => q.Id).FirstOrDefault() - sizeToDisplay;

            return await _context.Covid19Cases
                    .Where(q => q.Id > count)
                    .GroupBy(p => p.AgeBracket)
                    .Select(g => new ChartData
                    {
                        Group = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();
           
        }
        public async Task<IEnumerable<ChartData>> GetGenderData()
        {
            const int sizeToDisplay = 100;
            int count = _context.Covid19Cases.OrderByDescending(q => q.Id).Select(q => q.Id).FirstOrDefault() - sizeToDisplay;

            return await _context.Covid19Cases
                    .Where(q => q.Id > count)
                    .GroupBy(p => p.Gender)
                    .Select(g => new ChartData
                    {
                        Group = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();

        }
        public async Task<IEnumerable<ChartData>> GetMunicipalityData()
        {
            const int sizeToDisplay = 100;
            int count = _context.Covid19Cases.OrderByDescending(q => q.Id).Select(q => q.Id).FirstOrDefault() - sizeToDisplay;

            return await _context.Covid19Cases
                    .Where(q => q.Id > count)
                    .GroupBy(p => p.MunicipalityName)
                    .Select(g => new ChartData
                    {
                        Group = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();

        }
        public async Task<IEnumerable<ChartData>> GetConfirmationDateData()
        {
            const int sizeToDisplay = 100;
            int count = _context.Covid19Cases.OrderByDescending(q => q.Id).Select(q => q.Id).FirstOrDefault() - sizeToDisplay;

            return await _context.Covid19Cases
                    .Where(q => q.Id > count)
                    .GroupBy(p => p.ConfirmationDate)
                    .Select(g => new ChartData
                    {
                        Group = g.Key.ToString(),
                        Count = g.Count()
                    })
                    .ToListAsync();

        }
    }
}
