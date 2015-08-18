using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using manager.Business;
using manager.Entities;

namespace manager.Services
{
    public class MessageController : ApiController
    {
        [Authorize]
        [ActionName("GetInputMessages")]
        public IEnumerable<Message> GetInputMessages()
        {
            MessageLogic messageLogic = new MessageLogic();
            UserLogic userLogic = new UserLogic();
            int id = userLogic.GetIdByNameUser(User.Identity.Name);
            return messageLogic.GetInputMessagesById(id);
        }

        [Authorize]
        [ActionName("GetOutputMessages")]
        public IEnumerable<Message> GetOutputMessages()
        {
            MessageLogic messageLogic = new MessageLogic();
            UserLogic userLogic = new UserLogic();
            int id = userLogic.GetIdByNameUser(User.Identity.Name);
            return messageLogic.GetOutputMessagesById(id);
        }

        [Authorize]
        [ActionName("SendMessage")]
        public string SendMessage([FromBody] Message message)
        {
            MessageLogic messageLogic = new MessageLogic();
            try
            {
                messageLogic.AddMessage(message);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return "Сообщение успешно отправлено";
        }

        [Authorize]
        [HttpPut]
        [ActionName("UpdateMessage")]
        public string UpdateMessage(int id, [FromBody] Message message)
        {
            MessageLogic messageLogic = new MessageLogic();
            try
            {
                messageLogic.UpdateMessage(id, message);
            }
            catch (Exception exception)
            {
                return exception.Message; 
            }
            return "Ответ на приглашение успешно отправлен";
        }
    }
}
