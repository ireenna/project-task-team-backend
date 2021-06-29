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
        private protected readonly ThreadContext _context;
        private protected readonly IMapper _mapper;

        public BaseService(IMapper mapper)
        {
            _context = ThreadContext.getInstance();
            _mapper = mapper;
        }
    }
}
