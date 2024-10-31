using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Application.ValueObjects;
using UniversityIT.Core.Abstractions.HelpDesk.Tickets;
using UniversityIT.Core.Models.HelpDesk;

namespace UniversityIT.Application.Services.HelpDesk
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IMessageService _telegramService;

        public TicketsService(ITicketsRepository ticketsRepository, [FromKeyedServices("telegram")] IMessageService telegramService)
        {
            _ticketsRepository = ticketsRepository;
            _telegramService = telegramService;
        }

        public async Task<Guid> CreateTicket(Ticket ticket)
        {
            Guid ticketId = await _ticketsRepository.Create(ticket);

            await NotifyAboutCreation(ticketId);

            return ticketId;
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _ticketsRepository.Get();
        }

        public async Task<Ticket> GetTicketById(Guid id)
        {
            return await _ticketsRepository.GetById(id);
        }

        public async Task<List<Ticket>> GetTicketsByUserId(Guid UserId)
        {
            return await _ticketsRepository.GetByUserId(UserId);
        }

        public async Task<Guid> UpdateTicket(Guid id, string name, string description, string place, bool isCompleted)
        {
            return await _ticketsRepository.Update(id, name, description, place, isCompleted);
        }

        public async Task<Guid> DeleteTicket(Guid id)
        {
            return await _ticketsRepository.Delete(id);
        }

        public async Task NotifyAboutCreation(Guid id)
        {
            var createdTicket = await _ticketsRepository.GetById(id);
            
            string message = $"User {createdTicket.Author} created a new request:"
                + $"\n{createdTicket.Name}\n{createdTicket.Description}";

            var success = await _telegramService.SendMessage(MessageReceiver.Create(0).Value, "", message);
            if (success)
            {
                await _ticketsRepository.SetNotification(id);
            }
        }
    }
}