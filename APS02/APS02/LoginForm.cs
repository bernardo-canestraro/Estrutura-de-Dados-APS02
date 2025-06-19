using System;
using System.Drawing;
using System.Windows.Forms;

public class LoginForm : Form
{
    private Label lblUsuario;
    private Label lblSenha;
    private TextBox txtUsuario;
    private TextBox txtSenha;
    private Button btnLogin;

    public LoginForm()
    {
        this.Text = "Login - Sistema Médico";
        this.Size = new Size(350, 220);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;

        lblUsuario = new Label();
        lblUsuario.Text = "Usuário:";
        lblUsuario.Location = new Point(30, 30);
        lblUsuario.ForeColor = Color.Navy;
        lblUsuario.AutoSize = true;

        txtUsuario = new TextBox();
        txtUsuario.Location = new Point(110, 27);
        txtUsuario.Width = 180;

        lblSenha = new Label();
        lblSenha.Text = "Senha:";
        lblSenha.Location = new Point(30, 70);
        lblSenha.ForeColor = Color.Navy;
        lblSenha.AutoSize = true;

        txtSenha = new TextBox();
        txtSenha.Location = new Point(110, 67);
        txtSenha.Width = 180;
        txtSenha.PasswordChar = '*';

        btnLogin = new Button();
        btnLogin.Text = "Entrar";
        btnLogin.Location = new Point(110, 110);
        btnLogin.Width = 180;
        btnLogin.BackColor = Color.FromArgb(0, 120, 215);
        btnLogin.ForeColor = Color.White;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Click += BtnLogin_Click;

        this.Controls.Add(lblUsuario);
        this.Controls.Add(txtUsuario);
        this.Controls.Add(lblSenha);
        this.Controls.Add(txtSenha);
        this.Controls.Add(btnLogin);
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
        if (txtUsuario.Text == "admin" && txtSenha.Text == "1234")
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        else
        {
            MessageBox.Show("Usuário ou senha inválidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new LoginForm());
    }
}