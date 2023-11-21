
using UnityEngine.SceneManagement;

namespace NetGrammar.Client.Mailboxes
{
    [ClientMailbox]
    public class ResetSceneClientMailbox : SimpleClientMailbox
    {
        protected override ushort MailboxPrefix => (ushort)NetDefs.MailPrefix.ResetScene;

        protected override MailboxGrammar Clone()
        {
            return new ResetSceneClientMailbox();
        }
        
        public override void HandleMessage(byte[] data, ushort prefix)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
