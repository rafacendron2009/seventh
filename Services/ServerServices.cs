using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using seventh.Data;
using seventh.Models;
using seventh.Resources;

namespace seventh.Services
{
    public class ServerServices : IServerServices
    {
        private readonly IMapper _mapper;
        private DataContext _dataContext;
        public ServerServices(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<CreateServerResponse> CreateServer(CreateServerResource server)
        {
            try
            {
                var model = _mapper.Map<CreateServerResource, Server>(server);
                model.Id = Guid.NewGuid().ToString();

                await _dataContext.Server.AddAsync(model);
                await _dataContext.SaveChangesAsync();

                return new CreateServerResponse(_mapper.Map<Server, ServerResource>(model));
            }
            catch (Exception ex)
            {
                return new CreateServerResponse(ex.Message);
            }
        }

        public async Task<ServerResponse> FindServerById(string serverId)
        {
            try
            {
                var server = await _dataContext.Server.AsNoTracking().FirstOrDefaultAsync(x => x.Id == serverId);
                if (server == null)
                    return new ServerResponse("Server not found ");

                return new ServerResponse(_mapper.Map<Server, ServerResource>(server));
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }

        public async Task<ServerResponse> RemoveServer(string serverId)
        {
            try
            {
                var response = await FindServerById(serverId);
                if (!response.Success)
                    return new ServerResponse("Server not found ");

                _dataContext.Server.Remove(_mapper.Map<ServerResource, Server>(response.Server));
                await _dataContext.SaveChangesAsync();

                return new ServerResponse(response.Server);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }


        public async Task<ListServerResponse> ListServer()
        {
            try
            {
                var result = await _dataContext.Server.AsNoTracking().ToListAsync();
                return new ListServerResponse(result);
            }
            catch (Exception ex)
            {
                return new ListServerResponse(ex.Message);
            }
        }

        public async Task<ServerModelResponse> ListVideosByserver(string serverId)
        {
            try
            {
                var server = await _dataContext.Server.AsNoTracking().Include(x => x.Videos).FirstOrDefaultAsync(x => x.Id == serverId);

                return new ServerModelResponse(server);
            }
            catch (Exception ex)
            {
                return new ServerModelResponse(ex.Message);
            }
        }
    }
}
