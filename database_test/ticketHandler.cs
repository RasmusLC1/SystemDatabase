using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Tickets;

namespace TicketDatabase.Tests
{
    public class TicketHandlerTests
    {
        public TicketHandlerTests()
        {
            // Clear the singleton's internal list before each test
            ClearSingletonInstance(typeof(TicketHandler), "tickets");
        }

        private void ClearSingletonInstance(Type type, string fieldName)
        {
            var handler = type
                .GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                .GetValue(null);

            var field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            (field?.GetValue(handler) as IList<Ticket>)?.Clear();
        }

        [Fact]
        public void Singleton_Instance_IsNotNull()
        {
            // Act
            var instance = TicketHandler.Instance;

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void GetNextID_ReturnsIncrementedID()
        {
            // Arrange
            var handler = TicketHandler.Instance;
            var ticket1 = new Ticket(1, 100);
            var ticket2 = new Ticket(2, 101);

            // Act
            int nextID = handler.GetNextID();

            // Assert
            Assert.Equal(ticket2.ID + 1, nextID);
        }

        [Fact]
        public void GetNextID_ReturnsOne_WhenNoTicketsExist()
        {
            // Arrange
            var handler = TicketHandler.Instance;

            // Act
            int nextID = handler.GetNextID();

            // Assert
            Assert.Equal(1, nextID);
        }

        [Fact]
        public void GetTickets_ReturnsAllTickets()
        {
            // Arrange
            var handler = TicketHandler.Instance;
            var ticket1 = new Ticket(1, 100);
            var ticket2 = new Ticket(2, 101);

            // Act
            var tickets = handler.GetTickets();

            // Assert
            Assert.Equal(2, tickets.Count);
            Assert.Contains(ticket1, tickets);
            Assert.Contains(ticket2, tickets);
        }

        [Fact]
        public void RemoveTicket_DeletesExistingTicket()
        {
            // Arrange
            var handler = TicketHandler.Instance;
            var ticket = new Ticket(1, 100);
            int ticketID = ticket.ID;

            // Act
            bool removed = handler.RemoveTicket(ticketID);

            // Assert
            Assert.True(removed);
            Assert.Empty(handler.GetTickets());
        }

        [Fact]
        public void RemoveTicket_ReturnsFalse_WhenTicketNotFound()
        {
            // Arrange
            var handler = TicketHandler.Instance;

            // Act
            bool removed = handler.RemoveTicket(999); // Non-existent ID

            // Assert
            Assert.False(removed);
        }

        [Fact]
        public void PrintTicket_ReturnsLabel_WhenTicketExists()
        {
            // Arrange
            var handler = TicketHandler.Instance;
            var ticket = new Ticket(1, 100);

            // Act
            var label = handler.PrintTicket(ticket.ID);

            // Assert
            Assert.NotNull(label);
            Assert.Contains($"ID: {ticket.ID}", label.Text);
        }

        [Fact]
        public void PrintTicket_ReturnsNull_WhenTicketNotFound()
        {
            // Arrange
            var handler = TicketHandler.Instance;

            // Act
            var label = handler.PrintTicket(999); // Non-existent ID

            // Assert
            Assert.Null(label);
        }
    }
}
