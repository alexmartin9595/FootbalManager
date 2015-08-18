using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;
using manager.Data;
using System.Text.RegularExpressions;

namespace manager.Business
{
    public class MessageLogic
    {
        private MessageStorage messageStorage;

        public MessageLogic()
        {
            messageStorage = new MessageStorage();
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return messageStorage.GetAllMessages();
        }

        public IEnumerable<Message> GetInputMessagesById(int id)
        {
            return messageStorage.GetInputMessagesById(id);
        }

        public IEnumerable<Message> GetOutputMessagesById(int id)
        {
            return messageStorage.GetOutputMessagesById(id);
        }

        public string GetSenderNameByMessage(int messageId)
        {
            return messageStorage.GetMessageById(messageId).SenderUser.UserName;
        }

        public void AddMessage(Message message)
        {
            if (message.Text.Equals(""))
                throw new Exception("Введите текст сообщения");
            //DeleteSymbols(message);
            messageStorage.AddMessage(message);
        }

        private void DeleteSymbols(Message message)
        {
            string pattern = @"[^\w\.@-]";
            string text = message.Text;
            Regex.Replace(text, pattern, "");
            message.Text = text;
        }

        public void UpdateMessage(int messageId, Message message)
        {
            Message currentMessage = messageStorage.GetMessageById(messageId);
            if (currentMessage == null)
                throw new Exception("Сообщение не найдено");
            messageStorage.UpdateMessage(messageId, message);
        }
    }
}
