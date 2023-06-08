using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
    public interface ITicketRepository
    {
        Task<Ticket> AddTicket(Ticket model);
        Task<Ticket> EditTicket(Ticket model);
        Task<Ticket> DeleteTicket(int id);
        List<Ticket> GetListOfTickets();
        Task<Ticket> GetTicketById(int id);
        Task<Ticket> UndoDeleteTicket(int id);
    }
}
