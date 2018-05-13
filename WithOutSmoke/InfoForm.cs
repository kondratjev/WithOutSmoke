using System.Windows.Forms;

namespace WithOutSmoke
{
    public partial class DiseaseForm : Form
    {
        public DiseaseForm()
        {
            InitializeComponent();
            ShowInTaskbar = false;
        }

        public void LoadRtfFile(string rtfFile)
        {
            richTextBox1.Rtf = rtfFile;
        }
    }
}
