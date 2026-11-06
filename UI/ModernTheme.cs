using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    internal static class ModernTheme
    {
        private static readonly Color Background = Color.FromArgb(17, 29, 45);
        private static readonly Color Surface = Color.FromArgb(28, 44, 64);
        private static readonly Color Accent = Color.FromArgb(45, 95, 195);
        private static readonly Color Text = Color.FromArgb(236, 243, 250);

        public static void Apply(Form form)
        {
            form.BackColor = Background;
            form.ForeColor = Text;
            form.Font = new Font("Segoe UI", 9.5F);
            form.StartPosition = FormStartPosition.CenterParent;
            Style(form.Controls);
        }

        private static void Style(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case Button button:
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderSize = 0;
                        button.BackColor = Accent;
                        button.ForeColor = Color.White;
                        button.Cursor = Cursors.Hand;
                        break;
                    case DataGridView grid:
                        grid.BackgroundColor = Surface;
                        grid.BorderStyle = BorderStyle.None;
                        grid.EnableHeadersVisualStyles = false;
                        grid.ColumnHeadersDefaultCellStyle.BackColor = Accent;
                        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        grid.DefaultCellStyle.BackColor = Surface;
                        grid.DefaultCellStyle.ForeColor = Text;
                        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 104, 174);
                        grid.DefaultCellStyle.SelectionForeColor = Color.White;
                        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(35, 54, 77);
                        grid.GridColor = Color.FromArgb(55, 72, 94);
                        break;
                    case TreeView tree:
                        tree.BackColor = Color.FromArgb(21, 35, 53);
                        tree.ForeColor = Text;
                        tree.BorderStyle = BorderStyle.FixedSingle;
                        tree.LineColor = Color.FromArgb(91, 119, 150);
                        break;
                    case TextBox box:
                        box.BackColor = Color.FromArgb(237, 243, 250);
                        box.ForeColor = Color.FromArgb(20, 32, 48);
                        box.BorderStyle = BorderStyle.FixedSingle;
                        break;
                    case ComboBox combo:
                        combo.BackColor = Color.FromArgb(237, 243, 250);
                        combo.ForeColor = Color.FromArgb(20, 32, 48);
                        break;
                    case Panel panel:
                        panel.BackColor = Surface;
                        break;
                    default:
                        control.ForeColor = Text;
                        break;
                }
                if (control.HasChildren) Style(control.Controls);
            }
        }
    }
}
