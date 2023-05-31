using In9Manager.Models;

namespace In9Manager.Helpers.Session
{
    public interface ISessao
    {
        public Usuario GetSession();
        public void NewSession(Usuario session);
        public void RemoveSession();
    }
}
