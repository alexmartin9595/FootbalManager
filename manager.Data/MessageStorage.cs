using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.Data
{
    public class MessageStorage
    {
        private EFDbContext currentContext;

        public MessageStorage()
        {
            currentContext = new EFDbContext();
            currentContext.Configuration.LazyLoadingEnabled = true;
            currentContext.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return currentContext.Set<Message>();
        }

        public Message GetMessageById(int id)
        {
            return currentContext.Messages.FirstOrDefault(x =>x.Id == id);
        }

        public IEnumerable<Message> GetInputMessagesById (int id)
        {
            return currentContext.Set<Message>().Where(x => x.ReceiverId == id).OrderByDescending(x => x.Id).Include(x => x.SenderUser).ToList();
        }

        public IEnumerable<Message> GetOutputMessagesById(int id)
        {
            return currentContext.Set<Message>().Where(x => x.SenderId == id).OrderByDescending(x => x.Id).Include(x => x.ReceiverUser).ToList();
        }

        public void AddMessage(Message message)
        {
            currentContext.Messages.Add(message);
            currentContext.SaveChanges();
        }

        public void UpdateMessage(int messageId, Message message)
        {
            Message currentMessage = GetMessageById(messageId);
            currentMessage.IsRefused = message.IsRefused;
            currentMessage.IsSeen = message.IsSeen;
            currentContext.SaveChanges();
        }

        public void DeleteMessage(Message message)
        {
            currentContext.Messages.Remove(message);
            currentContext.SaveChanges();
        }
    }
}
