using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuestionsAndAnswers.Startup))]
namespace QuestionsAndAnswers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
