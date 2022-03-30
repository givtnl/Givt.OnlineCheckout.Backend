using Newtonsoft.Json.Linq;

namespace Givt.OnlineCheckout.Business.Models
{
    public class TemplateEmailModel : BaseEmailModel
    {
        public TemplateEmailModel(string templateName)
        {
            TemplateName = templateName;
            TemplateData = new JObject();
        }
    }
}
