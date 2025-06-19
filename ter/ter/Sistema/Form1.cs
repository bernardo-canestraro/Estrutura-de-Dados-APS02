using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.ComponentModel;

namespace Sistema
{
    public partial class Form1 : Form
    {
        private TabelaHash<Paciente> tabela = new TabelaHash<Paciente>(10);

        // Controles de entrada
        private TextBox txtCPF, txtNome, txtPA, txtTemp, txtOxi;
        private DataGridView dgv;
        private Button btnInserir, btnBuscar, btnAtualizar, btnRemover, btnExibir;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.Text = "Sistema Clínico - Hash"; 
            this.Size = new Size(800, 600);
            CriarControles();
        }

        private void InitializeComponent()
        {
            // Este método deve ser implementado para inicializar os componentes do formulário.
            // Caso esteja faltando, você pode criá-lo manualmente ou verificar se ele foi gerado corretamente pelo designer.
        }

        private void CriarControles()
        {
            System.Windows.Forms.Label lblTitulo = new System.Windows.Forms.Label
            {
                Text = "Cadastro e Gerenciamento de Pacientes",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(120, 10),
                Size = new Size(400, 40) 
            };
            this.Controls.Add(lblTitulo);

            int y = 60;
            int xLabel = 40, xInput = 180, largura = 180, altura = 28, espacamento = 40;

            // CPF
            this.Controls.Add(new System.Windows.Forms.Label 
            { 
                Text = "CPF:", 
                Location = new Point(xLabel, y), 
                ForeColor = Color.DarkBlue, 
                Font = new Font("Segoe UI", 10), 
                Size = new Size(100, 20) 
            });
            txtCPF = new TextBox { Location = new Point(xInput, y), Width = largura };
            this.Controls.Add(txtCPF);

            // Nome
            y += espacamento;
            this.Controls.Add(new System.Windows.Forms.Label { Text = "Nome:", Location = new Point(xLabel, y), ForeColor = Color.DarkBlue, Font = new Font("Segoe UI", 10), Size = new Size(100, 20) });
            txtNome = new TextBox { Location = new Point(xInput, y), Width = largura };
            this.Controls.Add(txtNome);

            // Pressão Arterial
            y += espacamento;
            this.Controls.Add(new System.Windows.Forms.Label { Text = "Pressão Arterial:", Location = new Point(xLabel, y), ForeColor = Color.DarkBlue, Font = new Font("Segoe UI", 10), AutoSize = true });
            txtPA = new TextBox { Location = new Point(xInput, y), Width = largura };
            this.Controls.Add(txtPA);

            // Temperatura
            y += espacamento;
            this.Controls.Add(new System.Windows.Forms.Label { Text = "Temperatura (°C):", Location = new Point(xLabel, y), ForeColor = Color.DarkBlue, Font = new Font("Segoe UI", 10), AutoSize = true });
            txtTemp = new TextBox { Location = new Point(xInput, y), Width = largura };
            this.Controls.Add(txtTemp);

            // Oxigenação
            y += espacamento;
            this.Controls.Add(new System.Windows.Forms.Label { Text = "Oxigenação (%):", Location = new Point(xLabel, y), ForeColor = Color.DarkBlue, Font = new Font("Segoe UI", 10), AutoSize = true });
            txtOxi = new TextBox { Location = new Point(xInput, y), Width = largura };
            this.Controls.Add(txtOxi);

            // Botões
            int yBtn = 60, xBtn = 420, larguraBtn = 120, alturaBtn = 32, espacamentoBtn = 38;
            btnInserir = CriarBotao("Inserir", xBtn, yBtn, larguraBtn, alturaBtn, Color.RoyalBlue, BtnInserir_Click);
            btnBuscar = CriarBotao("Buscar", xBtn, yBtn + espacamentoBtn, larguraBtn, alturaBtn, Color.RoyalBlue, BtnBuscar_Click);
            btnAtualizar = CriarBotao("Atualizar", xBtn, yBtn + 2 * espacamentoBtn, larguraBtn, alturaBtn, Color.RoyalBlue, BtnAtualizar_Click);
            btnRemover = CriarBotao("Remover", xBtn, yBtn + 3 * espacamentoBtn, larguraBtn, alturaBtn, Color.RoyalBlue, BtnRemover_Click);
            btnExibir = CriarBotao("Exibir Todos", xBtn, yBtn + 4 * espacamentoBtn, larguraBtn, alturaBtn, Color.MediumBlue, BtnExibir_Click);

            this.Controls.AddRange(new Control[] { btnInserir, btnBuscar, btnAtualizar, btnRemover, btnExibir });

            // DataGridView
            dgv = new DataGridView
            {
                Location = new Point(40, 280),
                Size = new Size(700, 260),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = Color.White,
                GridColor = Color.LightBlue,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LightBlue,
                    ForeColor = Color.DarkBlue,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                }
            };
            dgv.Columns.Add("CPF", "CPF");
            dgv.Columns.Add("Nome", "Nome");
            dgv.Columns.Add("PA", "Pressão");
            dgv.Columns.Add("Temp", "Temperatura");
            dgv.Columns.Add("Oxi", "O₂");
            dgv.Columns.Add("Prioridade", "Prioridade");
            this.Controls.Add(dgv);
        }

        private Button CriarBotao(string texto, int x, int y, int largura, int altura, Color cor, EventHandler evento)
        {
            var btn = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(largura, altura),
                BackColor = cor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderColor = Color.DarkBlue;
            btn.Click += evento;
            return btn;
        }

        private void BtnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                var paciente = ObterPacienteDosCampos();
                tabela.Inserir(paciente.CPF, paciente);
                MessageBox.Show("Paciente inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
                ExibirTabela();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string cpf = txtCPF.Text.Trim();
                if (string.IsNullOrEmpty(cpf))
                    throw new Exception("Informe o CPF para buscar.");
                var paciente = tabela.Buscar(cpf);
                PreencherCampos(paciente);
                MessageBox.Show("Paciente encontrado!", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string cpf = txtCPF.Text.Trim();
                if (string.IsNullOrEmpty(cpf))
                    throw new Exception("Informe o CPF para atualizar.");
                var paciente = ObterPacienteDosCampos();
                tabela.Atualizar(cpf, paciente);
                MessageBox.Show("Paciente atualizado com sucesso!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
                ExibirTabela();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            try
            {
                string cpf = txtCPF.Text.Trim();
                if (string.IsNullOrEmpty(cpf))
                    throw new Exception("Informe o CPF para remover.");
                tabela.Remover(cpf);
                MessageBox.Show("Paciente removido com sucesso!", "Remoção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
                ExibirTabela();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExibir_Click(object sender, EventArgs e)
        {
            ExibirTabela();
        }

        private Paciente ObterPacienteDosCampos()
        {
            string cpf = txtCPF.Text.Trim();
            string nome = txtNome.Text.Trim();
            if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(nome))
                throw new Exception("CPF e Nome são obrigatórios.");
            if (!double.TryParse(txtPA.Text, out double pa))
                throw new Exception("Pressão arterial inválida.");
            if (!double.TryParse(txtTemp.Text, out double temp))
                throw new Exception("Temperatura inválida.");
            if (!double.TryParse(txtOxi.Text, out double oxi))
                throw new Exception("Oxigenação inválida.");
            return new Paciente(cpf, nome, pa, temp, oxi);
        }

        private void PreencherCampos(Paciente p)
        {
            txtCPF.Text = p.CPF;
            txtNome.Text = p.Nome;
            txtPA.Text = p.PressaoArterial.ToString();
            txtTemp.Text = p.Temperatura.ToString();
            txtOxi.Text = p.Oxigenacao.ToString();
        }

        private void LimparCampos()
        {
            txtCPF.Text = "";
            txtNome.Text = "";
            txtPA.Text = "";
            txtTemp.Text = "";
            txtOxi.Text = "";
        }

        private void ExibirTabela()
        {
            dgv.Rows.Clear();
            foreach (var bucket in tabela.Buckets)
            {
                foreach (var par in bucket)
                {
                    var p = par.Value;
                    int idx = dgv.Rows.Add(p.CPF, p.Nome, p.PressaoArterial, p.Temperatura, p.Oxigenacao, p.PrioridadeAtendimento.ToString());
                    Color cor = Color.White;
                    switch (p.PrioridadeAtendimento)
                    {
                        case Prioridade.Vermelha: cor = Color.Red; break;
                        case Prioridade.Amarela: cor = Color.Gold; break;
                        case Prioridade.Verde: cor = Color.LightGreen; break;
                    }
                    dgv.Rows[idx].DefaultCellStyle.BackColor = cor;
                    dgv.Rows[idx].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
    }
}