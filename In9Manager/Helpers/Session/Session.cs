using In9Manager.Models;
using Newtonsoft.Json;

namespace In9Manager.Helpers.Session
{
    public class Session : ISessao
    {
        private IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Usuario GetSession()
        {
            string activeSession = _httpContext.HttpContext.Session.GetString("UsuarioLogado");
            if (string.IsNullOrEmpty(activeSession)) return null;
            return JsonConvert.DeserializeObject<Usuario>(activeSession);
        }

        public void RemoveSession()
        {
            _httpContext.HttpContext.Session.Remove("UsuarioLogado");
        }

        public void NewSession(Usuario usuario)
        {
            string model = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("UsuarioLogado", model);
        }
    }
}
