using Givt.OnlineCheckout.Integrations.Interfaces;
using Newtonsoft.Json.Linq;

namespace Givt.OnlineCheckout.Business.Models
{
    public class TemplateEmailModel : BaseEmailModel
    {
        public TemplateEmailModel(EmailType emailType)
        {
            EmailType = emailType;
            TemplateData = new JObject();
        }
    }
}
