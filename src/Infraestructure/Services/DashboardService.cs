using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using AutoMapper;
using Dapper;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http;


namespace Infraestructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public DashboardService(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Response<object>> GetData()
        {
            object list = new object();
            list = await _dbContext.clientes.ToListAsync();
            return new Response<object>(list);
        }

        public async Task<Response<string>> GetIp()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress IpAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            var ip = IpAddress?.ToString() ?? "No se puede obtener la ip";
            return new Response<string>(ip);
        }


    }
}
