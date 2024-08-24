using System.Runtime.InteropServices;
using MosaicoSolutions.ViaCep;

namespace BuscaCep
{
    public partial class FrmBuscaCEP : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public FrmBuscaCEP()
        {
            InitializeComponent();

        }

        public void buscaCEP(string cep)
        {
            var viaCEPService = ViaCepService.Default();
            try
            {
                var endereco = viaCEPService.ObterEndereco(cep);

                txtRua.Text = endereco.Logradouro;
                txtBairro.Text = endereco.Bairro;
                txtCidade.Text = endereco.Localidade;
                cbbEstado.Text = endereco.UF;
                txtNumero.Focus();
            }
            catch (Exception)
            {

                MessageBox.Show("Favor inserir CEP válido!!!", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                limparCampos();
            }

        }

        public void limparCampos()
        {
            txtRua.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtNumero.Clear();
            mskCEP.Clear();
            mskCEP.Focus();
        }

        private void btnBuscaCEP_Click(object sender, EventArgs e)
        {
            buscaCEP(mskCEP.Text);
        }

        private void FrmBuscaCEP_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mskCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscaCEP(mskCEP.Text);
            }
            
        }
    }
}
