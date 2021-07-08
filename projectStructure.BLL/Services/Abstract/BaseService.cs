using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.DAL;
using AutoMapper;

namespace projectStructure.BLL.Services
{
    public abstract class BaseService
    {
        private protected readonly ProjectsDbContext _context;
        private protected readonly IMapper _mapper;
        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public BaseService(IMapper mapper, ProjectsDbContext context)
        {
            _context =context;
            _mapper = mapper;
        }
    }
}
