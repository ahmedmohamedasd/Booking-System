using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Iservices
{
   public interface IGuestTicketRepository
    {
        Task<List<GuestTicket>> AddGuestTicket(List<GuestTicket> model);
        List<GuestTicket> GetListOfGuestTickets(int guestId);
        Task<int> EditGuestTicketsDateBooking(int guestId, DateTime DateOfBooking);
        Task<GuestTicket> EditGuestTickets(int guestId, GuestTicket model);
        Task<GuestTicket> DeleteGuestTickets(int guestId, int ticketId);
        Task<List<GuestTicket>> DeleteGuestTickets(int guestId);
    }
}
